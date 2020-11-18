using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.SiteViewModels.Page
{
    public class PageCommentsViewModel
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }

        public IEnumerable<PageCommentsViewModel> ReplyComments { get; set; }

    }
}
