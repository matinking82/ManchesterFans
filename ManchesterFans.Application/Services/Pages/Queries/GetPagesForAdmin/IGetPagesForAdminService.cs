using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ManchesterFans.Domain.Entities.Pages;
using Microsoft.EntityFrameworkCore;

namespace ManchesterFans.Application.Services.Pages.Queries.GetPagesForAdmin
{
    public interface IGetPagesForAdminService
    {
        ResultDto<IEnumerable<PagesForAdminDto>> Execute(RequestPagesForAdminDto request);
    }
    public class GetPagesForAdminService : IGetPagesForAdminService
    {
        IDataBaseContext _context;
        public GetPagesForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<PagesForAdminDto>> Execute(RequestPagesForAdminDto request)
        {
            IEnumerable<Page> Pages;
            try
            {
                Pages = _context.Pages
                    .Include(p=>p.PageGroup)
                    .ToPaged(request.Page, request.PageSize, out int rowCount);

            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<PagesForAdminDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

            IEnumerable<PagesForAdminDto> Data = Pages.Select(p => new PagesForAdminDto()
            {
                CreateDate = p.InsertTime,
                ImageName = p.ImageName,
                Likes = p.Likes,
                PageId = p.PageId,
                ShortDescribtion = p.ShortDescribtion,
                Title = p.Title,
                Group = p.PageGroup.GroupName,
                Visits = p.Visits
            }).ToList();

            return new ResultDto<IEnumerable<PagesForAdminDto>>()
            {
                Data = Data,
                IsSuccess = true
            };
        }
    }

    public class RequestPagesForAdminDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PagesForAdminDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string ImageName { get; set; }
        public string Group { get; set; }
        public DateTime CreateDate { get; set; }
        public int Visits { get; set; }
        public int Likes { get; set; }
    }

}
