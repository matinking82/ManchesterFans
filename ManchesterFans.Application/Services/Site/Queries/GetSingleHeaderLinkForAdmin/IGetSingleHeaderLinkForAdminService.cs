using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Queries.GetSingleHeaderLinkForAdmin
{
    public interface IGetSingleHeaderLinkForAdminService
    {
        ResultDto<SingleHeaderLinkForAdminDto> Execute(int Id);
    }

    public class GetSingleHeaderLinkForAdminService : IGetSingleHeaderLinkForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetSingleHeaderLinkForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<SingleHeaderLinkForAdminDto> Execute(int Id)
        {
            try
            {
                var HeaderLink = _context.HeaderLinks.Find(Id);
                if (HeaderLink == null)
                {
                    return new ResultDto<SingleHeaderLinkForAdminDto>()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                return new ResultDto<SingleHeaderLinkForAdminDto>()
                {
                    Data = new SingleHeaderLinkForAdminDto()
                    {
                        DisplayText = HeaderLink.DisplayText,
                        LinkId = HeaderLink.LinkId,
                        Url = HeaderLink.Url
                    },
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                return new ResultDto<SingleHeaderLinkForAdminDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class SingleHeaderLinkForAdminDto
    {
        public int LinkId { get; set; }
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
