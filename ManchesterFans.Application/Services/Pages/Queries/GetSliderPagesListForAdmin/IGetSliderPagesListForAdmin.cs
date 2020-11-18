using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetSliderPagesListForAdmin
{
    public interface IGetSliderPagesListForAdmin
    {
        ResultDto<IEnumerable<SliderPagesListForAdminDto>> Execute();
    }

    public class GetSliderPagesListForAdmin : IGetSliderPagesListForAdmin
    {
        private readonly IDataBaseContext _context;

        public GetSliderPagesListForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<IEnumerable<SliderPagesListForAdminDto>> Execute()
        {
            try
            {
                IEnumerable<SliderPagesListForAdminDto> pages = 
                    _context.SliderPosts
                    .Include(sp => sp.Page)
                    .ThenInclude(p => p.PageGroup)
                    .Select(sp=> new SliderPagesListForAdminDto()
                    {
                        GroupName = sp.Page.PageGroup.GroupName,
                        ImageName= sp.Page.ImageName,
                        Likes = sp.Page.Likes,
                        PageId = sp.Page.PageId,
                        ShortDescribtion= sp.Page.ShortDescribtion,
                        Title= sp.Page.Title,
                        Visits = sp.Page.Visits
                    });

                return new ResultDto<IEnumerable<SliderPagesListForAdminDto>>()
                {
                    Data= pages,
                    IsSuccess = true,
                };

            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<SliderPagesListForAdminDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
                throw;
            }
        }
    }

    public class SliderPagesListForAdminDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string ImageName { get; set; }
        public int Visits { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public string GroupName { get; set; }
    }
}
