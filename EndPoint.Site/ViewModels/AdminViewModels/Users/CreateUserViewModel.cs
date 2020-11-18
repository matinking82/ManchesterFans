using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels.Users
{
    public class CreateUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
    }
}
