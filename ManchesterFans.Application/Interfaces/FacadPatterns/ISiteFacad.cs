using ManchesterFans.Application.Services.Site.Commands.CreateHeaderLink;
using ManchesterFans.Application.Services.Site.Commands.DeleteHeaderLinkService;
using ManchesterFans.Application.Services.Site.Commands.EditHeaderLink;
using ManchesterFans.Application.Services.Site.Commands.EditSiteName;
using ManchesterFans.Application.Services.Site.Queries.GetGroupsForSiteFooter;
using ManchesterFans.Application.Services.Site.Queries.GetHeaderLinksForAdmin;
using ManchesterFans.Application.Services.Site.Queries.GetRecentPagesForSiteFooter;
using ManchesterFans.Application.Services.Site.Queries.GetSingleHeaderLinkForAdmin;
using ManchesterFans.Application.Services.Site.Queries.GetSiteHeaderForSite;
using ManchesterFans.Application.Services.Site.Queries.GetSiteNameForAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Interfaces.FacadPatterns
{
    public interface ISiteFacad
    {
        IEditSiteNameService EditSiteNameService { get; }
        IEditHeaderLinkService EditHeaderLinkService { get; }
        ICreateHeaderLinkService CreateHeaderLinkService { get; }
        IDeleteHeaderLinkService DeleteHeaderLinkService { get; }

        IGetSiteNameForAdminService GetSiteNameForAdminService { get; }
        IGetHeaderLinksForAdminService GetHeaderLinksForAdminService { get; }
        IGetSingleHeaderLinkForAdminService GetSingleHeaderLinkForAdminService { get; }
        IGetSiteHeaderForSiteService GetSiteHeaderForSiteService { get; }
        IGetGroupsForSiteFooter GetGroupsForSiteFooter { get; }
        IGetRecentPagesForSiteFooterService GetRecentPagesForSiteFooterService { get; }
    }
}
