using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Posts.Models
{
    public class PostFilterOptions
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AuthorId { get; set; }
        public int? TagId { get; set; }
    }
}
