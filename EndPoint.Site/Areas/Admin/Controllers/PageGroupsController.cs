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
using ManchesterFans.Application.Services.Pages.Queries.GetPageGrpoupsForAdmin;
using EndPoint.Site.ViewModels;
using ManchesterFans.Application.Services.Pages.Commands.EditPageGroup;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageGroupsController : Controller
    {
        private readonly IPageFacad _pageFacad;

        public PageGroupsController(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }


        // GET: Admin/PageGroups
        public IActionResult Index(int page = 1, int pageSize = 50)
        {
            var result = _pageFacad.GetPageGroupsForAdminService.Execute();

            return View(result.Data
                .Select(pg => new PageGroupsForAdminViewModel()
                {
                    GroupId = pg.GroupId,
                    GroupName = pg.GroupName
                }).ToList());
        }


        // GET: Admin/PageGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PageGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupName")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {

                var result = _pageFacad.CreatePageGroupService.Execute(pageGroup.GroupName);
                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }
                return RedirectToAction(nameof(Index));

            }
            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Edit/5
        public IActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _pageFacad.GetSinglePageGroupService.Execute(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return View(new PageGroupsForAdminViewModel()
            {
                GroupId = result.Data.GroupId,
                GroupName = result.Data.GroupName
            });
        }

        // POST: Admin/PageGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("GroupId,GroupName")] PageGroup pageGroup)
        {
            if (id != pageGroup.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                RequestEditPageGroupDto request = new RequestEditPageGroupDto()
                {
                    GroupId = pageGroup.GroupId,
                    GroupName = pageGroup.GroupName
                };
                var result = _pageFacad.EditPageGroupService.Execute(request);

                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Delete/5
        public IActionResult Delete(int id=0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _pageFacad.GetSinglePageGroupService.Execute(id);
            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;

                return View("ShowMessage");
            }

            return View(new PageGroupsForAdminViewModel()
            {
                GroupId = result.Data.GroupId, 
                GroupName = result.Data.GroupName
            });
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _pageFacad.DeletePageGroupService.Execute(id);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
