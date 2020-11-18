using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterFans.Application.Services.Site.Queries.GetGroupsForSiteFooter
{
    public interface IGetGroupsForSiteFooter
    {
        ResultDto<IEnumerable<GroupsForSiteFooterDto>> Execute();
    }

    public class GetGroupsForSiteFooter : IGetGroupsForSiteFooter
    {
        private readonly IDataBaseContext _context;

        public GetGroupsForSiteFooter(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<IEnumerable<GroupsForSiteFooterDto>> Execute()
        {
            try
            {
                var Groups = _context.PageGroups
                    .Include(pg => pg.Pages);

                return new ResultDto<IEnumerable<GroupsForSiteFooterDto>>()
                {
                    Data = Groups.Select(g=> new GroupsForSiteFooterDto()
                    {
                        GroupName = g.GroupName,
                        PagesCount = g.Pages.Count(),
                        GroupId = g.GroupId
                    }),
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<GroupsForSiteFooterDto>>()
                {
                    IsSuccess = false,
                    Message = "مشکلی پیش آمد"
                };
            }
        }
    }

    public class GroupsForSiteFooterDto
    {
        public string GroupName { get; set; }
        public int PagesCount { get; set; }
        public int GroupId { get; set; }
    }
}
