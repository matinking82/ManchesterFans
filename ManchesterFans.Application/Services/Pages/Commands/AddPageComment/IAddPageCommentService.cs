using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.AddPageComment
{
    public interface IAddPageCommentService
    {
        ResultDto Execute(RequestAddPageCommentDto request);
    }

    public class AddPageCommentService : IAddPageCommentService
    {
        private readonly IDataBaseContext _context;

        public AddPageCommentService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddPageCommentDto request)
        {

            try
            {
                var page = _context.Pages.Find(request.PageId);
                var user = _context.Users.Find(request.UserId);

                var parent = _context.PageComments.Find(request.Reply);


                if (page == null || user == null || (parent == null && request.Reply != 0))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                PageComments pageComments = new PageComments()
                {
                    Comment = request.Comment,
                    InsertTime = DateTime.Now,
                    PageId = request.PageId,
                    Reply = request.Reply,
                    UserId = request.UserId,
                    Page = page,
                    User = user,
                    Parent = parent,
                };

                _context.PageComments.Add(pageComments);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                };

            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

        }
    }

    public class RequestAddPageCommentDto
    {
        public string Comment { get; set; }
        public int Reply { get; set; }
        public int PageId { get; set; }
        public int UserId { get; set; }
    }
}
