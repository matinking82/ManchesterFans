using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.UserSignup
{
    public interface IUserSignupService
    {
        ResultDto Execute(RequestUserSignupDto request);
    }

    public class UserSignupService : IUserSignupService
    {
        private readonly IDataBaseContext _context;
        public UserSignupService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestUserSignupDto request)
        {
            try
            {
                if (_context.Users.Any(u=> u.Username==request.Username))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نام کاربری قبلا ثبت شده است"
                    };
                }


                User user = new User()
                {
                    image = "Default.png",
                    InsertTime = DateTime.Now,
                    Level = 1,
                    Password = request.Password,
                    Username = request.Username
                };

                _context.Users.Add(user);

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

    public class RequestUserSignupDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
