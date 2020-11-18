using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Queries.GetRecentPagesForSiteFooter
{
    public interface IGetRecentPagesForSiteFooterService
    {
        ResultDto<IEnumerable<RecentPagesForSiteFooterDto>> Execute();
    }

    public class GetRecentPagesForSiteFooterService : IGetRecentPagesForSiteFooterService
    {
        private readonly IDataBaseContext _context;
        public GetRecentPagesForSiteFooterService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<RecentPagesForSiteFooterDto>> Execute()
        {
            try
            {
                var Pages = _context.Pages
                    .OrderByDescending(p => p.PageId)
                    .Take(3);

                return new ResultDto<IEnumerable<RecentPagesForSiteFooterDto>>()
                {
                    Data = Pages.Select(p=> new RecentPagesForSiteFooterDto()
                    {
                         Date = p.InsertTime,
                         ImageName = p.ImageName,
                         PageId = p.PageId,
                         Title = p.Title,
                    }),
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<RecentPagesForSiteFooterDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class RecentPagesForSiteFooterDto
    {
        public int PageId { get; set; }
        public string ImageName { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}
