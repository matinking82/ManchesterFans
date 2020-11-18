using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.SiteViewModels.Site
{
    public class SiteHeaderViewModel
    {
        public string SiteName { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }

    public class Link
    {
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
