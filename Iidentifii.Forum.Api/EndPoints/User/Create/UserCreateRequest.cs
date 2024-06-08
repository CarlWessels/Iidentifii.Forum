
namespace Iidentifii.Forum.Api.EndPoints.User.Create
{
    public class UserCreateRequest
    {
        public required Iidentifii.Forum.Library.Models.User User { get; set; }
        public required string Password { get; set; }
    }
}
