using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ManchesterFans.Common.Utilities;

namespace ManchesterFans.Application.Services.Pages.Queries.GetPageGrpoupsForAdmin
{
    public interface IGetPageGroupsForAdminService
    {
        ResultDto<IEnumerable<PageGroupsForAdminDto>> Execute();
    }

    public class GetPageGroupsForAdminService : IGetPageGroupsForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetPageGroupsForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<IEnumerable<PageGroupsForAdminDto>> Execute()
        {
            IEnumerable<PageGroupsForAdminDto> Data;


            try
            {
                Data = _context.PageGroups
                    .Select(pg => new PageGroupsForAdminDto()
                    {
                        GroupId = pg.GroupId,
                        GroupName = pg.GroupName
                    }).ToList();

            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<PageGroupsForAdminDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمذ"
                };

            }

            return new ResultDto<IEnumerable<PageGroupsForAdminDto>>()
            {
                Data = Data,
                IsSuccess = true,
            };
        }
    }
    public class PageGroupsForAdminDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
