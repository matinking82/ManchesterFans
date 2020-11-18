using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Queries.GetUserForDeleteFromAdmin
{
    public interface IGetUserForDeleteFromAdmin
    {
        ResultDto<UserForDeleteFromAdmin> Execute(int Id);
    }

    public class GetUserForDeleteFromAdmin : IGetUserForDeleteFromAdmin
    {
        private readonly IDataBaseContext _context;
        public GetUserForDeleteFromAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<UserForDeleteFromAdmin> Execute(int Id)
        {
            try
            {
                User user = _context.Users.Find(Id);

                if (user == null)
                {
                    return new ResultDto<UserForDeleteFromAdmin>()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                return new ResultDto<UserForDeleteFromAdmin>()
                {
                    IsSuccess = true,
                    Data = new UserForDeleteFromAdmin()
                    {
                        image = user.image,
                        InsertTime = user.InsertTime,
                        Level = user.Level,
                        NickName = user.NickName,
                        Username = user.Username,
                        Id = user.LoginId
                    }
                };


            }
            catch (Exception)
            {
                return new ResultDto<UserForDeleteFromAdmin>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
                throw;
            }
        }
    }

    public class UserForDeleteFromAdmin
    {
        public string Username { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
        public string image { get; set; }
        public int Id { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
