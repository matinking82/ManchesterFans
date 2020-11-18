using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels.Users
{
    public class EditUserViewModel
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
    }
}
