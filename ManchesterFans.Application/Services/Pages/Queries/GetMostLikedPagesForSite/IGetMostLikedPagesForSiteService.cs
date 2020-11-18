using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetMostLikedPagesForSite
{
    public interface IGetMostLikedPagesForSiteService
    {
        ResultDto<IEnumerable<MostLikedPagesForSiteDto>> Execute(int Take);
    }

    public class GetMostLikedPagesForSiteService : IGetMostLikedPagesForSiteService
    {
        IDataBaseContext _context;
        public GetMostLikedPagesForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<MostLikedPagesForSiteDto>> Execute(int Take)
        {
            try
            {
                var Pages = _context.Pages
                    .Include(p => p.PageGroup)
                    .OrderByDescending(p => p.Likes)
                    .Take(Take);

                return new ResultDto<IEnumerable<MostLikedPagesForSiteDto>>()
                {
                    Data = Pages.ToList().Select(p => new MostLikedPagesForSiteDto()
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
                return new ResultDto<IEnumerable<MostLikedPagesForSiteDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class MostLikedPagesForSiteDto
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
