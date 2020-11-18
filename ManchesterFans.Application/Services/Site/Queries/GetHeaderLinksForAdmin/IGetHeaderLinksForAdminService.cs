using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Queries.GetHeaderLinksForAdmin
{
    public interface IGetHeaderLinksForAdminService
    {
        ResultDto<IEnumerable<HeaderLinksForAdminDto>> Execute();
    }

    public class GetHeaderLinksForAdminService : IGetHeaderLinksForAdminService
    {
        IDataBaseContext _context;
        public GetHeaderLinksForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<IEnumerable<HeaderLinksForAdminDto>> Execute()
        {
            try
            {
                IEnumerable<HeaderLinksForAdminDto> Data = _context.HeaderLinks.Select(hl => new HeaderLinksForAdminDto()
                {
                    DisplayText = hl.DisplayText,
                    LinkId = hl.LinkId,
                    Url = hl.Url
                }).ToList();

                return new ResultDto<IEnumerable<HeaderLinksForAdminDto>>()
                {
                    Data = Data,
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<HeaderLinksForAdminDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

        }
    }

    public class HeaderLinksForAdminDto
    {
        public int LinkId { get; set; }
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
