using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ManchesterFans.Common;
using ManchesterFans.Common.Utilities;

namespace ManchesterFans.Application.Services.Users.Queries.GetAdminsList
{
    public interface IGetAdminsListService
    {
        ResultDto<List<AdminsListDto>> Execute(int page, int pageSize);
    }

    public class GetAdminsListService : IGetAdminsListService
    {
        private readonly IDataBaseContext _context;

        public GetAdminsListService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<AdminsListDto>> Execute(int page, int pageSize)
        {
            try
            {

                List<AdminsListDto> Data = _context.Users
                    .Where(u => u.Level >= UserLevels.Admin1)
                    .ToPaged(page, pageSize, out int RowCount)
                    .Select(u => new AdminsListDto()
                    {
                        image = u.image,
                        Level = u.Level,
                        LoginId = u.LoginId,
                        NickName = u.NickName,
                        Username = u.Username
                    }).ToList(); ;

                return new ResultDto<List<AdminsListDto>>()
                {
                    Data = Data,
                    IsSuccess = true
                };
            }
            catch (Exception)
            {

                return new ResultDto<List<AdminsListDto>>()
                {
                    IsSuccess =false,
                    Message = "مشکلی پیش آمد!!"
                };

            }
        }
    }


    public class AdminsListDto
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
        public string image { get; set; }
    }
}
