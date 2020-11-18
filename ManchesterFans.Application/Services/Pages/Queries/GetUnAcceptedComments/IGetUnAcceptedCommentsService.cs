using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetUnAcceptedComments
{
    public interface IGetUnAcceptedCommentsService
    {
        ResultDto<IEnumerable<UnAcceptedCommentsDto>> Execute(RequestGetUnAcceptedCommentsDto request);
    }

    public class GetUnAcceptedCommentsService : IGetUnAcceptedCommentsService
    {
        private readonly IDataBaseContext _context;
        public GetUnAcceptedCommentsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<UnAcceptedCommentsDto>> Execute(RequestGetUnAcceptedCommentsDto request)
        {
            try
            {
                var Comments = _context.PageComments
                    .Include(c => c.Page)
                    .Where(c => !c.IsAccepted);


                return new ResultDto<IEnumerable<UnAcceptedCommentsDto>>()
                {
                    Data = Comments.Select(c=> new UnAcceptedCommentsDto()
                    {
                        Comment = c.Comment,
                        CommentId = c.CommentId,
                        PageId = c.PageId,
                        PageImage = c.Page.ImageName,
                        PageTitle = c.Page.Title,
                        InsertTime = c.InsertTime
                    }),
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<UnAcceptedCommentsDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد!!"
                };
            }
        }
    }

    public class UnAcceptedCommentsDto
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public string PageImage { get; set; }
        public string PageTitle { get; set; }
        public int PageId { get; set; }
        public DateTime InsertTime { get; set; }
    }

    public class RequestGetUnAcceptedCommentsDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
