using EndPoint.Site.ViewModels.SiteViewModels.Site;
using ManchesterFans.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class SiteHeader : ViewComponent
    {
        private readonly ISiteFacad _siteFacad;
        public SiteHeader(ISiteFacad siteFacad)
        {
            _siteFacad = siteFacad;
        }
        public IViewComponentResult Invoke()
        {
            var result = _siteFacad.GetSiteHeaderForSiteService.Execute();

            return View(viewName: "SiteHeader",
                model: new SiteHeaderViewModel()
                {
                    Links = result.Data.Links.Select(l => new Link()
                    {
                        DisplayText = l.DisplayText,
                        Url = l.Url
                    }).ToList(),
                    SiteName = result.Data.SiteName
                });
        }
    }
}
