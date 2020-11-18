using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.CheckUserExist
{
    public interface ICheckUserExistService
    {
        ResultDto<ResultCheckUserExistDto> Execute(RequestCheckUserExistDto request);
    }

    public class CheckUserExistService : ICheckUserExistService
    {
        private readonly IDataBaseContext _context;
        public CheckUserExistService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultCheckUserExistDto> Execute(RequestCheckUserExistDto request)
        {
            try
            {

                var user = _context.Users
                    .FirstOrDefault(u => u.Username.ToLower() == request.Username.ToLower() && u.Password == request.Password.ToSHA256());


                if (user==null)
                {
                    return new ResultDto<ResultCheckUserExistDto>()
                    {
                        IsSuccess = true,
                        Data = new ResultCheckUserExistDto()
                        {
                            IsExist = false
                        }
                    };
                }

                return new ResultDto<ResultCheckUserExistDto>()
                {
                    IsSuccess = true,
                    Data = new ResultCheckUserExistDto()
                    {
                        IsExist = true,
                        UserId = user.LoginId,
                        Level = user.Level
                    }
                };


            }
            catch (Exception)
            {
                return new ResultDto<ResultCheckUserExistDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
                throw;
            }


        }
    }

    public class ResultCheckUserExistDto
    {
        public bool IsExist { get; set; }
        public int UserId { get; set; }
        public int Level { get; set; }
    }

    public class RequestCheckUserExistDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
