using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Common.Utilities;
using ManchesterFans.Domain.Entities.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetPagesForSliderSelect
{
    public interface IGetPagesListForSliderSelectService
    {
        ResultDto<IEnumerable<PagesListForSliderSelectDto>> Execute(RequestGetPagesListForSliderSelect request);
    }

    public class GetPagesListForSliderSelectService : IGetPagesListForSliderSelectService
    {
        private readonly IDataBaseContext _context;

        public GetPagesListForSliderSelectService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<IEnumerable<PagesListForSliderSelectDto>> Execute(RequestGetPagesListForSliderSelect request)
        {
            try
            {
                IQueryable<Page> pages = _context.Pages
                    .Include(p => p.PageGroup);


                if (request.Search != null)
                {
                    pages = pages
                    .Where(p => p.Text.ToLower().Contains(request.Search.ToLower()) || p.ShortDescribtion.ToLower().Contains(request.Search.ToLower()));
                }


                return new ResultDto<IEnumerable<PagesListForSliderSelectDto>>()
                {
                    Data = pages
                    .ToPaged(request.Page, request.PageSize, out int Rows)
                    .Select(p => new PagesListForSliderSelectDto()
                    {
                        GroupName = p.PageGroup.GroupName,
                        ImageName = p.ImageName,
                        PageId = p.PageId,
                        Rows = Rows,
                        ShortDescribtion = p.ShortDescribtion,
                        Title = p.Title
                    }).ToList(),

                    IsSuccess = true,

                };
            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<PagesListForSliderSelectDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
                throw;
            }
        }
    }

    public class RequestGetPagesListForSliderSelect
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PagesListForSliderSelectDto
    {
        public int PageId { get; set; }
        public string ImageName { get; set; }
        public string ShortDescribtion { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }
        public int Rows { get; set; }
    }
}
