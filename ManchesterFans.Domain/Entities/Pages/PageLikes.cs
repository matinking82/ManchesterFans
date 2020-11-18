using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManchesterFans.Domain.Entities.Pages
{
    public class PageLikes
    {
        [Key]
        public int LikeId { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime InsertTime { get; set; }

        public virtual Page Page { get; set; }
        public int PageId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public PageLikes()
        {

        }
    }
}
