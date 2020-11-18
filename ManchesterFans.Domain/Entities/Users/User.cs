using ManchesterFans.Domain.Entities.Common;
using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManchesterFans.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        [Key]
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string NickName { get; set; }
        public string image { get; set; }
        public string Bio { get; set; }


        public virtual IEnumerable<PageComments> PageComments { get; set; }

    }


}
