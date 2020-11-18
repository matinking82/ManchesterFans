using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.EditPageGroup
{
    public interface IEditPageGroupService
    {
        ResultDto Execute(RequestEditPageGroupDto request);
    }

    public class EditPageGroupService : IEditPageGroupService
    {
        IDataBaseContext _context;
        public EditPageGroupService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditPageGroupDto request)
        {
            PageGroup pageGroup;

            try
            {
                pageGroup = _context.PageGroups.Find(request.GroupId);
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }

            if (pageGroup == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "گروهی یافت نشد"
                };
            }

            pageGroup.GroupName = request.GroupName;
            pageGroup.UpdateTime = DateTime.Now;

            try
            {
                _context.PageGroups.Update(pageGroup);
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
                    IsSuccess=false,
                    Message = "مشکلی پیش آمد"
                };
            }

        }
    }

    public class RequestEditPageGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
