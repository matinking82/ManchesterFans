using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForAdmin
{
    public interface IGetSinglePageForAdminService
    {
        ResultDto<SinglePageForAdminDto> Execute(RequestSinglePageForAdminDto request);
    }

    public class GetSinglePageForAdminService : IGetSinglePageForAdminService
    {
        IDataBaseContext _context;
        public GetSinglePageForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<SinglePageForAdminDto> Execute(RequestSinglePageForAdminDto request)
        {
            Page Page;
            try
            {
                Page = _context.Pages
                    .Include(p => p.PageGroup)
                    .SingleOrDefault(p => p.PageId == request.PageId);

            }
            catch (Exception)
            {
                return new ResultDto<SinglePageForAdminDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

            if (Page == null)
            {
                return new ResultDto<SinglePageForAdminDto>()
                {
                    IsSuccess = false,
                    Message = "صفحه ای یافت نشد"
                };
            }

            SinglePageForAdminDto Data = new SinglePageForAdminDto()
            {
                CreateDate = Page.InsertTime,
                ImageName = Page.ImageName,
                Likes = Page.Likes,
                PageId = Page.PageId,
                ShortDescribtion = Page.ShortDescribtion,
                Tags = Page.Tags,
                Text = Page.Text,
                Title = Page.Title,
                Visits = Page.Visits,
                Group = Page.PageGroup.GroupName
            };

            return new ResultDto<SinglePageForAdminDto>()
            {
                Data = Data,
                IsSuccess = true
            };
        }
    }

    public class RequestSinglePageForAdminDto
    {
        public int PageId { get; set; }
    }

    public class SinglePageForAdminDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string Group { get; set; }
        public DateTime CreateDate { get; set; }
        public string Tags { get; set; }
        public int Visits { get; set; }
        public int Likes { get; set; }
    }
}
