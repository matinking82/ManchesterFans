using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Common.Utilities;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.EditUserFromAdmin
{
    public interface IEditUserFromAdminService
    {

        ResultDto Execute(RequestEditUserFromAdminDto request);

    }

    public class EditUserFromAdminService : IEditUserFromAdminService
    {

        private readonly IDataBaseContext _context;

        public EditUserFromAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditUserFromAdminDto request)
        {

            try
            {
                User user = _context.Users.Find(request.LoginId);

                if (user == null)
                {
                    return new ResultDto()
                    {
                        Message = "n",
                        IsSuccess = false
                    };
                }

                user.Username = request.Username;
                user.Password = request.Password;
                user.NickName = request.NickName;
                user.Level = request.Level;
                user.UpdateTime = DateTime.Now;

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

    public class RequestEditUserFromAdminDto
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
    }
}
