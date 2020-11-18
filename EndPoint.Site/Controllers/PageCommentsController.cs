using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EndPoint.Site.ViewModels.SiteViewModels.Page;
using ManchesterFans.Application.FacadPatterns;
using ManchesterFans.Application.Services.Pages.Commands.AddPageComment;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class PageCommentsController : Controller
    {
        private readonly IPageFacad _pageFacad;
        public PageCommentsController(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }

        [HttpPost]
        public IActionResult AddPageComment([Bind("Comment,Reply,PageId")] AddPageCommentsViewModel pageComments)
        {
            //TODO: ModelState Returns False (XD)
            if (pageComments.PageId != 0 && pageComments.Comment != null)
            {
                RequestAddPageCommentDto request = new RequestAddPageCommentDto()
                {
                    Comment = pageComments.Comment,
                    PageId = pageComments.PageId,
                    Reply = pageComments.Reply,
                    UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
                };
                var result = _pageFacad.AddPageCommentService.Execute(request);

                if (!result.IsSuccess)
                {
                    if (result.Message == "n")
                    {
                        return NotFound();
                    }

                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }

                return Redirect($"/Page/{request.PageId}");

            }
            return NotFound();
        }
    }
}
