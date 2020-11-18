using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.SiteViewModels.Page
{
    public class AddPageCommentsViewModel
    {
        public string Comment { get; set; }
        public int Reply { get; set; }
        public int PageId { get; set; }
    }
}
