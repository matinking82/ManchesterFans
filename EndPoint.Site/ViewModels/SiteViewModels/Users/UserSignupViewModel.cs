using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.SiteViewModels.Users
{
    public class UserSignupViewModel
    {

        [Required]
        [Display(Name ="نام کاربری")]
        public string Username { get; set; }


        [Required]
        [Display(Name ="کلمه عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Display(Name ="تایید کلمه عبور")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
