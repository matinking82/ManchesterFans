using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.SiteViewModels.Page
{
    public class SinglePageForSiteViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int Visits { get; set; }
        public int Likes { get; set; }
        public DateTime CreateDate { get; set; }

        public string GroupName { get; set; }
        public int GroupId { get; set; }

    }
}
