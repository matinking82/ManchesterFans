using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.ChangeUsername
{
    public interface IChangeUsernameService
    {
        ResultDto Execute(RequestChangeUsername request);
    }

    public class ChangeUsernameService : IChangeUsernameService
    {
        private readonly IDataBaseContext _context;
        public ChangeUsernameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestChangeUsername request)
        {
            try
            {

                if (_context.Users.Any(u => u.Username == request.Username))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نام کاربری وارد شده قبلا ثبت شده است"
                    };
                }


                var user = _context.Users.Find(request.UserId);
                if (user == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                user.Username = request.Username;

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
            }
        }
    }

    public class RequestChangeUsername
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
