using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForEdit
{
    public interface IGetSinglePageForEditService
    {
        ResultDto<SinglePageForEditDto> Execute(RequestGetSinglePageForEditDto request);
    }

    public class GetSinglePageForEditService : IGetSinglePageForEditService
    {
        private readonly IDataBaseContext _context;
        public GetSinglePageForEditService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<SinglePageForEditDto> Execute(RequestGetSinglePageForEditDto request)
        {
            Page page;
            try
            {
                page = _context.Pages.Find(request.PageId);
            }
            catch (Exception)
            {
                return new ResultDto<SinglePageForEditDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
            if (page==null)
            {
                return new ResultDto<SinglePageForEditDto>()
                {
                    Message = "صفحه یافت نشد",
                    IsSuccess = false
                };
            }

            SinglePageForEditDto Data = new SinglePageForEditDto()
            {
                GroupId = page.GroupId,
                PageId = page.PageId,
                ShortDescribtion = page.ShortDescribtion,
                Tags = page.Tags,
                Text = page.Text,
                Title = page.Title,
                ImageName = page.ImageName
            };

            return new ResultDto<SinglePageForEditDto>()
            {
                Data = Data,
                IsSuccess = true,
            };

        }
    }

    public class RequestGetSinglePageForEditDto
    {
        public int PageId { get; set; }
    }

    public class SinglePageForEditDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
        public int GroupId { get; set; }
        public string ImageName { get; set; }
    }
}
