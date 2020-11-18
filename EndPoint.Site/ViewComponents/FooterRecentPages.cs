using EndPoint.Site.Convertors;
using EndPoint.Site.ViewModels.SiteViewModels.Site;
using ManchesterFans.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class FooterRecentPages:ViewComponent
    {
        private readonly ISiteFacad _siteFacad;
        public FooterRecentPages(ISiteFacad siteFacad)
        {
            _siteFacad = siteFacad;
        }
        public IViewComponentResult Invoke()
        {
            var result = _siteFacad.GetRecentPagesForSiteFooterService.Execute();



            return View(viewName: "FooterRecentPages",
                model:result.Data
                .Select(p=> new FooterRecentPagesViewModel()
                {
                    Date = p.Date.ToShamsi(),
                    ImageName = p.ImageName,
                    PageId = p.PageId,
                    Title = p.Title,
                }));
        }
    }
}
