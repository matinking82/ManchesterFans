using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetPagesForSearch
{
    public interface IGetPagesForSearchService
    {
        ResultDto<IEnumerable<PagesForSearchServiceDto>> Execute(RequestGetPagesForSearchServiceDto request);
    }

    public class GetPagesForSearchService : IGetPagesForSearchService
    {
        private readonly IDataBaseContext _context;
        public GetPagesForSearchService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<PagesForSearchServiceDto>> Execute(RequestGetPagesForSearchServiceDto request)
        {
            try
            {
                var Pages = _context.Pages
                    .Include(p => p.PageGroup)
                    .Where(p => p.Title.ToLower().Contains(request.Key.ToLower()) || p.ShortDescribtion.ToLower().Contains(request.Key.ToLower()) || p.Tags.ToLower().Contains(request.Key.ToLower()))
                    .ToPaged(request.Page, request.PageSize, out int Rows);

                return new ResultDto<IEnumerable<PagesForSearchServiceDto>>()
                {
                    Data = Pages.Select(p => new PagesForSearchServiceDto()
                    {
                        CreateDate = p.InsertTime,
                        GroupName = p.PageGroup.GroupName,
                        ImageName = p.ImageName,
                        Likes = p.Likes,
                        PageId = p.PageId,
                        ShortDescribtion = p.ShortDescribtion,
                        Title =p.Title,
                        Visits = p.Visits,
                    }),
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<PagesForSearchServiceDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class RequestGetPagesForSearchServiceDto
    {
        public string Key { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PagesForSearchServiceDto
    {
        public int PageId { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public int Visits { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShortDescribtion { get; set; }
    }
}
