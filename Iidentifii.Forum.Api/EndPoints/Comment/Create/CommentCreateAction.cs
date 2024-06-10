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
            Roles("User", "Moderator", "Owner");
            Summary(s => {
                s.Summary = "Create a Comment";
                s.Description = "This endpoint allows users, moderators, and owners to create a comment on a post.";
                s.ExampleRequest = new CommentCreateRequest { PostId = 123, Comment = "Comment text" };
                s.ResponseExamples[200] = new CommentCreateResponse { Id = 456 };
                s.Responses[200] = "The comment was created successfully.";
                s.Responses[403] = "The user is not authorized to perform this action.";
            });
        }

        public override async Task HandleAsyncImpl(CommentCreateRequest req, CancellationToken ct)
        {
            var commentId = _commentService.CreateComment(req.PostId, req.Comment, UserId);
            await SendAsync(new()
            {
                Id = commentId
            }, cancellation: ct);
        }
    }
}
