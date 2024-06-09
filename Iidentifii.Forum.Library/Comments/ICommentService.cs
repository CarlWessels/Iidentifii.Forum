using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Comments
{
    public interface ICommentService
    {
        int CreateComment(int postId, string comment, int userId);
    }
}
