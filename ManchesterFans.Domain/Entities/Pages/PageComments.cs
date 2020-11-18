using ManchesterFans.Domain.Entities.Common;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchesterFans.Domain.Entities.Pages
{
    public class PageComments : BaseEntity
    {
        [Key]
        public int CommentId{ get; set; }
        public string Comment { get; set; }
        public bool IsAccepted { get; set; }

        public virtual PageComments Parent { get; set; }
        public int Reply { get; set; }

        public int Like { get; set; }

        public virtual Page Page { get; set; }
        public int PageId{ get; set; }

        public virtual User User { get; set; }
        public int UserId{ get; set; }

        public virtual IEnumerable<PageComments> RepllyComments { get; set; }

        public PageComments()
        {

        }
    }
}
