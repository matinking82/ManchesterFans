using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.PageVisited
{
    public interface IPageVisitedService
    {
        ResultDto Execute(RequestPageVisited request);
    }

    public class PageVisitedService : IPageVisitedService
    {
        IDataBaseContext _context;
        public PageVisitedService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestPageVisited request)
        {
            Page page;

            try
            {
                page = _context.Pages.Find(request.PageId);
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

            if (page == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "n"
                };
            }

            page.Visits++;
            try
            {
                _context.SaveChanges();

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

    public class RequestPageVisited
    {
        public int PageId { get; set; }
    }
}
