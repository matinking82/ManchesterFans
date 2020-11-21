using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManchesterFans.Domain.Entities.Users;
using ManchesterFans.Persistance.Context;
using ManchesterFans.Application.Interfaces;
using EndPoint.Site.ViewModels.AdminViewModels.Users;
using ManchesterFans.Application.Services.Users.Commands.CreateNewUser;
using ManchesterFans.Application.Services.Users.Commands.EditUserFromAdmin;
using Microsoft.AspNetCore.Authorization;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Master")]
    public class AdminsController : Controller
    {
        private readonly IUserFacad _userFacad;

        public AdminsController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        // GET: Admin/Admins
        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var result = _userFacad.GetAdminsListService.Execute(page, pageSize);
            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }



            return View(result.Data.Select(u => new AdminsListViewModel()
            {
                image = u.image,
                Level = u.Level,
                LoginId = u.LoginId,
                NickName = u.NickName,
                Username = u.Username
            }).ToList());
        }

        // GET: Admin/Admins/Details/5
        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _userFacad.GetAdminsDetailForAdminService.Execute(id);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(new AdminDetailViewModel()
            {
                Bio = result.Data.Bio,
                image = result.Data.image,
                Level = result.Data.Level,
                LoginId = result.Data.LoginId,
                NickName = result.Data.NickName,
                Username = result.Data.Username,
            });
        }

        // GET: Admin/Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Username,Password,Level,NickName")] CreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                RequestCreateNewUserDto request = new RequestCreateNewUserDto()
                {
                    Level = user.Level,
                    NickName = user.NickName,
                    Password = user.Password,
                    Username = user.Username,
                };
                var result = _userFacad.CreateNewUserService.Execute(request);

                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Admins/Edit/5
        public IActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _userFacad.GetUserForEditForAdmin.Execute(id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(new EditUserViewModel
            {
                Level = result.Data.Level,
                LoginId = result.Data.LoginId,
                NickName = result.Data.NickName,
                Username = result.Data.Username
            });
        }

        // POST: Admin/Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LoginId,Username,ConfirmPassword,Password,Level,NickName")] EditUserViewModel user)
        {
            if (id != user.LoginId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                RequestEditUserFromAdminDto request = new RequestEditUserFromAdminDto()
                {
                    Level = user.Level,
                    LoginId = user.LoginId,
                    NickName = user.NickName,
                    Password = user.Password,
                    Username = user.Username
                };

                var result = _userFacad.EditUserFromAdminService.Execute(request);

                if (!result.IsSuccess)
                {

                    if (result.Message == "n")
                    {
                        return NotFound();
                    }

                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Admins/Delete/5
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _userFacad.GetUserForDeleteFromAdmin.Execute(id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }

                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }
            return View(new DeleteUserViewModel()
            {
                image = result.Data.image,
                InsertTime = result.Data.InsertTime,
                Level = result.Data.Level,
                NickName = result.Data.NickName,
                Username = result.Data.Username,
                Id = result.Data.Id
            });
        }

        // POST: Admin/Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var result = _userFacad.DeleteUserService.Execute(id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }

                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
