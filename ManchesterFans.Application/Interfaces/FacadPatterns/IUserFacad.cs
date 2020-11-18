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
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Application.Interfaces
{
    public interface IUserFacad
    {

        IGetAdminsListService GetAdminsListService { get; }
        IGetAdminsDetailForAdminService GetAdminsDetailForAdminService { get; }
        IGetUserForEditForAdmin GetUserForEditForAdmin { get; }
        IGetUserForDeleteFromAdmin GetUserForDeleteFromAdmin { get; }
        IGetUserProfileService GetUserProfileService { get; }


        ICreateNewUserService CreateNewUserService { get; }
        IEditUserFromAdminService EditUserFromAdminService { get; }
        IDeleteUserService DeleteUserService { get; }
        IUserSignupService UserSignupService { get; }
        ICheckUserExistService CheckUserExistService { get; }
        IChangeUserImageService ChangeUserImageService { get; }
        IChangeUsernameService ChangeUsernameService { get; }
        IChangeUserBioService ChangeUserBioService { get; }

    }
}
