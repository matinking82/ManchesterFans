using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.DeletePageComments
{
    public interface IDeletePageCommentsService
    {
        ResultDto Execute(int Id);
    }

    public class DeletePageCommentsService : IDeletePageCommentsService
    {
        private readonly IDataBaseContext _context;
        public DeletePageCommentsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int Id)
        {
            try
            {
                var Comment = _context.PageComments.Find(Id);

                if (Comment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                Comment.IsRemoved = false;
                Comment.RemoveTime = DateTime.Now;

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
