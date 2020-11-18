using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetPageCommentsForSitePage
{
    public interface IGetPageCommentsForSitePageSevice
    {
        ResultDto<IEnumerable<PageCommentsForSitePageDto>> Execute(int pageId);
    }


    public class GetPageCommentsForSitePageSevice : IGetPageCommentsForSitePageSevice
    {
        private readonly IDataBaseContext _context;
        public GetPageCommentsForSitePageSevice(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<PageCommentsForSitePageDto>> Execute(int pageId)
        {
            try
            {
                var PageComments = _context.PageComments
                    .Include(pc => pc.RepllyComments)
                    .Include(pc => pc.User)
                    .Where(pc => pc.PageId == pageId && pc.Parent == null && pc.IsAccepted);


                return new ResultDto<IEnumerable<PageCommentsForSitePageDto>>()
                {
                    Data = PageComments.Select(p => new PageCommentsForSitePageDto()
                    {
                        Comment = p.Comment,
                        CommentId = p.CommentId,
                        Date = p.InsertTime,
                        ImageName = p.User.image,
                        Name = p.User.Username,
                        ReplyComments = p.RepllyComments
                        .Select(r => new PageCommentsForSitePageDto()
                        {
                            Comment = r.Comment,
                            CommentId = r.CommentId,
                            Date = r.InsertTime,
                            ImageName = r.User.image,
                            Name = r.User.Username,
                        }),
                    }),
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<PageCommentsForSitePageDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class PageCommentsForSitePageDto
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<PageCommentsForSitePageDto> ReplyComments { get; set; }
    }
}
