using EndPoint.Site.ViewModels.SiteViewModels.Site;
using ManchesterFans.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class FooterCategories:ViewComponent
    {
        private readonly ISiteFacad _siteFacad;

        public FooterCategories(ISiteFacad siteFacad)
        {
            _siteFacad = siteFacad;
        }

        public IViewComponentResult Invoke()
        {
            var result = _siteFacad.GetGroupsForSiteFooter.Execute();


            return View(viewName: "FooterCategories",
                model: result.Data.Select(g=> new FooterCategoriesViewModel()
                {
                    GroupName = g.GroupName,
                    PagesCount = g.PagesCount,
                    GroupId = g.GroupId
                }));
        }

    }
}
