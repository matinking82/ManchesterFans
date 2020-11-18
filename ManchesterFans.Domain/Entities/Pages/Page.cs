using ManchesterFans.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManchesterFans.Domain.Entities.Pages
{
    public class Page : BaseEntity
    {
        [Key]
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
        public int Visits { get; set; } = 0;
        public int Likes { get; set; } = 0;


        public virtual PageGroup PageGroup { get; set; }
        public int GroupId { get; set; }

        public virtual IEnumerable<PageComments> PageComments { get; set; }

        public Page()
        {

        }
    }
}
