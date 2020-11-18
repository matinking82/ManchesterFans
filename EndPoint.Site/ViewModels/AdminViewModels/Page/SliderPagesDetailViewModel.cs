using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels.Page
{
    public class SliderPagesDetailViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
        public int Visits { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public int GroupName { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
