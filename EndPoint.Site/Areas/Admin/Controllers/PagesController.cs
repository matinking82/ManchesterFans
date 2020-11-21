using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManchesterFans.Domain.Entities.Pages;
using ManchesterFans.Persistance.Context;
using ManchesterFans.Application.FacadPatterns;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesForAdmin;
using EndPoint.Site.ViewModels.AdminViewModels;
using ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForAdmin;
using Microsoft.AspNetCore.Http;
using EndPoint.Site.ViewModels.AdminViewModels.Page;
using ManchesterFans.Application.Services.Pages.Commands.CreatePage;
using ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForEdit;
using ManchesterFans.Application.Services.Pages.Commands.EditPage;
using Microsoft.AspNetCore.Authorization;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class PagesController : Controller
    {
        private readonly IPageFacad _pageFacad;

        public PagesController(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }

        // GET: Admin/Pages
        public IActionResult Index(int page=1, int pageSize=20)
        {
            RequestPagesForAdminDto request = new RequestPagesForAdminDto()
            {
                Page = page,
                PageSize = pageSize
            };

            var result = _pageFacad.GetPagesForAdminService.Execute(request);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }



            return View(result.Data.Select(p => new PagesForAdminViewModel()
            {
                CreateDate = p.CreateDate,
                ImageName = p.ImageName,
                Likes = p.Likes,
                PageId = p.PageId,
                ShortDescribtion = p.ShortDescribtion,
                Title = p.Title,
                Visits = p.Visits,
                Group = p.Group
            }).ToList());
        }

        // GET: Admin/Pages/Details/5
        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            RequestSinglePageForAdminDto request = new RequestSinglePageForAdminDto()
            {
                PageId = id
            };

            var result = _pageFacad.GetSinglePageForAdminService.Execute(request);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }


            return View(new SinglePageForAdminViewModel()
            {
                PageId = result.Data.PageId,
                CreateDate = result.Data.CreateDate,
                ImageName = result.Data.ImageName,
                Likes = result.Data.Likes,
                ShortDescribtion = result.Data.ShortDescribtion,
                Tags = result.Data.Tags,
                Text = result.Data.Text,
                Title = result.Data.Title,
                Visits = result.Data.Visits,
                Group = result.Data.Group
            });
        }

        // GET: Admin/Pages/Create
        public IActionResult Create()
        {
            var GetGPResult = _pageFacad.GetPageGroupsForAdminService.Execute();
            if (GetGPResult.IsSuccess)
            {
                ViewBag.GroupSelect = new SelectList(GetGPResult.Data, "GroupId", "GroupName");
            }
            else
            {
                ViewBag.Message = GetGPResult.Message;
                return View("ShowMessage");
            }
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,ShortDescribtion,Text,Tags,GroupId")] CreatePageViewModel page, IFormFile imgUp)
        {
            if (ModelState.IsValid)
            {
                RequestCreatePageService request = new RequestCreatePageService()
                {
                    GroupId = page.GroupId,
                    ImageFile = imgUp,
                    ShortDescribtion = page.ShortDescribtion,
                    Tags = page.Tags,
                    Text = page.Text,
                    Title = page.Title
                };

                var result = _pageFacad.CreatePageService.Execute(request);


                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }

                return Redirect("/admin/pages");
            }
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public IActionResult Edit(int id = 0)
        {

            if (id == 0)
            {
                return NotFound();
            }
            RequestGetSinglePageForEditDto request = new RequestGetSinglePageForEditDto()
            {
                PageId = id
            };

            var result = _pageFacad.GetSinglePageForEditService.Execute(request);


            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            var GetGPResult = _pageFacad.GetPageGroupsForAdminService.Execute();
            if (GetGPResult.IsSuccess)
            {
                ViewBag.GroupSelect = new SelectList(GetGPResult.Data, "GroupId", "GroupName");
            }
            else
            {
                ViewBag.Message = GetGPResult.Message;
                return View("ShowMessage");
            }

            return View(new EditPageViewModel()
            {
                GroupId = result.Data.GroupId,
                PageId = result.Data.PageId,
                ShortDescribtion = result.Data.ShortDescribtion,
                Tags = result.Data.Tags,
                Text = result.Data.Text,
                Title = result.Data.Title,
                ImageName = result.Data.ImageName
            });
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PageId,Title,ShortDescribtion,Text,Tags,GroupId,ImageName")] EditPageViewModel page, IFormFile imgUp)
        {
            if (id != page.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                RequestEditPageDto request = new RequestEditPageDto()
                {
                    GroupId = page.GroupId,
                    ImageFile = imgUp,
                    PageId = page.PageId,
                    ShortDescribtion = page.ShortDescribtion,
                    Tags = page.Tags,
                    Text = page.Text,
                    Title = page.Title
                };
                var result = _pageFacad.EditPageService.Execute(request);
                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            RequestSinglePageForAdminDto request = new RequestSinglePageForAdminDto()
            {
                PageId = id
            };
            var result = _pageFacad.GetSinglePageForAdminService.Execute(request);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(new SinglePageForAdminDto()
            {
                CreateDate = result.Data.CreateDate,
                Group = result.Data.Group,
                ImageName = result.Data.ImageName,
                Likes = result.Data.Likes,
                PageId = result.Data.PageId,
                ShortDescribtion = result.Data.ShortDescribtion,
                Tags = result.Data.Tags,
                Text = result.Data.Text,
                Title = result.Data.Title,
                Visits = result.Data.Visits
            });
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _pageFacad.DeletePageService.Execute(id);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
