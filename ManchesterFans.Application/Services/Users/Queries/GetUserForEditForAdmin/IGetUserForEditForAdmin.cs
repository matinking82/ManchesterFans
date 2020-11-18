using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Queries.GetUserForEditForAdmin
{
    public interface IGetUserForEditForAdmin
    {
        ResultDto<EditUserDto> Execute(int Id);
    }

    public class GetUserForEditForAdmin : IGetUserForEditForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetUserForEditForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<EditUserDto> Execute(int Id)
        {
            //Finding unedited User
            try
            {

                User user = _context.Users.Find(Id);

                if (user == null)
                {
                    return new ResultDto<EditUserDto>()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }


                EditUserDto Data = new EditUserDto()
                {
                    Level = user.Level,
                    LoginId = user.LoginId,
                    NickName = user.NickName,
                    Username = user.Username
                };

                return new ResultDto<EditUserDto>()
                {
                    Data = Data,
                    IsSuccess = true,
                };

            }
            catch (Exception)
            {

                return new ResultDto<EditUserDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

        }
    }

    public class EditUserDto
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
    }

}
