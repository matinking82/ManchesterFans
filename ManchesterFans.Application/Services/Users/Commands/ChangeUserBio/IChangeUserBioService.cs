using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.ChangeUserBio
{
    public interface IChangeUserBioService
    {
        ResultDto Execute(RequestChangeUserBioDto request);

    }

    public class ChangeUserBioService : IChangeUserBioService
    {
        private readonly IDataBaseContext _context;

        public ChangeUserBioService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestChangeUserBioDto request)
        {
            try
            {

                var user = _context.Users.Find(request.UserId);

                if (user == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                user.Bio = request.Bio;

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
                    Message = "مشکلی پیش آمد!!"
                };
            }
        }
    }

    public class RequestChangeUserBioDto
    {
        public int UserId { get; set; }
        public string Bio { get; set; }
    }
}
