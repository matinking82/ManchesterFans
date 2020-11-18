using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.RemovePageFromSlider
{
    public interface IRemovePageFromSliderService
    {
        ResultDto Execute(int PageId);
    }

    public class RemovePageFromSliderService : IRemovePageFromSliderService
    {
        private readonly IDataBaseContext _context;
        public RemovePageFromSliderService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int PageId)
        {
            try
            {
                var page = _context.SliderPosts
                    .Include(sp => sp.Page)
                    .FirstOrDefault(sp => sp.Page.PageId == PageId);

                if (page==null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }


                page.IsRemoved = true;
                page.RemoveTime = DateTime.Now;

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
                throw;
            }
        }
    }
}
