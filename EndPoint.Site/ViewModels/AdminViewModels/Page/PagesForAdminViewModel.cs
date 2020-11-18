using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels
{
    public class PagesForAdminViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string ImageName { get; set; }
        public string Group { get; set; }
        public DateTime CreateDate { get; set; }
        public int Visits { get; set; }
        public int Likes { get; set; }
    }
}
