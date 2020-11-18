using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManchesterFans.Application.Services.Site.Commands.EditSiteName
{
    public interface IEditSiteNameService
    {
        ResultDto Execute(RequestEditSiteNameDto request);
    }
    public class EditSiteNameService : IEditSiteNameService
    {
        IDataBaseContext _context;
        public EditSiteNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditSiteNameDto request)
        {
            Header header;

            try
            {
                header = _context.Headers.FirstOrDefault();
                header.SiteName = request.SiteName;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                };

            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
                throw;
            }

        }
    }

    public class RequestEditSiteNameDto
    {
        public string SiteName { get; set; }
    }
}
