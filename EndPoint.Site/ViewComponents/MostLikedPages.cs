﻿using EndPoint.Site.Convertors;
using EndPoint.Site.ViewModels.SiteViewModels.Page;
using ManchesterFans.Application.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EndPoint.Site.ViewComponents
{
    public class MostLikedPages : ViewComponent
    {
        IPageFacad _pageFacad;
        public MostLikedPages(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }
        public IViewComponentResult Invoke()
        {
            var result = _pageFacad.GetMostLikedPagesForSiteService.Execute(3);
            if (!result.IsSuccess)
            {
                throw new Exception(result.Message);
            }

            return View("MostLikedPages",result.Data
                .Select(p=> new SinglePageForSiteCardsViewModel()
                {
                    CreateDate = p.CreateDate.ToShamsi(),
                    GroupName = p.GroupName,
                    ImageName =p.ImageName,
                    PageId = p.PageId,
                    ShortDescribtion = p.ShortDescribtion,
                    Title = p.Title,
                }).ToList());
        }
    }
}
