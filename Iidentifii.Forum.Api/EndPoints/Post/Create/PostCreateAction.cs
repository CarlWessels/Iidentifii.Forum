using FastEndpoints;
using Iidentifii.Forum.Api.EndPoints.User.Create;
using Iidentifii.Forum.Library;
using Iidentifii.Forum.Library.Posts;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Iidentifii.Forum.Api.EndPoints.Post.Create
{
    public class PostCreateAction : BaseAction<PostCreateRequest, PostCreateResponse>
    {
        private IPostService _postService;

        public PostCreateAction(IPostService postService)
        {
            _postService = postService;
        }

        public override void Configure()
        {
            Post("/api/post/create");
            Roles("User", "Moderator", "Owner");
            Summary(s => {
                s.Summary = "Create a Post";
                s.Description = "This endpoint allows users, moderators and owners to create a new post in a subforum.";
                s.ExampleRequest = new PostCreateRequest { SubforumId = 2, Title = "Post Title", Content = "Post Content" };
                s.ResponseExamples[200] = new PostCreateResponse { Id = 123 };
                s.Responses[200] = "The post was created successfully.";
                s.Responses[403] = "The user is not authorized to perform this action.";
            });
        }

        public override async Task HandleAsyncImpl(PostCreateRequest req, CancellationToken ct)
        {
            var postId = _postService.Create(req.SubforumId, req.Title, req.Content, UserId);
            await SendAsync(new()
            {
                Id = postId
            }, cancellation: ct);
        }
    }
}
