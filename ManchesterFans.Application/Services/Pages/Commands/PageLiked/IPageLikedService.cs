using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.PageLiked
{
    public interface IPageLikedService
    {
        ResultDto Execute(RequestPageLikedDto request);
    }

    public class PageLikedService : IPageLikedService
    {
        private readonly IDataBaseContext _context;
        public PageLikedService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestPageLikedDto request)
        {
            try
            {
                if (!_context.PageLikes.Any(l => l.PageId == request.PageId && l.UserId == request.UserId))
                {
                    var user = _context.Users.Find(request.UserId);
                    var page = _context.Pages.Find(request.PageId);

                    if (page == null || user == null)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "n"
                        };
                    }
                    PageLikes pageLikes = new PageLikes()
                    {
                        InsertTime = DateTime.Now,
                        Page = page,
                        PageId = request.PageId,
                        User = user,
                        UserId = request.UserId,
                    };

                    _context.PageLikes.Add(pageLikes);

                    page.Likes++;

                    _context.SaveChanges();
                }

                return new ResultDto()
                {
                    IsSuccess = true
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

    public class RequestPageLikedDto
    {
        public int PageId { get; set; }
        public int UserId { get; set; }
    }
}
