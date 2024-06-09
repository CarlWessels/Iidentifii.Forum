using Iidentifii.Forum.Library.Posts.Models;

namespace Iidentifii.Forum.Api.EndPoints.Post.GetAll
{
    public class PostGetAllRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PostSortingOptions? SortingOptions { get; set; }
        public SortingDirection SortingDirection { get; set; }
        public PostFilterOptions? FilterOptions { get; set; }
    }
}
