using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ManchesterFans.Common;

namespace ManchesterFans.Application.Services.Users.Queries.GetAdminsDetailForAdmin
{
    public interface IGetAdminsDetailForAdminService
    {
        ResultDto<AdminDetailDto> Execute(int Id);
    }

    public class GetAdminsDetailForAdminService : IGetAdminsDetailForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetAdminsDetailForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<AdminDetailDto> Execute(int Id)
        {
            try
            {

                var admin = _context.Users
                    .Find(Id);

                AdminDetailDto Data = new AdminDetailDto()
                {
                    Level = admin.Level,
                    Bio = admin.Bio,
                    image = admin.image,
                    LoginId = admin.LoginId,
                    NickName = admin.NickName,
                    Username = admin.Username,
                };

                return new ResultDto<AdminDetailDto>()
                {
                    Data = Data,
                    IsSuccess = true
                };
            }
            catch (Exception)
            {

                return new ResultDto<AdminDetailDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };

            }
        }
    }

    public class AdminDetailDto
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
        public string image { get; set; }
        public string Bio { get; set; }
    }
}
