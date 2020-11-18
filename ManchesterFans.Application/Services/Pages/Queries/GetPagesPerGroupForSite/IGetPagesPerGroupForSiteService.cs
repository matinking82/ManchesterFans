using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetPagesPerGroupForSite
{
    public interface IGetPagesPerGroupForSiteService
    {
        ResultDto<IEnumerable<PagesPerGroupForSiteDto>> Execute(int groupId, int Page, int PageSize);
    }

    public class GetPagesPerGroupForSiteService : IGetPagesPerGroupForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetPagesPerGroupForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<IEnumerable<PagesPerGroupForSiteDto>> Execute(int groupId,int Page,int PageSize)
        {
            try
            {
                var Group = _context.PageGroups
                    .Include(g => g.Pages)
                    .FirstOrDefault(g => g.GroupId == groupId);

                if (Group==null)
                {
                    return new ResultDto<IEnumerable<PagesPerGroupForSiteDto>>()
                    {
                        IsSuccess =false,
                        Message = "n"
                    };
                }


                return new ResultDto<IEnumerable<PagesPerGroupForSiteDto>>()
                {
                    Data = Group.Pages
                    .ToPaged(Page,PageSize,out int Rows)
                    .Select(p=> new PagesPerGroupForSiteDto()
                    {
                        CreateDate = p.InsertTime,
                        GroupId = p.GroupId,
                        GroupName = Group.GroupName,
                        ImageName = p.ImageName,
                        Likes = p.Likes,
                        PageId = p.PageId,
                        ShortDescribtion = p.ShortDescribtion,
                        Title = p.Title,
                        Visits = p.Visits
                    }),
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<PagesPerGroupForSiteDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class PagesPerGroupForSiteDto
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
