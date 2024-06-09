using Iidentifii.Forum.Api.EndPoints.User.Create;
using Iidentifii.Forum.Library.Posts;

namespace Iidentifii.Forum.Api.EndPoints.Post.Get
{
    public class PostGetAction : BaseAction<PostGetRequest, PostGetResponse>
    {
        private IPostService _postService;

        public PostGetAction(IPostService postService)
        {
            _postService = postService;
        }

        public override void Configure()
        {
            Get("/api/post/{PostId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PostGetRequest req, CancellationToken ct)
        {
            try
            {
                var output = _postService.Get(req.PostId, req.PageNumber, req.PageSize, req.SortingOptions, req.SortingDirection, req.FilterOptions);

                await SendAsync(new()
                {
                    Data = output
                }, cancellation: ct);
            }
            catch
            {
                await SendErrorsAsync(500, ct);
            }
        }
    }
}
