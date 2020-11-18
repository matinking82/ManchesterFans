using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.CreatePage
{
    public interface ICreatePageService
    {
        ResultDto Execute(RequestCreatePageService request);
    }

    public class CreatePageService : ICreatePageService
    {
        private readonly IHostingEnvironment _environment;
        private readonly IDataBaseContext _context;

        public CreatePageService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(RequestCreatePageService request)
        {
            Page page = new Page()
            {
                GroupId = request.GroupId,
                ShortDescribtion = request.ShortDescribtion,
                Tags = request.Tags,
                Text = request.Text,
                Title = request.Title,
            };

            if (request.ImageFile != null)
            {
                var upResult = UploadFile(request.ImageFile);
                if (!upResult.Status)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مشکلی در ذخیره تصویر بوجود آمد"
                    };
                }

                page.ImageName = upResult.FileNameAddress;
            }
            else
            {
                page.ImageName = @"images\PageImages\" +"Default.png";
            }

            PageGroup pagegroup;
            try
            {
                pagegroup = _context.PageGroups.Find(request.GroupId);
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
            if (pagegroup == null)
            {
                return new ResultDto()
                {
                    Message = "گروه مورد نظر یافت نشد",
                    IsSuccess = false
                };
            }
            page.PageGroup =  pagegroup;

            try
            {

                _context.Pages.Add(page);
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
                    Message = "مشکلی پیش آمد"
                };
            }
        }

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\PageImages\";
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
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
    }
    public class UploadDto
    {
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }

    public class RequestCreatePageService
    {
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
        public int GroupId { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
