namespace Iidentifii.Forum.Api.EndPoints.Post.Create
{
    public class PostCreateRequest
    {
        public int SubforumId {get ;set; }
	    public required string Title { get; set; }
        public required string Content { get; set; }

    }
}
