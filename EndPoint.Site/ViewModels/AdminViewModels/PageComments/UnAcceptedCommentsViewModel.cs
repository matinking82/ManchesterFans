using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels.PageComments
{
    public class UnAcceptedCommentsViewModel
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public string PageImage { get; set; }
        public string PageTitle { get; set; }
        public int PageId { get; set; }
        public string InsertTime { get; set; }
    }
}
