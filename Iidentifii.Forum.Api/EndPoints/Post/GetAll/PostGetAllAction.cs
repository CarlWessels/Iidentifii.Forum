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
            Summary(s =>
            {
                s.Summary = "Get All Posts";
                s.Description = "This endpoint retrieves all posts based on the filter options.";
                s.ExampleRequest = new PostGetAllRequest { PageNumber = 1, PageSize = 10 };
                s.ResponseExamples[200] = new PostGetAllResponse
                {
                    Data = new List<Library.Models.PostView>
                    {
                        new Library.Models.PostView()
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
                        },
                        new Library.Models.PostView()
                        {
                            Comments = new List<Library.Models.CommentView>()
                            {
                                new Library.Models.CommentView()
                                {
                                    CommentId = 345,
                                    Comment = "This is yet another example comment",
                                    CommentCreationDate = DateTime.Now,
                                    CommentUser = "Koos Wessels"
                                }

                            },
                            Content = "This is another example post",
                            PostUser = "Philip Hein",
                            LikeCount = 4,
                            PostCreationDate = DateTime.Now,
                            PostId = 321,
                            SubforumName = "General discussions",
                            Title = "This is another example topic"
                        }
                    }
                };
                s.Responses[200] = "The posts were retrieved successfully.";
            });
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
