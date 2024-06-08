using FastEndpoints;
using Iidentifii.Forum.Api.EndPoints.User.Create;
using Iidentifii.Forum.Library;
using Iidentifii.Forum.Library.Posts;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Iidentifii.Forum.Api.EndPoints.Post.Create
{
    public class Action : BaseAction<PostCreateRequest, PostCreateResponse>
    {
        private IPostService _postService;

        public Action(IPostService postService)
        {
            _postService = postService;
        }

        public override void Configure()
        {
            Post("/api/post/create");
            Roles("User", "Moderator");
        }
        public override async Task HandleAsync(PostCreateRequest req, CancellationToken ct)
        {
            try
            {
                var postId = _postService.CreatePost(req.SubforumId, req.Title, req.Content, UserId);
                await SendAsync(new()
                {
                    Id = postId
                }, cancellation: ct) ;
            }
            catch
            {
                await SendErrorsAsync(500, ct);
            }
        }
    }
}
