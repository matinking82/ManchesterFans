using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Site.ViewModels.AdminViewModels.Site;
using ManchesterFans.Application.Interfaces.FacadPatterns;
using ManchesterFans.Application.Services.Site.Commands.EditSiteName;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeaderController : Controller
    {
        ISiteFacad _siteFacad;
        public HeaderController(ISiteFacad siteFacad)
        {
            _siteFacad = siteFacad;
        }
        public IActionResult Index()
        {
            var result = _siteFacad.GetSiteNameForAdminService.Execute();
            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }
            return View(new SiteNameViewModel()
            {
                SiteName = result.Data.SiteName
            });
        }

        [HttpPost]
        public IActionResult Edit([Bind("SiteName")] SiteNameViewModel site)
        {
            if (ModelState.IsValid)
            {
                RequestEditSiteNameDto request = new RequestEditSiteNameDto()
                {
                    SiteName = site.SiteName
                };
                var result = _siteFacad.EditSiteNameService.Execute(request);
                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }
            }
            return RedirectToAction("Index","Home");
        }
    }
}
