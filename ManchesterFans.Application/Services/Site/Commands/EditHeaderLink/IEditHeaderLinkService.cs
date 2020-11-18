using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Commands.EditHeaderLink
{
    public interface IEditHeaderLinkService
    {
        ResultDto Execute(RequestEditHeaderLink request);
    }

    public partial class EditHeaderLinkService : IEditHeaderLinkService
    {
        private readonly IDataBaseContext _context;
        public EditHeaderLinkService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditHeaderLink request)
        {
            try
            {
                var headerlink = _context.HeaderLinks.Find(request.LinkId);
                if (headerlink == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                headerlink.DisplayText = request.DisplayText;
                headerlink.Url = request.Url;
                headerlink.UpdateTime = DateTime.Now;

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

    public class RequestEditHeaderLink
    {
        public int LinkId { get; set; }
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
