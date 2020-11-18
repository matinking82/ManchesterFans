using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManchesterFans.Domain.Entities.Site;
using ManchesterFans.Persistance.Context;
using ManchesterFans.Application.Interfaces.FacadPatterns;
using EndPoint.Site.ViewModels.AdminViewModels.Site;
using ManchesterFans.Application.Services.Site.Commands.CreateHeaderLink;
using ManchesterFans.Application.Services.Site.Commands.EditHeaderLink;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeaderLinksController : Controller
    {
        ISiteFacad _siteFacad;
        public HeaderLinksController(ISiteFacad siteFacad)
        {
            _siteFacad = siteFacad;
        }

        // GET: Admin/HeaderLinks
        public IActionResult Index()
        {
            var result = _siteFacad.GetHeaderLinksForAdminService.Execute();
            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(
                result.Data.Select(h => new HeaderLinksViewModel()
                {
                    DisplayText = h.DisplayText,
                    LinkId = h.LinkId,
                    Url = h.Url
                }).ToList());
        }
        // GET: Admin/HeaderLinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HeaderLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DisplayText,Url")] HeaderLinksViewModel headerLinks)
        {
            if (ModelState.IsValid)
            {
                RequestCreateHeaderLink request = new RequestCreateHeaderLink()
                {
                    DisplayText = headerLinks.DisplayText,
                    Url = headerLinks.Url
                };
                var result = _siteFacad.CreateHeaderLinkService.Execute(request);

                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(new HeaderLinksViewModel()
            {
                DisplayText = headerLinks.DisplayText,
                Url = headerLinks.Url
            });
        }

        // GET: Admin/HeaderLinks/Edit/5
        public IActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var result = _siteFacad.GetSingleHeaderLinkForAdminService.Execute(id);
            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(new HeaderLinksViewModel()
            {
                DisplayText = result.Data.DisplayText,
                LinkId = result.Data.LinkId,
                Url = result.Data.Url
            });
        }

        // POST: Admin/HeaderLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LinkId,DisplayText,Url")] HeaderLinksViewModel headerLinks)
        {
            if (id != headerLinks.LinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                RequestEditHeaderLink request = new RequestEditHeaderLink()
                {
                    DisplayText = headerLinks.DisplayText,
                    LinkId = headerLinks.LinkId,
                    Url = headerLinks.Url
                };
                var result = _siteFacad.EditHeaderLinkService.Execute(request);

                return RedirectToAction(nameof(Index));
            }
            return View(headerLinks);
        }

        // GET: Admin/HeaderLinks/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _siteFacad.GetSingleHeaderLinkForAdminService.Execute(id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(new HeaderLinksViewModel()
            {
                DisplayText = result.Data.DisplayText,
                LinkId = result.Data.LinkId,
                Url = result.Data.Url
            });
        }

        // POST: Admin/HeaderLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _siteFacad.DeleteHeaderLinkService.Execute(id);

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
