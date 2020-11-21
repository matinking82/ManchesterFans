using EndPoint.Site.Convertors;
using EndPoint.Site.ViewModels.AdminViewModels.PageComments;
using ManchesterFans.Application.FacadPatterns;
using ManchesterFans.Application.Services.Pages.Queries.GetUnAcceptedComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class CommentsController : Controller
    {
        private readonly IPageFacad _pageFacad;
        public CommentsController(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }

        public IActionResult Index(int page = 1, int pageSize = 30)
        {
            RequestGetUnAcceptedCommentsDto request = new RequestGetUnAcceptedCommentsDto()
            {
                Page = page,
                PageSize = pageSize
            };

            var result = _pageFacad.GetUnAcceptedCommentsService.Execute(request);

            if (!result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return View(result.Data
                .Select(c => new UnAcceptedCommentsViewModel()
                {
                    Comment = c.Comment,
                    CommentId = c.CommentId,
                    InsertTime = c.InsertTime.ToShamsi(),
                    PageId = c.PageId,
                    PageImage = c.PageImage,
                    PageTitle = c.PageTitle,
                }));
        }


        public IActionResult AcceptComments(int Id)
        {
            var result = _pageFacad.AcceptPageCommentsService.Execute(Id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }

                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return RedirectToAction(actionName: "Index");
        }

        public IActionResult DeleteComments(int Id)
        {

            var result = _pageFacad.DeletePageCommentsService.Execute(Id);

            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }

                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }

            return RedirectToAction(actionName: "Index");
        }
    }
}
