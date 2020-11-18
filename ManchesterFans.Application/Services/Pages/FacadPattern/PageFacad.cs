using ManchesterFans.Application.FacadPatterns;
using ManchesterFans.Application.Interfaces.Context;
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
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Pages.FacadPattern
{
    public class PageFacad : IPageFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public PageFacad(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        private IGetPageGroupsForAdminService _getPageGroupsForAdminService;
        public IGetPageGroupsForAdminService GetPageGroupsForAdminService
        {
            get
            {
                if (_getPageGroupsForAdminService == null)
                {
                    _getPageGroupsForAdminService = new GetPageGroupsForAdminService(_context);
                }
                return _getPageGroupsForAdminService;
            }
        }

        private ICreatePageGroupService _createPageGroupService;
        public ICreatePageGroupService CreatePageGroupService
        {
            get
            {
                if (_createPageGroupService == null)
                {
                    _createPageGroupService = new CreatePageGroupService(_context);
                }
                return _createPageGroupService;
            }
        }

        private IGetSinglePageGroupService _getSinglePageGroupService;
        public IGetSinglePageGroupService GetSinglePageGroupService
        {
            get
            {
                if (_getSinglePageGroupService == null)
                {
                    _getSinglePageGroupService = new GetSinglePageGroupService(_context);
                }
                return _getSinglePageGroupService;
            }
        }

        private IEditPageGroupService _editPageGroupService;
        public IEditPageGroupService EditPageGroupService
        {
            get
            {
                if (_editPageGroupService == null)
                {
                    _editPageGroupService = new EditPageGroupService(_context);
                }
                return _editPageGroupService;
            }
        }

        private IDeletePageGroupService _deletePageGroupService;
        public IDeletePageGroupService DeletePageGroupService
        {
            get
            {
                if (_deletePageGroupService == null)
                {
                    _deletePageGroupService = new DeletePageGroupService(_context);
                }
                return _deletePageGroupService;
            }
        }

        private IGetPagesForAdminService _getPagesForAdminService;
        public IGetPagesForAdminService GetPagesForAdminService
        {
            get
            {
                if (_getPagesForAdminService == null)
                {
                    _getPagesForAdminService = new GetPagesForAdminService(_context);
                }
                return _getPagesForAdminService;
            }
        }

        private IGetSinglePageForAdminService _getSinglePageForAdminService;
        public IGetSinglePageForAdminService GetSinglePageForAdminService
        {
            get
            {
                if (_getSinglePageForAdminService == null)
                {
                    _getSinglePageForAdminService = new GetSinglePageForAdminService(_context);
                }
                return _getSinglePageForAdminService;
            }
        }

        private ICreatePageService _createPageService;
        public ICreatePageService CreatePageService
        {
            get
            {
                if (_createPageService == null)
                {
                    _createPageService = new CreatePageService(_context, _environment);
                }
                return _createPageService;
            }
        }

        private IEditPageService _editPageService;
        public IEditPageService EditPageService
        {
            get
            {
                if (_editPageService == null)
                {
                    _editPageService = new EditPageService(_context, _environment);
                }
                return _editPageService;
            }
        }

        private IGetSinglePageForEditService _getSinglePageForEditService;
        public IGetSinglePageForEditService GetSinglePageForEditService
        {
            get
            {
                if (_getSinglePageForEditService == null)
                {
                    _getSinglePageForEditService = new GetSinglePageForEditService(_context);
                }
                return _getSinglePageForEditService;
            }
        }

        private IDeletePageService _deletePageService;
        public IDeletePageService DeletePageService
        {
            get
            {
                if (_deletePageService == null)
                {
                    _deletePageService = new DeletePageService(_context);
                }
                return _deletePageService;
            }
        }
        
        private IGetSinglePageForSiteService _getSinglePageForSiteService;
        public IGetSinglePageForSiteService GetSinglePageForSiteService
        {
            get
            {
                if (_getSinglePageForSiteService == null)
                {
                    _getSinglePageForSiteService = new GetSinglePageForSiteService(_context);
                }
                return _getSinglePageForSiteService;
            }
        }

        private IPageVisitedService _pageVisitedService;
        public IPageVisitedService PageVisitedService
        {
            get
            {
                if (_pageVisitedService == null)
                {
                    _pageVisitedService = new PageVisitedService(_context);
                }
                return _pageVisitedService;
            }
        }
        
        private IGetRecentPagesForSiteService _getRecentPagesForSiteService;
        public IGetRecentPagesForSiteService GetRecentPagesForSiteService
        {
            get
            {
                if (_getRecentPagesForSiteService == null)
                {
                    _getRecentPagesForSiteService = new GetRecentPagesForSiteService(_context);
                }
                return _getRecentPagesForSiteService;
            }
        }
        
        private IGetMostViewedPagesService _getMostViewedPagesService;
        public IGetMostViewedPagesService GetMostViewedPagesService
        {
            get
            {
                if (_getMostViewedPagesService == null)
                {
                    _getMostViewedPagesService = new GetMostViewedPagesService(_context);
                }
                return _getMostViewedPagesService;
            }
        }

        private IGetMostLikedPagesForSiteService _getMostLikedPagesForSiteService;
        public IGetMostLikedPagesForSiteService GetMostLikedPagesForSiteService
        {
            get
            {
                if (_getMostLikedPagesForSiteService == null)
                {
                    _getMostLikedPagesForSiteService = new GetMostLikedPagesForSiteService(_context);
                }
                return _getMostLikedPagesForSiteService;
            }
        }

        private IGetSliderPagesListForAdmin _getSliderPagesListForAdmin;
        public IGetSliderPagesListForAdmin GetSliderPagesListForAdmin
        {
            get
            {
                if (_getSliderPagesListForAdmin == null)
                {
                    _getSliderPagesListForAdmin = new GetSliderPagesListForAdmin(_context);
                }
                return _getSliderPagesListForAdmin;
            }
        }

        private IGetSliderPagesDetailForAdmin _getSliderPagesDetailForAdmin;
        public IGetSliderPagesDetailForAdmin GetSliderPagesDetailForAdmin
        {
            get
            {
                if (_getSliderPagesDetailForAdmin == null)
                {
                    _getSliderPagesDetailForAdmin = new GetSliderPagesDetailForAdmin(_context);
                }
                return _getSliderPagesDetailForAdmin;
            }
        }

        private IRemovePageFromSliderService _removePageFromSliderService;
        public IRemovePageFromSliderService  RemovePageFromSliderService
        {
            get
            {
                if (_removePageFromSliderService == null)
                {
                    _removePageFromSliderService = new RemovePageFromSliderService(_context);
                }
                return _removePageFromSliderService;
            }
        }

        private IGetPagesListForSliderSelectService _getPagesListForSliderSelectService;
        public IGetPagesListForSliderSelectService GetPagesListForSliderSelectService
        {
            get
            {
                if (_getPagesListForSliderSelectService == null)
                {
                    _getPagesListForSliderSelectService = new GetPagesListForSliderSelectService(_context);
                }
                return _getPagesListForSliderSelectService;
            }
        }
        
        private IAddPageToSliderService _addPageToSliderService;
        public IAddPageToSliderService AddPageToSliderService
        {
            get
            {
                if (_addPageToSliderService == null)
                {
                    _addPageToSliderService = new AddPageToSliderService(_context);
                }
                return _addPageToSliderService;
            }
        }

        private IGetPagesForSiteSliderService _getPagesForSiteSliderService;
        public IGetPagesForSiteSliderService GetPagesForSiteSliderService
        {
            get
            {
                if (_getPagesForSiteSliderService == null)
                {
                    _getPagesForSiteSliderService = new GetPagesForSiteSliderService(_context);
                }
                return _getPagesForSiteSliderService;
            }
        }
        
        private IGetPagesForSearchService _getPagesForSearchService;
        public IGetPagesForSearchService GetPagesForSearchService
        {
            get
            {
                if (_getPagesForSearchService == null)
                {
                    _getPagesForSearchService = new GetPagesForSearchService(_context);
                }
                return _getPagesForSearchService;
            }
        }
        
        private IAddPageCommentService _addPageCommentService;
        public IAddPageCommentService AddPageCommentService
        {
            get
            {
                if (_addPageCommentService == null)
                {
                    _addPageCommentService = new AddPageCommentService(_context);
                }
                return _addPageCommentService;
            }
        }
        
        private IGetPageCommentsForSitePageSevice _getPageCommentsForSitePageSevice;
        public IGetPageCommentsForSitePageSevice GetPageCommentsForSitePageSevice
        {
            get
            {
                if (_getPageCommentsForSitePageSevice == null)
                {
                    _getPageCommentsForSitePageSevice = new GetPageCommentsForSitePageSevice(_context);
                }
                return _getPageCommentsForSitePageSevice;
            }
        }
        

        private IGetPagesPerGroupForSiteService _getPagesPerGroupForSiteService;
        public IGetPagesPerGroupForSiteService GetPagesPerGroupForSiteService
        {
            get
            {
                if (_getPagesPerGroupForSiteService == null)
                {
                    _getPagesPerGroupForSiteService = new GetPagesPerGroupForSiteService(_context);
                }
                return _getPagesPerGroupForSiteService;
            }
        }
        
        private IPageLikedService _pageLikedService;
        public IPageLikedService PageLikedService
        {
            get
            {
                if (_pageLikedService == null)
                {
                    _pageLikedService = new PageLikedService(_context);
                }
                return _pageLikedService;
            }
        }
        
        private IGetUnAcceptedCommentsService _getUnAcceptedCommentsService;
        public IGetUnAcceptedCommentsService GetUnAcceptedCommentsService
        {
            get
            {
                if (_getUnAcceptedCommentsService == null)
                {
                    _getUnAcceptedCommentsService = new GetUnAcceptedCommentsService(_context);
                }
                return _getUnAcceptedCommentsService;
            }
        }
        
        private IAcceptPageCommentsService _acceptPageCommentsService;
        public IAcceptPageCommentsService AcceptPageCommentsService
        {
            get
            {
                if (_acceptPageCommentsService == null)
                {
                    _acceptPageCommentsService = new AcceptPageCommentsService(_context);
                }
                return _acceptPageCommentsService;
            }
        }

        private IDeletePageCommentsService _deletePageCommentsService;
        public IDeletePageCommentsService DeletePageCommentsService
        {
            get
            {
                if (_deletePageCommentsService == null)
                {
                    _deletePageCommentsService = new DeletePageCommentsService(_context);
                }
                return _deletePageCommentsService;
            }
        }

    }
}
