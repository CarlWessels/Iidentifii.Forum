using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Models
{
    public class PostView
    {
        public int PostId { get; set; }
        public required string SubforumName { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string PostUser { get; set; }
        public DateTime PostCreationDate { get; set; }
        public int LikeCount { get; set; }
        public List<CommentVoew> Comments { get; set; } = new();
    }

    public class CommentVoew
    {
        public int CommentId { get; set; }
        public required string Comment { get; set; }
        public required string CommentUser { get; set; }
        public DateTime CommentCreationDate { get; set; }
    }

}
