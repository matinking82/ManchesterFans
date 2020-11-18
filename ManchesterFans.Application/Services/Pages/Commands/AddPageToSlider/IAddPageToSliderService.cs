using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.AddPageToSlider
{
    public interface IAddPageToSliderService
    {
        ResultDto Execute(int PageId);
    }

    public class AddPageToSliderService : IAddPageToSliderService
    {
        private readonly IDataBaseContext _context;
        public AddPageToSliderService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int PageId)
        {

            try
            {
                var page = _context.Pages.Find(PageId);

                if (page==null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                SliderPosts slider = new SliderPosts()
                {
                    InsertTime = DateTime.Now,
                    PageID = PageId,
                    Page = page
                };

                _context.SliderPosts.Add(slider);

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
