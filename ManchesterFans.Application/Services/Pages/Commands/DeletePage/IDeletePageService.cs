using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.DeletePage
{
    public interface IDeletePageService
    {
        ResultDto Execute(int PageId);
    }
    public class DeletePageService : IDeletePageService
    {
        private readonly IDataBaseContext _context;
        public DeletePageService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int PageId)
        {
            Page page;
            try
            {
                page = _context.Pages.Find(PageId);
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
                    Message = "صفحه یافت نشد"
                };
            }


            page.IsRemoved = true;
            page.RemoveTime = DateTime.Now;

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
}
