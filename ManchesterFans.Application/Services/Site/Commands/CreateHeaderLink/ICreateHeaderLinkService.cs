using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Commands.CreateHeaderLink
{
    public interface ICreateHeaderLinkService
    {
        ResultDto Execute(RequestCreateHeaderLink request);
    }

    public class CreateHeaderLinkService : ICreateHeaderLinkService
    {
        IDataBaseContext _context;
        public CreateHeaderLinkService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestCreateHeaderLink request)
        {

            HeaderLinks link = new HeaderLinks
            {
                DisplayText = request.DisplayText,
                Url = request.Url,
                Id = 1,
                InsertTime = DateTime.Now,
                Header = _context.Headers.Find(1)
            };

            try
            {
                _context.HeaderLinks.Add(link);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

        }
    }

    public class RequestCreateHeaderLink
    {
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
