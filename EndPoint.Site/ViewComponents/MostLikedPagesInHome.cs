using EndPoint.Site.Convertors;
using EndPoint.Site.ViewModels.SiteViewModels.Page;
using ManchesterFans.Application.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EndPoint.Site.ViewComponents
{
    public class MostLikedPagesInHome : ViewComponent
    {
        IPageFacad _pageFacad;
        public MostLikedPagesInHome(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }
        public IViewComponentResult Invoke()
        {
            var result = _pageFacad.GetMostLikedPagesForSiteService.Execute(4);

            if (!result.IsSuccess)
            {
                throw new Exception(result.Message);
            }

            return View("MostLikedPagesInHome",result.Data
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
