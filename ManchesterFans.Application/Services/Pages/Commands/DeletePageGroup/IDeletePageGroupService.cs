using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Commands.DeletePageGroup
{
    public interface IDeletePageGroupService
    {
        ResultDto Execute(int Id);
    }
    public class DeletePageGroupService : IDeletePageGroupService
    {
        IDataBaseContext _context;
        public DeletePageGroupService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int Id)
        {
            PageGroup pageGroup;

            try
            {
                pageGroup = _context.PageGroups.Find(Id);
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

            try
            {

                pageGroup.IsRemoved = true;
                pageGroup.RemoveTime = DateTime.Now;

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
                    Message = "مشکلی پیش آمد",
                    IsSuccess = false,
                };
            }
        }
    }
}
