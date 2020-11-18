using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.SiteViewModels.Page
{
    public class SinglePageForSiteCardsViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }
        public string ShortDescribtion { get; set; }
        public int Likes { get; set; }
        public int Visits { get; set; }
        public string CreateDate { get; set; }
        public string ImageName { get; set; }
        public int GroupId { get; set; }
    }
}
