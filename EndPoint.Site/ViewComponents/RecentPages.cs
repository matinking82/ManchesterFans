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
    public class RecentPages : ViewComponent
    {
        IPageFacad _pageFacad;
        public RecentPages(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }
        public IViewComponentResult Invoke()
        {
            var result = _pageFacad.GetRecentPagesForSiteService.Execute(3);
            if (!result.IsSuccess)
            {
                throw new Exception(result.Message);
            }

            return View("RecentPages", result.Data
                .Select(p => new SinglePageForSiteCardsViewModel()
                {
                    CreateDate = p.CreateDate.ToShamsi(),
                    GroupName = p.GroupName,
                    ImageName = p.ImageName,
                    PageId = p.PageId,
                    ShortDescribtion = p.ShortDescribtion,
                    Title = p.Title,
                }).ToList());
        }
    }
}
