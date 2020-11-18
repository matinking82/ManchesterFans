using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.Queries.GetSinglePageGroup
{
    public interface IGetSinglePageGroupService
    {
        ResultDto<SinglePageGroupDto> Execute(int Id);
    }

    public class GetSinglePageGroupService : IGetSinglePageGroupService
    {
        IDataBaseContext _context;
        public GetSinglePageGroupService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<SinglePageGroupDto> Execute(int Id)
        {
            PageGroup pageGroup;
            try
            {
                pageGroup = _context.PageGroups.Find(Id);
            }
            catch (Exception)
            {
                return new ResultDto<SinglePageGroupDto>()
                {
                     IsSuccess = false,
                     Message = "مشکلی پیش آمد"
                };
            }

            if (pageGroup==null)
            {
                return new ResultDto<SinglePageGroupDto>()
                {
                    IsSuccess = false,
                    Message = "گروهی یافت نشد"                    
                };
            }

            SinglePageGroupDto Data = new SinglePageGroupDto()
            {
                GroupId = pageGroup.GroupId,
                GroupName = pageGroup.GroupName
            };

            return new ResultDto<SinglePageGroupDto>()
            {
                Data = Data,
                IsSuccess = true,
            };
        }
    }

    public class SinglePageGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }

}
