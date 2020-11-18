using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForSite
{
    public interface IGetSinglePageForSiteService
    {
        ResultDto<SinglePageForSiteDto> Execute(int Id);
    }
    public class GetSinglePageForSiteService : IGetSinglePageForSiteService
    {
        IDataBaseContext _context;
        public GetSinglePageForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<SinglePageForSiteDto> Execute(int Id)
        {
            Page page;

            try
            {
                page = _context.Pages
                    .Include(p => p.PageGroup)
                    .FirstOrDefault(p => p.PageId == Id);
            }
            catch (Exception)
            {
                return new ResultDto<SinglePageForSiteDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

            if (page == null)
            {
                return new ResultDto<SinglePageForSiteDto>()
                {
                    Message = "n",
                    IsSuccess = false
                };
            }

            SinglePageForSiteDto Data = new SinglePageForSiteDto()
            {
                CreateDate = page.InsertTime,
                GroupId = page.GroupId,
                GroupName = page.PageGroup.GroupName,
                ImageName = page.ImageName,
                Likes = page.Likes,
                Tags = (page.Tags != null) ? page.Tags.Split(",") : null,
                Text = page.Text,
                Title = page.Title,
                Visits = page.Visits,
                PageId = page.PageId
            };


            return new ResultDto<SinglePageForSiteDto>()
            {
                Data = Data,
                IsSuccess = true
            };

        }
    }
    public class SinglePageForSiteDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int Visits { get; set; }
        public int Likes { get; set; }
        public DateTime CreateDate { get; set; }

        public string GroupName { get; set; }
        public int GroupId { get; set; }
    }
}
