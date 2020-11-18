using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EndPoint.Site.Convertors;
using EndPoint.Site.ViewModels.SiteViewModels.Page;
using ManchesterFans.Application.FacadPatterns;
using ManchesterFans.Application.Services.Pages.Commands.PageLiked;
using ManchesterFans.Application.Services.Pages.Commands.PageVisited;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesForSearch;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPageFacad _pageFacad;
        public PagesController(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }

        [HttpGet]
        [Route("Page/{id?}")]
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _pageFacad.GetSinglePageForSiteService.Execute(id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            RequestPageVisited VisitRequest = new RequestPageVisited()
            {
                PageId = id
            };

            var VisitResult = _pageFacad.PageVisitedService.Execute(VisitRequest);
            if (!VisitResult.IsSuccess)
            {
                ViewBag.Message = VisitResult.Message;
                return View("ShowMessage");
            }

            return View(new SinglePageForSiteViewModel()
            {
                CreateDate = result.Data.CreateDate,
                GroupId = result.Data.GroupId,
                GroupName = result.Data.GroupName,
                ImageName = result.Data.ImageName,
                Likes = result.Data.Likes,
                Tags = result.Data.Tags,
                Text = result.Data.Text,
                Title = result.Data.Title,
                Visits = result.Data.Visits,
                PageId = result.Data.PageId
            });
        }


        [HttpGet]
        [Route("/Search")]
        public IActionResult Search(string Key, int page = 1, int pageSize = 15)
        {

            RequestGetPagesForSearchServiceDto request = new RequestGetPagesForSearchServiceDto()
            {
                Key = Key,
                Page = page,
                PageSize = pageSize
            };

            var result = _pageFacad.GetPagesForSearchService.Execute(request);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            ViewBag.SearchKey = Key;
            return View(result.Data
                .Select(p => new PagesForSearchResultViewModel()
                {
                    CreateDate = p.CreateDate.ToShamsi(),
                    GroupName = p.GroupName,
                    ImageName = p.ImageName,
                    Likes = p.Likes,
                    PageId = p.PageId,
                    ShortDescribtion = p.ShortDescribtion,
                    Title = p.Title,
                    Visits = p.Visits,
                }));
        }



        [Route("/Category/{GroupId}")]
        public IActionResult CategoryPages(int GroupId, int page = 1, int pageSize = 20)
        {
            var result = _pageFacad.GetPagesPerGroupForSiteService.Execute(GroupId, page, pageSize);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }

                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(result.Data
                .Select(p => new SinglePageForSiteCardsViewModel()
                {
                    CreateDate = p.CreateDate.ToShamsi(),
                    GroupId = p.GroupId,
                    GroupName = p.GroupName,
                    ImageName = p.ImageName,
                    Likes = p.Likes,
                    PageId = p.PageId,
                    ShortDescribtion = p.ShortDescribtion,
                    Title = p.Title,
                    Visits = p.Visits,
                }));
        }


        [HttpGet]
        [Route("/PageLike/{PageId}")]
        public IActionResult Like(int PageId)
        {

            if (User.Identity.IsAuthenticated)
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                RequestPageLikedDto request = new RequestPageLikedDto()
                {
                    PageId = PageId,
                    UserId = UserId,
                };
                var result = _pageFacad.PageLikedService.Execute(request);

                if (!result.IsSuccess)
                {
                    if (result.Message=="n")
                    {
                        return NotFound();
                    }

                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }
            }

            return Redirect($"/Page/{PageId}");
        }
    }
}
