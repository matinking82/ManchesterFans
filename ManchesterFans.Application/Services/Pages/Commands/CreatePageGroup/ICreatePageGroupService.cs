using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.CreatePageGroup
{
    public interface ICreatePageGroupService
    {
        ResultDto Execute(string GroupName);
    }

    public class CreatePageGroupService : ICreatePageGroupService
    {
        IDataBaseContext _context;
        public CreatePageGroupService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(string GroupName)
        {
            PageGroup pageGroup = new PageGroup();
            pageGroup.GroupName = GroupName;

            try
            {
                _context.PageGroups.Add(pageGroup);
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
                    Message = "مشکلی در ثبت پیش آمد"
                };
            }
        }
    }
}
