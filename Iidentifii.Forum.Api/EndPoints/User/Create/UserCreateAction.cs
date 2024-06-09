using FastEndpoints;
using Iidentifii.Forum.Library.Auth;

namespace Iidentifii.Forum.Api.EndPoints.User.Create
{
    public class UserCreateAction : BaseAction<UserCreateRequest, UserCreateResponse>
    {
        private IUserManager _userManager;

        public UserCreateAction(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override void Configure()
        {
            Post("/api/user/create");
            AllowAnonymous();
        }
       

        public override async Task HandleAsyncImpl(UserCreateRequest req, CancellationToken ct)
        {
            var userId = _userManager.Create(req.Name, req.Email, req.Password);

            await SendAsync(new()
            {
            }, cancellation: ct);
        }
    }
}
