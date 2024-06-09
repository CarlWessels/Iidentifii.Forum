namespace Iidentifii.Forum.Api.EndPoints.Comment.Create
{
    public class CommentCreateRequest
    {
        public int PostId { get; set; }
        public required string Comment { get; set; }

    }
}
