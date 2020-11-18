using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Queries.GetSiteHeaderForSite
{
    public interface IGetSiteHeaderForSiteService
    {
        ResultDto<SiteHeaderDto> Execute();
    }

    public class GetSiteHeaderForSiteService : IGetSiteHeaderForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetSiteHeaderForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<SiteHeaderDto> Execute()
        {
            try
            {
                var Header = _context.Headers
                    .Include(h => h.Links)
                    .FirstOrDefault();


                return new ResultDto<SiteHeaderDto>()
                {
                    Data = new SiteHeaderDto()
                    {
                        SiteName = Header.SiteName,
                        Links = Header.Links.Select(l => new LinkDto()
                        {
                            DisplayText = l.DisplayText,
                            Url = l.Url
                        }).ToList(),
                    },
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ResultDto<SiteHeaderDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
                throw;
            }
        }
    }

    public class SiteHeaderDto
    {
        public string SiteName { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }

    public class LinkDto
    {
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
