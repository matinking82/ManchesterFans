using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels.Users
{
    public class AdminsListViewModel
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
        public string image { get; set; }
    }
}
