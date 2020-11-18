using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManchesterFans.Domain.Entities.Pages;
using ManchesterFans.Persistance.Context;
using ManchesterFans.Application.FacadPatterns;
using EndPoint.Site.ViewModels.AdminViewModels.Page;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesForSliderSelect;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IPageFacad _pageFacad;

        public SliderController(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }

        // GET: Admin/Slider
        public IActionResult Index()
        {
            var result = _pageFacad.GetSliderPagesListForAdmin.Execute();

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(result.Data
                .Select(p => new SliderPagesListViewModel()
                {
                    GroupName = p.GroupName,
                    ImageName = p.ImageName,
                    Likes = p.Likes,
                    PageId = p.PageId,
                    ShortDescribtion = p.ShortDescribtion,
                    Title = p.Title,
                    Visits = p.Visits
                }));
        }

        // GET: Admin/Slider/Details/5
        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _pageFacad.GetSliderPagesDetailForAdmin.Execute(id);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(new SliderPagesDetailViewModel()
            {
                PageId = result.Data.PageId,
                GroupName = result.Data.PageId,
                ImageName = result.Data.ImageName,
                InsertTime = result.Data.InsertTime,
                Likes = result.Data.Likes,
                Tags = result.Data.Tags,
                ShortDescribtion = result.Data.ShortDescribtion,
                Text = result.Data.Text,
                Title = result.Data.Title,
                Visits = result.Data.Visits,
            });
        }

        public IActionResult Add(string search, int Page = 1, int PageSize = 30)
        {
            RequestGetPagesListForSliderSelect request = new RequestGetPagesListForSliderSelect()
            {
                Page = Page,
                PageSize = PageSize,
                Search = search
            };

            var result = _pageFacad.GetPagesListForSliderSelectService.Execute(request);


            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(result.Data.Select(p => new PagesListForSelectSliderViewModel()
            {
                GroupName = p.GroupName,
                ImageName = p.ImageName,
                PageId = p.PageId,
                ShortDescribtion = p.ShortDescribtion,
                Title = p.Title,
            }));
        }

        public IActionResult AddConfirm(int Id = 0)
        {
            if (Id==0)
            {
                return NotFound();
            }

            var result = _pageFacad.AddPageToSliderService.Execute(Id);

            if (!result.IsSuccess)
            {
                if (result.Message=="n")
                {
                    return NotFound();
                }
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }


            var result = _pageFacad.RemovePageFromSliderService.Execute(id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return RedirectToAction("Index");
        }
    }
}
