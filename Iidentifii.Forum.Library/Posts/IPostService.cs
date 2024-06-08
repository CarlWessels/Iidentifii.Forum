using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Posts
{
    public interface IPostService
    {
        public int CreatePost(int subforumId, string title, string content, int userId);
        
    }
}
