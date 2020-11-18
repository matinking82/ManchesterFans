using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManchesterFans.Application.Services.Users.Commands.ChangeUserImage
{
    public interface IChangeUserImageService
    {
        ResultDto Execute(RequestChangeUserImageDto request);
    }

    public class ChangeUserImageService : IChangeUserImageService
    {

        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public ChangeUserImageService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(RequestChangeUserImageDto request)
        {
            try
            {
                var result = UploadFile(request.Image);

                if (!result.Status)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مشکلی در آپلود تصویر پیش آمد"
                    };
                }


                var user = _context.Users.Find(request.UserId);

                if (user==null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "n"
                    };
                }

                user.image = result.FileNameAddress;

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


        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\UserImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = fileName,
                    Status = true,
                };
            }
            return null;
        }
        public class UploadDto
        {
            public bool Status { get; set; }
            public string FileNameAddress { get; set; }
        }
    }

    public class RequestChangeUserImageDto
    {
        public int UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
