using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ManchesterFans.Application.Services.Pages.Queries.GetRecentPagesForSite
{
    public interface IGetRecentPagesForSiteService
    {
        ResultDto<IEnumerable<RecentPagesForSiteDto>> Execute(int Take);
    }

    public class GetRecentPagesForSiteService : IGetRecentPagesForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetRecentPagesForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<RecentPagesForSiteDto>> Execute(int Take)
        {
            try
            {
                var Pages = _context.Pages
                    .Include(p => p.PageGroup)
                    .OrderByDescending(p => p.InsertTime)
                    .Take(Take);

                return new ResultDto<IEnumerable<RecentPagesForSiteDto>>()
                {
                    Data = Pages.ToList().Select(p => new RecentPagesForSiteDto()
                    {
                        CreateDate = p.InsertTime,
                        GroupName = p.PageGroup.GroupName,
                        Likes = p.Likes,
                        PageId = p.PageId,
                        ShortDescribtion = p.ShortDescribtion,
                        Title = p.Title,
                        Visits = p.Visits,
                        ImageName = p.ImageName,
                        GroupId = p.GroupId
                    }).ToList(),
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<RecentPagesForSiteDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class RecentPagesForSiteDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }
        public string ShortDescribtion { get; set; }
        public int Likes { get; set; }
        public int Visits { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageName { get; set; }
        public int GroupId { get; set; }
    }
}
