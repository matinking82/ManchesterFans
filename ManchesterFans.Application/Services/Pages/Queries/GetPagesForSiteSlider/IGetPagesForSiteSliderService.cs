using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetPagesForSiteSlider
{
    public interface IGetPagesForSiteSliderService
    {
        ResultDto<IEnumerable<PagesForSiteSliderDto>> Execute();
    }

    public class GetPagesForSiteSliderService : IGetPagesForSiteSliderService
    {
        private readonly IDataBaseContext _context;

        public GetPagesForSiteSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<IEnumerable<PagesForSiteSliderDto>> Execute()
        {
            try
            {
                var sliders = _context.SliderPosts
                    .Include(sp => sp.Page)
                    .ThenInclude(p => p.PageGroup);


                return new ResultDto<IEnumerable<PagesForSiteSliderDto>>()
                {
                    Data = sliders.Select(sp => new PagesForSiteSliderDto()
                    {
                        Date = sp.Page.InsertTime,
                        GroupName = sp.Page.PageGroup.GroupName,
                        ImageName = sp.Page.ImageName,
                        Likes = sp.Page.Likes,
                        Title = sp.Page.Title,
                        Visits = sp.Page.Visits,
                    }),
                    IsSuccess = true,

                };
            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<PagesForSiteSliderDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class PagesForSiteSliderDto
    {
        public string ImageName { get; set; }
        public string GroupName { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }
        public int Visits { get; set; }
    }
}
