using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Queries.GetUserProfile
{
    public interface IGetUserProfileService
    {
        ResultDto<UserProfileDto> Execute(int UserId);
    }

    public class GetUserProfileService : IGetUserProfileService
    {
        private readonly IDataBaseContext _context;
        public GetUserProfileService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<UserProfileDto> Execute(int UserId)
        {
            try
            {

                var user = _context.Users.Find(UserId);

                if (user == null)
                {
                    return new ResultDto<UserProfileDto>()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }


                return new ResultDto<UserProfileDto>()
                {
                    Data = new UserProfileDto()
                    {
                        Bio = user.Bio,
                        ImageName = user.image,
                        Level = GetLevel(user.Level),
                        Username = user.Username
                    },
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ResultDto<UserProfileDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }

        private string GetLevel(int level)
        {
            if (level < 5)
            {
                return "عضو";
            }
            if (level >= 5 && level < 10)
            {
                return "ادمین";
            }

            return "مدیر";
        }
    }

    public class UserProfileDto
    {
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Level { get; set; }
        public string ImageName { get; set; }
    }
}
