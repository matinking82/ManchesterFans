using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManchesterFans.Application.Services.Site.Queries.GetSiteNameForAdmin
{
    public interface IGetSiteNameForAdminService
    {
        ResultDto<SiteNameForAdminDto> Execute();
    }

    public class GetSiteNameForAdminService : IGetSiteNameForAdminService
    {
        IDataBaseContext _context;
        public GetSiteNameForAdminService(IDataBaseContext context)
        {
            _context = context;

        }
        public ResultDto<SiteNameForAdminDto> Execute()
        {
            Header header;

            try
            {
                header = _context.Headers.FirstOrDefault();
            }
            catch (Exception)
            {
                return new ResultDto<SiteNameForAdminDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

            SiteNameForAdminDto Data = new SiteNameForAdminDto()
            {
                SiteName = header.SiteName
            };

            return new ResultDto<SiteNameForAdminDto>()
            {
                Data = Data,
                IsSuccess = true
            };

        }
    }

    public class SiteNameForAdminDto
    {
        public string SiteName { get; set; }
    }
}
