using ManchesterFans.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ManchesterFans.Domain.Entities.Site
{
    public class HeaderLinks : BaseEntity
    {
        [Key]
        public int LinkId { get; set; }
        public string DisplayText { get; set; }
        public string Url { get; set; }

        public virtual Header Header { get; set; }
        public int Id { get; set; }

        public HeaderLinks()
        {

        }
    }
}
