using Iidentifii.Forum.Api.EndPoints.Comment.Create;
using Iidentifii.Forum.Library.Posts;

namespace Iidentifii.Forum.Api.EndPoints.Post.GetAll
{
    public class PostGetAllAction : BaseAction<PostGetAllRequest, PostGetAllResponse>
    {
        private IPostService _postService;

        public PostGetAllAction(IPostService postService)
        {
            _postService = postService;
        }

        public override void Configure()
        {
            Get("/api/post/");
            AllowAnonymous();
        }

        public override async Task HandleAsyncImpl(PostGetAllRequest req, CancellationToken ct)
        {
            var output = _postService.GetAll(req.PageNumber, req.PageSize, req.SortingOptions, req.SortingDirection, req.FilterOptions);

            await SendAsync(new()
            {
                Data = output
            }, cancellation: ct);
        }
    }
}
