using Iidentifii.Forum.Library.Models;
using Iidentifii.Forum.Library.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Posts
{
    public interface IPostService
    {
        public int Create(int subforumId, string title, string content, int userId);
        PostView? Get(int postId);
        List<PostView>? GetAll(int pageNumber, int pageSize, PostSortingOptions? sortingOptions, SortingDirection sortingDirection, PostFilterOptions? postFilterOptions);
    }
}
