using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels.Page
{
    public class SliderPagesListViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string ImageName { get; set; }
        public int Visits { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public string GroupName { get; set; }
    }
}
