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
            Summary(s =>
            {
                s.Summary = "Get Post Details";
                s.Description = "This endpoint retrieves details of a specific post based on the filter options.";
                s.ExampleRequest = new PostGetRequest { PostId = 123 };
                s.ResponseExamples[200] = new PostGetResponse
                {
                    Data = new Library.Models.PostView()
                    {
                        Comments = new List<Library.Models.CommentView>()
                            {
                                new Library.Models.CommentView()
                                {
                                    CommentId = 345,
                                    Comment = "This is an example comment",
                                    CommentCreationDate = DateTime.Now,
                                    CommentUser = "Bob Anderson"
                                }

                            },
                        Content = "This is an example post",
                        PostUser = "Smithy Smithers",
                        LikeCount = 4,
                        PostCreationDate = DateTime.Now,
                        PostId = 123,
                        SubforumName = "General discussions",
                        Title = "This is an example topic"
                    }
                };
                s.Responses[200] = "The post details were retrieved successfully.";
                s.Responses[404] = "The requested post was not found.";
            });
        }

        public override async Task HandleAsyncImpl(PostGetRequest req, CancellationToken ct)
        {
            var output = _postService.Get(req.PostId);
            await SendAsync(new()
            {
                Data = output
            }, cancellation: ct);
        }
    }
}
