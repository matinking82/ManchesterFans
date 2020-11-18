using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewModels.AdminViewModels.Page
{
    public class CreatePageViewModel
    {
        public string Title { get; set; }
        public string ShortDescribtion { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
        public int GroupId { get; set; }

    }
}
