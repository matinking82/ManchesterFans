using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManchesterFans.Domain.Entities.Site
{
    public class Header
    {
        [Key]
        public int Id { get; set; }
        public string SiteName { get; set; }

        public virtual IEnumerable<HeaderLinks> Links { get; set; }
        public Header()
        {

        }
    }
}
