using Iidentifii.Forum.Api.EndPoints.Like.LikeOrUnlike;
using Iidentifii.Forum.Library.Likes;

namespace Iidentifii.Forum.Api.EndPoints.Like.LikeOrUnlike
{
    public class LikeOrUnlikeAction : BaseAction<LikeOrUnlikeRequest, LikeOrUnlikeResponse>
    {
        private ILikeService _likeService;

        public LikeOrUnlikeAction(ILikeService likeService)
        {
            _likeService = likeService;
        }

        public override void Configure()
        {
            Put("/api/like/likeorunlike");
            Roles("Moderator", "User", "Owner");
            Summary(s => {
                s.Summary = "Like or Unlike a Post";
                s.Description = "This endpoint allows a user, moderator or owner to like or unlike a post.";
                s.ExampleRequest = new LikeOrUnlikeRequest { PostId = 123 };
                s.ResponseExamples[200] = new LikeOrUnlikeResponse { };
                s.Responses[200] = "The like/unlike operation was successful.";
                s.Responses[403] = "The user is not authorized to perform this action.";
            });
        }

        public override async Task HandleAsyncImpl(LikeOrUnlikeRequest req, CancellationToken ct)
        {
            _likeService.LikeOrUnlike(req.PostId, UserId);
            await SendAsync(new()
            {
            }, cancellation: ct);
        }
    }
}
