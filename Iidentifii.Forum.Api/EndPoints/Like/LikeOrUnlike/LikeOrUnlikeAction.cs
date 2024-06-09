using Iidentifii.Forum.Api.EndPoints.POC;
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
