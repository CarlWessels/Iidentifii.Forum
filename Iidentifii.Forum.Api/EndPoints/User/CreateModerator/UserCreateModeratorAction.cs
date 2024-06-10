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
            Summary(s => {
                s.Summary = "Create a Moderator";
                s.Description = "This endpoint allows owners to promote a user to moderator status.";
                s.ExampleRequest = new UserCreateModeratorRequest { UserId = 3 };
                s.Responses[200] = "The user was promoted to moderator successfully.";
                s.Responses[403] = "The user is not authorized to perform this action.";
            });
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
