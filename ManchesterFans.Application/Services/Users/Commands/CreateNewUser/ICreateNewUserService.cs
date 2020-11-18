using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Common.Utilities;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.CreateNewUser
{
    public interface ICreateNewUserService
    {
        ResultDto Execute(RequestCreateNewUserDto request);
    }

    public class CreateNewUserService : ICreateNewUserService
    {
        private readonly IDataBaseContext _context;

        public CreateNewUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestCreateNewUserDto request)
        {
            User user = new User()
            {
                InsertTime = DateTime.Now,
                Level = request.Level,
                NickName = request.NickName,
                Password = request.Password.ToSHA256(),
                Username = request.Username,
            };

            try
            {

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

    public class RequestCreateNewUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
    }
}
