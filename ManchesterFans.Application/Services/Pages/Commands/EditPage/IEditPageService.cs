using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.EditPage
{
    public interface IEditPageService
    {
        ResultDto<EditPageDto> Execute(RequestEditPageDto request);
    }

    public class EditPageService : IEditPageService
    {

        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public EditPageService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto<EditPageDto> Execute(RequestEditPageDto request)
        {
            Page page;

            //Find nonUpdated Page
            try
            {
                page = _context.Pages.Find(request.PageId);
            }
            catch (Exception)
            {
                return new ResultDto<EditPageDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

            // Change Image If Changed
            if (request.ImageFile!=null)
            {
                var upResult = UploadFile(request.ImageFile);
                if (upResult.Status)
                {
                    DeleteFile(page.ImageName);
                    page.ImageName = upResult.FileNameAddress;
                }
                else
                {
                    return new ResultDto<EditPageDto>()
                    {
                        IsSuccess = false,
                        Message = "مشکلی در ذخیره تصویر پیش آمد"
                    };
                }
            }


            // Set Changes
            page.GroupId = request.GroupId;
            page.ShortDescribtion = request.ShortDescribtion;
            page.Tags = request.Tags;
            page.Text = request.Text;
            page.Title = request.Title;
            
            // Set Update Time
            page.UpdateTime = DateTime.Now;


            PageGroup pagegroup;
            try
            {
                pagegroup = _context.PageGroups.Find(request.GroupId);
            }
            catch (Exception)
            {
                return new ResultDto<EditPageDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
            if (pagegroup == null)
            {
                return new ResultDto<EditPageDto>()
                {
                    Message = "گروه مورد نظر یافت نشد",
                    IsSuccess = false
                };
            }
            page.PageGroup = pagegroup;

            try
            {
                _context.SaveChanges();

                return new ResultDto<EditPageDto>()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                return new ResultDto<EditPageDto>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

        }

        private bool DeleteFile(string Path)
        {
            return true;
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
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }

    public class EditPageDto
    {

    }

    public class RequestEditPageDto
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
        public int GroupId { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
