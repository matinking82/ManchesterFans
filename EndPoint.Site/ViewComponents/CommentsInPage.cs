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
    public class CommentsInPage:ViewComponent
    {
        private readonly IPageFacad _pageFacad;
        public CommentsInPage(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }
        public IViewComponentResult Invoke(int id)
        {
            var result = _pageFacad.GetPageCommentsForSitePageSevice.Execute(id);

            return View(viewName: "CommentsInPage",
                model: result.Data
                .Select(p=> new PageCommentsViewModel()
                {
                    Comment = p.Comment,
                    CommentId = p.CommentId,
                    Date = p.Date.ToShamsi(),
                    ImageName = p.ImageName,
                    Name = p.Name,
                    ReplyComments = p.ReplyComments
                    .Select(r=> new PageCommentsViewModel()
                    {
                        Comment = r.Comment,
                        CommentId = r.CommentId,
                        Date = r.Date.ToShamsi(),
                        ImageName = r.ImageName,
                        Name = r.Name,
                    }),
                }));
        }
    }
}
