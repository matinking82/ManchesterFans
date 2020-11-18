using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Commands.DeleteHeaderLinkService
{
    public interface IDeleteHeaderLinkService
    {
        ResultDto Execute(int Id);
    }

    public class DeleteHeaderLinkService : IDeleteHeaderLinkService
    {
        IDataBaseContext _context;
        public DeleteHeaderLinkService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int Id)
        {
            try
            {
                var HeaderLink = _context.HeaderLinks.Find(Id);

                if (HeaderLink == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                HeaderLink.IsRemoved = true;
                HeaderLink.RemoveTime = DateTime.Now;

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
}
