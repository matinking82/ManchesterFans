using ManchesterFans.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchesterFans.Domain.Entities.Pages
{
    public class PageGroup : BaseEntity
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual IEnumerable<Page> Pages { get; set; }

        public PageGroup()
        {

        }
    }
}
