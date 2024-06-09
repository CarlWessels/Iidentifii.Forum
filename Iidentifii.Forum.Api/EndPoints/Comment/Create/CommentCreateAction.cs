using Iidentifii.Forum.Api.EndPoints.Post.Create;
using Iidentifii.Forum.Library.Comments;
using Iidentifii.Forum.Library.Posts;

namespace Iidentifii.Forum.Api.EndPoints.Comment.Create
{
    public class CommentCreateAction : BaseAction<CommentCreateRequest, CommentCreateResponse>
    {
        private ICommentService _commentService;

        public CommentCreateAction(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public override void Configure()
        {
            Post("/api/comment/create");
            Roles("User", "Moderator");
        }
        public override async Task HandleAsync(CommentCreateRequest req, CancellationToken ct)
        {
            try
            {
                var commentId = _commentService.CreateComment(req.PostId, req.Comment, UserId);
                await SendAsync(new()
                {
                    Id  = commentId
                }, cancellation: ct);
            }
            catch
            {
                await SendErrorsAsync(500, ct);
            }
        }
    }
}
