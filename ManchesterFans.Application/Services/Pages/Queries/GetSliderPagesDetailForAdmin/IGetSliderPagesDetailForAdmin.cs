using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetSliderPagesDetailForAdmin
{
    public interface IGetSliderPagesDetailForAdmin
    {
        ResultDto<SliderPagesDetailForAdminDto> Execute(int PageId);
    }

    public class GetSliderPagesDetailForAdmin : IGetSliderPagesDetailForAdmin
    {
        private readonly IDataBaseContext _context;

        public GetSliderPagesDetailForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<SliderPagesDetailForAdminDto> Execute(int PageId)
        {
            try
            {

                var page = _context.SliderPosts
                    .Include(sp => sp.Page)
                    .FirstOrDefault(sp => sp.Page.PageId == PageId);

                if (page == null)
                {
                    return new ResultDto<SliderPagesDetailForAdminDto>()
                    {
                         IsSuccess = false,
                         Message = "n"
                    };
                }

                SliderPagesDetailForAdminDto Data = new SliderPagesDetailForAdminDto()
                {
                    PageId = page.Page.PageId,
                    GroupName = page.Page.PageId,
                    ImageName = page.Page.ImageName,
                    InsertTime = page.Page.InsertTime,
                    Likes = page.Page.Likes,
                    Tags = page.Page.Tags,
                    ShortDescribtion = page.Page.ShortDescribtion,
                    Text = page.Page.Text,
                    Title = page.Page.Title,
                    Visits = page.Page.Visits,
                };

                return new ResultDto<SliderPagesDetailForAdminDto>()
                {
                    Data = Data,
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                return new ResultDto<SliderPagesDetailForAdminDto>()
                {
                    Message = "مشکلی پیش آمد",
                    IsSuccess = false
                };
                throw;
            }
        }
    }

    public class SliderPagesDetailForAdminDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
        public int Visits { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public int GroupName { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
