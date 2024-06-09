
namespace Iidentifii.Forum.Api.EndPoints.User.Create
{
    public class UserCreateRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
