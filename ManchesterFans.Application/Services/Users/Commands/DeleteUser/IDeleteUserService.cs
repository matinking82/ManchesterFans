using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.DeleteUser
{
    public interface IDeleteUserService
    {
        ResultDto Execute(int Id);
    }

    public class DeleteUserService : IDeleteUserService
    {
        private readonly IDataBaseContext _context;
        public DeleteUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int Id)
        {

            try
            {
                User user = _context.Users.Find(Id);

                if (user == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                user.IsRemoved = true;
                user.RemoveTime = DateTime.Now;

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
                throw;
            }

        }
    }
}
