using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Likes
{
    public interface ILikeService
    {
        void LikeOrUnlike(int postId, int userId);
    }
}
