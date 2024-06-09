
using Iidentifii.Forum.Library.Auth;

namespace Iidentifii.Forum.Api.EndPoints.User.CreateModerator
{
    public class UserCreateModeratorAction : BaseAction<UserCreateModeratorRequest, UserCreateModeratorResponse>
    {
        private readonly IUserManager _userManager;

        public UserCreateModeratorAction(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override void Configure()
        {
            Put("/api/user/createmoderator");
            Roles("Owner");
        }

        public override async Task HandleAsyncImpl(UserCreateModeratorRequest req, CancellationToken ct)
        {
            _userManager.CreateModerator(req.UserId);

            await SendAsync(new()
            {
            }, cancellation: ct);
        }
    }
}
