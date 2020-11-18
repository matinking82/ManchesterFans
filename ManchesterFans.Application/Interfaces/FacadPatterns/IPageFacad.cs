using ManchesterFans.Application.Services.Pages.Commands.AcceptPageComments;
using ManchesterFans.Application.Services.Pages.Commands.AddPageComment;
using ManchesterFans.Application.Services.Pages.Commands.AddPageToSlider;
using ManchesterFans.Application.Services.Pages.Commands.CreatePage;
using ManchesterFans.Application.Services.Pages.Commands.CreatePageGroup;
using ManchesterFans.Application.Services.Pages.Commands.DeletePage;
using ManchesterFans.Application.Services.Pages.Commands.DeletePageComments;
using ManchesterFans.Application.Services.Pages.Commands.DeletePageGroup;
using ManchesterFans.Application.Services.Pages.Commands.EditPage;
using ManchesterFans.Application.Services.Pages.Commands.EditPageGroup;
using ManchesterFans.Application.Services.Pages.Commands.PageLiked;
using ManchesterFans.Application.Services.Pages.Commands.PageVisited;
using ManchesterFans.Application.Services.Pages.Commands.RemovePageFromSlider;
using ManchesterFans.Application.Services.Pages.Queries.GetMostLikedPagesForSite;
using ManchesterFans.Application.Services.Pages.Queries.GetMostViewedPages;
using ManchesterFans.Application.Services.Pages.Queries.GetPageCommentsForSitePage;
using ManchesterFans.Application.Services.Pages.Queries.GetPageGrpoupsForAdmin;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesForAdmin;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesForSearch;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesForSiteSlider;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesForSliderSelect;
using ManchesterFans.Application.Services.Pages.Queries.GetPagesPerGroupForSite;
using ManchesterFans.Application.Services.Pages.Queries.GetRecentPagesForSite;
using ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForAdmin;
using ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForEdit;
using ManchesterFans.Application.Services.Pages.Queries.GetSinglePageForSite;
using ManchesterFans.Application.Services.Pages.Queries.GetSinglePageGroup;
using ManchesterFans.Application.Services.Pages.Queries.GetSliderPagesDetailForAdmin;
using ManchesterFans.Application.Services.Pages.Queries.GetSliderPagesListForAdmin;
using ManchesterFans.Application.Services.Pages.Queries.GetUnAcceptedComments;

using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.FacadPatterns
{
    public interface IPageFacad
    {

        ////////////////////////// PageGroup Services ///////////////////////

        //Commands

        ICreatePageGroupService CreatePageGroupService { get; }
        IEditPageGroupService EditPageGroupService { get; }
        IDeletePageGroupService DeletePageGroupService { get; }

        //Queries

        IGetPageGroupsForAdminService GetPageGroupsForAdminService { get; }
        IGetSinglePageGroupService GetSinglePageGroupService { get; }

        ///////////////////////////////////////////////////////////////////


        ////////////////////////// Page Services ///////////////////////

        //Commands

        ICreatePageService CreatePageService { get; }
        IEditPageService EditPageService { get; }
        IDeletePageService DeletePageService { get; }
        IPageVisitedService PageVisitedService { get; }
        IRemovePageFromSliderService RemovePageFromSliderService { get; }
        IAddPageToSliderService AddPageToSliderService { get; }
        IPageLikedService PageLikedService { get; }

        //Queries

        IGetPagesForAdminService GetPagesForAdminService { get; }
        IGetSinglePageForAdminService GetSinglePageForAdminService { get; }
        IGetSinglePageForEditService GetSinglePageForEditService { get; }
        IGetSinglePageForSiteService GetSinglePageForSiteService { get; }
        IGetRecentPagesForSiteService GetRecentPagesForSiteService { get; }
        IGetMostViewedPagesService GetMostViewedPagesService { get; }
        IGetMostLikedPagesForSiteService GetMostLikedPagesForSiteService { get; }
        IGetSliderPagesListForAdmin GetSliderPagesListForAdmin { get; }
        IGetSliderPagesDetailForAdmin GetSliderPagesDetailForAdmin { get; }
        IGetPagesListForSliderSelectService GetPagesListForSliderSelectService { get; }
        IGetPagesForSiteSliderService GetPagesForSiteSliderService { get; }
        IGetPagesForSearchService GetPagesForSearchService { get; }
        IGetPagesPerGroupForSiteService GetPagesPerGroupForSiteService { get; }

        ///////////////////////////////////////////////////////////////////

        ////////////////////////// PageComments Services ///////////////////////

        //Commands
        IAddPageCommentService AddPageCommentService { get; }
        IAcceptPageCommentsService AcceptPageCommentsService { get; }
        IDeletePageCommentsService DeletePageCommentsService { get; }

        //Queries
        IGetPageCommentsForSitePageSevice GetPageCommentsForSitePageSevice { get;}
        IGetUnAcceptedCommentsService GetUnAcceptedCommentsService { get; }

        ///////////////////////////////////////////////////////////////////

    }
}
