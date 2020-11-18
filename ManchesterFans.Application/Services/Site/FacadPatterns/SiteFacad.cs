using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Application.Interfaces.FacadPatterns;
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

namespace ManchesterFans.Application.Services.Site.FacadPatterns
{
    public class SiteFacad: ISiteFacad
    {
        private readonly IDataBaseContext _context;
        public SiteFacad(IDataBaseContext context)
        {
            _context = context;
        }

        
        private IGetSiteNameForAdminService _getSiteNameForAdminService;
        public IGetSiteNameForAdminService GetSiteNameForAdminService
        {
            get
            {
                if (_getSiteNameForAdminService == null)
                {
                    _getSiteNameForAdminService = new GetSiteNameForAdminService(_context);
                }
                return _getSiteNameForAdminService;
            }
        }
        
        private IEditSiteNameService _editSiteNameService;
        public IEditSiteNameService EditSiteNameService
        {
            get
            {
                if (_editSiteNameService == null)
                {
                    _editSiteNameService = new EditSiteNameService(_context);
                }
                return _editSiteNameService;
            }
        }
        
        private IGetHeaderLinksForAdminService _getHeaderLinksForAdminService;
        public IGetHeaderLinksForAdminService GetHeaderLinksForAdminService
        {
            get
            {
                if (_getHeaderLinksForAdminService == null)
                {
                    _getHeaderLinksForAdminService = new GetHeaderLinksForAdminService(_context);
                }
                return _getHeaderLinksForAdminService;
            }
        }
        
        private ICreateHeaderLinkService _createHeaderLinkService;
        public ICreateHeaderLinkService CreateHeaderLinkService
        {
            get
            {
                if (_createHeaderLinkService == null)
                {
                    _createHeaderLinkService = new CreateHeaderLinkService(_context);
                }
                return _createHeaderLinkService;
            }
        }
        
        private IGetSingleHeaderLinkForAdminService _getSingleHeaderLinkForAdminService;
        public IGetSingleHeaderLinkForAdminService GetSingleHeaderLinkForAdminService
        {
            get
            {
                if (_getSingleHeaderLinkForAdminService == null)
                {
                    _getSingleHeaderLinkForAdminService = new GetSingleHeaderLinkForAdminService(_context);
                }
                return _getSingleHeaderLinkForAdminService;
            }
        }
        
        private IEditHeaderLinkService _editHeaderLinkService;
        public IEditHeaderLinkService EditHeaderLinkService
        {
            get
            {
                if (_editHeaderLinkService == null)
                {
                    _editHeaderLinkService = new EditHeaderLinkService(_context);
                }
                return _editHeaderLinkService;
            }
        }
        
        private IDeleteHeaderLinkService _deleteHeaderLinkService;
        public IDeleteHeaderLinkService DeleteHeaderLinkService
        {
            get
            {
                if (_deleteHeaderLinkService == null)
                {
                    _deleteHeaderLinkService = new DeleteHeaderLinkService(_context);
                }
                return _deleteHeaderLinkService;
            }
        }
        
        private IGetSiteHeaderForSiteService _getSiteHeaderForSiteService;
        public IGetSiteHeaderForSiteService GetSiteHeaderForSiteService
        {
            get
            {
                if (_getSiteHeaderForSiteService == null)
                {
                    _getSiteHeaderForSiteService = new GetSiteHeaderForSiteService(_context);
                }
                return _getSiteHeaderForSiteService;
            }
        }
        
        private IGetGroupsForSiteFooter _getGroupsForSiteFooter;
        public IGetGroupsForSiteFooter GetGroupsForSiteFooter
        {
            get
            {
                if (_getGroupsForSiteFooter == null)
                {
                    _getGroupsForSiteFooter = new GetGroupsForSiteFooter(_context);
                }
                return _getGroupsForSiteFooter;
            }
        }

        private IGetRecentPagesForSiteFooterService _getRecentPagesForSiteFooterService;
        public IGetRecentPagesForSiteFooterService GetRecentPagesForSiteFooterService
        {
            get
            {
                if (_getRecentPagesForSiteFooterService == null)
                {
                    _getRecentPagesForSiteFooterService = new GetRecentPagesForSiteFooterService(_context);
                }
                return _getRecentPagesForSiteFooterService;
            }
        }

    }
}
