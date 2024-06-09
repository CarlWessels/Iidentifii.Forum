namespace Iidentifii.Forum.Api.EndPoints.User.Create
{
    public class UserCreateResponse : BaseResponse
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
