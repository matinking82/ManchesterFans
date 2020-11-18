using ManchesterFans.Domain.Entities.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Domain.Entities.Site
{
    public class SliderPosts
    {
        public int Id { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }


        public virtual Page Page { get; set; }
        public int PageID { get; set; }
        
        public SliderPosts()
        {

        }
    }
}
