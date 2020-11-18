using EndPoint.Site.Convertors;
using EndPoint.Site.ViewModels.SiteViewModels.Page;
using ManchesterFans.Application.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class RecentPagesInHome : ViewComponent
    {
        IPageFacad _pageFacad;
        public RecentPagesInHome(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }
        public IViewComponentResult Invoke()
        {
            var result = _pageFacad.GetRecentPagesForSiteService.Execute(8);

            if (!result.IsSuccess)
            {
                throw new Exception(result.Message);
            }

            return View("RecentPagesInHome", result.Data
                .Select(p => new SinglePageForSiteCardsViewModel()
                {
                    CreateDate = p.CreateDate.ToShamsi(),
                    GroupName = p.GroupName,
                    Likes = p.Likes,
                    PageId = p.PageId,
                    ShortDescribtion = p.ShortDescribtion,
                    Title = p.Title,
                    Visits = p.Visits,
                    ImageName = p.ImageName,
                    GroupId = p.GroupId
                }));
        }
    }
}
