using ManchesterFans.Application.Interfaces;
using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Application.Services.Users.Commands.ChangeUserBio;
using ManchesterFans.Application.Services.Users.Commands.ChangeUserImage;
using ManchesterFans.Application.Services.Users.Commands.ChangeUsername;
using ManchesterFans.Application.Services.Users.Commands.CheckUserExist;
using ManchesterFans.Application.Services.Users.Commands.CreateNewUser;
using ManchesterFans.Application.Services.Users.Commands.DeleteUser;
using ManchesterFans.Application.Services.Users.Commands.EditUserFromAdmin;
using ManchesterFans.Application.Services.Users.Commands.UserSignup;
using ManchesterFans.Application.Services.Users.Queries.GetAdminsDetailForAdmin;
using ManchesterFans.Application.Services.Users.Queries.GetAdminsList;
using ManchesterFans.Application.Services.Users.Queries.GetUserForDeleteFromAdmin;
using ManchesterFans.Application.Services.Users.Queries.GetUserForEditForAdmin;
using ManchesterFans.Application.Services.Users.Queries.GetUserProfile;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Services.Users.FacadPattern
{
    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public UserFacad(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private IGetAdminsListService _getAdminsListService;
        public IGetAdminsListService GetAdminsListService
        {
            get
            {
                if (_getAdminsListService == null)
                {
                    _getAdminsListService = new GetAdminsListService(_context);
                }

                return _getAdminsListService;
            }
        }

        private IGetAdminsDetailForAdminService _getAdminsDetailForAdminService;
        public IGetAdminsDetailForAdminService GetAdminsDetailForAdminService
        {
            get
            {
                if (_getAdminsDetailForAdminService == null)
                {
                    _getAdminsDetailForAdminService = new GetAdminsDetailForAdminService(_context);
                }

                return _getAdminsDetailForAdminService;
            }
        }


        private ICreateNewUserService _createNewUserService;
        public ICreateNewUserService CreateNewUserService
        {
            get
            {
                if (_createNewUserService == null)
                {
                    _createNewUserService = new CreateNewUserService(_context);
                }

                return _createNewUserService;
            }
        }

        private IGetUserForEditForAdmin _getUserForEditForAdmin;
        public IGetUserForEditForAdmin GetUserForEditForAdmin
        {
            get
            {
                if (_getUserForEditForAdmin == null)
                {
                    _getUserForEditForAdmin = new GetUserForEditForAdmin(_context);
                }

                return _getUserForEditForAdmin;
            }
        }


        private IEditUserFromAdminService _editUserFromAdminService;
        public IEditUserFromAdminService EditUserFromAdminService
        {
            get
            {
                if (_editUserFromAdminService == null)
                {
                    _editUserFromAdminService = new EditUserFromAdminService(_context);
                }

                return _editUserFromAdminService;
            }
        }

        private IGetUserForDeleteFromAdmin _getUserForDeleteFromAdmin;
        public IGetUserForDeleteFromAdmin GetUserForDeleteFromAdmin
        {
            get
            {
                if (_getUserForDeleteFromAdmin == null)
                {
                    _getUserForDeleteFromAdmin = new GetUserForDeleteFromAdmin(_context);
                }

                return _getUserForDeleteFromAdmin;
            }
        }

        private IDeleteUserService _deleteUserService;
        public IDeleteUserService DeleteUserService
        {
            get
            {
                if (_deleteUserService == null)
                {
                    _deleteUserService = new DeleteUserService(_context);
                }

                return _deleteUserService;
            }
        }

        private IUserSignupService _userSignupService;
        public IUserSignupService UserSignupService
        {
            get
            {
                if (_userSignupService == null)
                {
                    _userSignupService = new UserSignupService(_context);
                }

                return _userSignupService;
            }
        }

        private ICheckUserExistService _checkUserExistService;
        public ICheckUserExistService CheckUserExistService
        {
            get
            {
                if (_checkUserExistService == null)
                {
                    _checkUserExistService = new CheckUserExistService(_context);
                }

                return _checkUserExistService;
            }
        }

        private IGetUserProfileService _getUserProfileService;
        public IGetUserProfileService GetUserProfileService
        {
            get
            {
                if (_getUserProfileService == null)
                {
                    _getUserProfileService = new GetUserProfileService(_context);
                }

                return _getUserProfileService;
            }
        }
        
        private IChangeUserImageService _changeUserImageService;
        public IChangeUserImageService ChangeUserImageService
        {
            get
            {
                if (_changeUserImageService == null)
                {
                    _changeUserImageService = new ChangeUserImageService(_context, _environment);
                }

                return _changeUserImageService;
            }
        }
        
        private IChangeUsernameService _changeUsernameService;
        public IChangeUsernameService ChangeUsernameService
        {
            get
            {
                if (_changeUsernameService == null)
                {
                    _changeUsernameService = new ChangeUsernameService(_context);
                }

                return _changeUsernameService;
            }
        }

        private IChangeUserBioService _changeUserBioService;
        public IChangeUserBioService ChangeUserBioService
        {
            get
            {
                if (_changeUserBioService == null)
                {
                    _changeUserBioService = new ChangeUserBioService(_context);
                }

                return _changeUserBioService;
            }
        }

    }
}
