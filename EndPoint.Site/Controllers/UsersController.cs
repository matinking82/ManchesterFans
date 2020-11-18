using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EndPoint.Site.ViewModels.SiteViewModels.Users;
using ManchesterFans.Application.Interfaces;
using ManchesterFans.Application.Services.Users.Commands.ChangeUserBio;
using ManchesterFans.Application.Services.Users.Commands.ChangeUserImage;
using ManchesterFans.Application.Services.Users.Commands.ChangeUsername;
using ManchesterFans.Application.Services.Users.Commands.CheckUserExist;
using ManchesterFans.Application.Services.Users.Commands.UserSignup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserFacad _userFacad;
        public UsersController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        [HttpGet]
        [Route("/Signup")]
        public IActionResult Signup()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        [Route("/Signup")]
        [ValidateAntiForgeryToken]
        public IActionResult Signup([Bind("Username,Password,ConfirmPassword")] UserSignupViewModel user)
        {
            if (ModelState.IsValid && user.Password == user.ConfirmPassword)
            {

                RequestUserSignupDto request = new RequestUserSignupDto()
                {
                    Password = user.Password,
                    Username = user.Username,
                };

                var result = _userFacad.UserSignupService.Execute(request);

                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = "حساب شما ایجاد شد !!";
                }

            }
            else
            {
                ViewBag.Message = "اطلاعات وارد شده اشتباه است";
            }
            return View("ShowMessage");
        }

        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        [Route("/Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Username,Password")] UserLoginVewModel user)
        {
            if (ModelState.IsValid)
            {

                RequestCheckUserExistDto request = new RequestCheckUserExistDto()
                {
                    Password = user.Password,
                    Username = user.Username
                };
                var result = _userFacad.CheckUserExistService.Execute(request);

                if (!result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    if (result.Data.IsExist)
                    {


                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier,result.Data.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Role, result.Data.Level.ToString()),
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);


                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = true
                        };

                        HttpContext.SignInAsync(principal, properties);



                        ViewBag.Message = "با موفقیت وارد حساب شدید";
                    }
                    else
                    {
                        ViewBag.Message = "نام کاربری یا کلمه عبور اشتباه است";
                    }
                }
            }
            else
            {
                ViewBag.Message = "اطلاعات وارد شده صحیح نیست";
            }

            return View("ShowMessage");


        }

        [HttpGet]
        [Route("/Signout")]
        public IActionResult Signout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                ViewBag.Message = "با موفقیت از حساب خود خارج شدید";
                return View("ShowMessage");
            }
            return Redirect("/");
        }

        [HttpGet]
        [Route("/Im")]
        public IActionResult UserProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var result = _userFacad.GetUserProfileService.Execute(userId);


            if (!result.IsSuccess)
            {
                if (result.Message == "n")
                {
                    return NotFound();
                }

                ViewBag.Message = result.Message;
                return View("ShowMessage");
            }


            return View(new UserProfileViewModel()
            {
                Bio = result.Data.Bio,
                ImageName = result.Data.ImageName,
                Level = result.Data.Level,
                Username = result.Data.Username,
            });
        }


        [HttpPost]
        public IActionResult ChangeImage(IFormFile imgUp)
        {

            if (imgUp.ContentType.Contains("image/") && User.Identity.IsAuthenticated)
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                RequestChangeUserImageDto request = new RequestChangeUserImageDto()
                {
                    Image = imgUp,
                    UserId = UserId
                };

                var result = _userFacad.ChangeUserImageService.Execute(request);

                if (!result.IsSuccess)
                {
                    if (result.Message == "n")
                    {
                        return NotFound();
                    }

                    ViewBag.Message = result.Message;
                    return View("ShowMessage");

                }

            }
            return Redirect("/Im");
        }

        [HttpPost]
        public IActionResult ChangeName(string Username)
        {

            if (User.Identity.Name != Username && User.Identity.IsAuthenticated)
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                RequestChangeUsername request = new RequestChangeUsername()
                {
                    UserId = UserId,
                    Username = Username
                };
                var result = _userFacad.ChangeUsernameService.Execute(request);

                if (!result.IsSuccess)
                {
                    if (result.Message == "n")
                    {
                        return NotFound();
                    }

                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }
            }

            return Redirect("/im");
        }

        [HttpPost]
        public IActionResult ChangeBio(string Bio)
        {

            if (User.Identity.IsAuthenticated)
            {

                var UserClaims = User.Claims.ToDictionary(c => c.Type, c => Convert.ToInt32(c.Value));

                RequestChangeUserBioDto request = new RequestChangeUserBioDto()
                {
                    Bio = Bio,
                    UserId = UserClaims[ClaimTypes.NameIdentifier]
                };
                var result = _userFacad.ChangeUserBioService.Execute(request);

                if (!result.IsSuccess)
                {
                    if (result.Message == "n")
                    {
                        return NotFound();
                    }
                    ViewBag.Message = result.Message;
                    return View("ShowMessage");
                }

            }

            return Redirect("/Im");
        }
    }
}
