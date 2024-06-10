using FastEndpoints;
using Iidentifii.Forum.Library.Auth;

namespace Iidentifii.Forum.Api.EndPoints.User.Login
{
    public class Action : BaseAction<LoginRequest, LoginResponse>
    {
        private IUserManager _userManager;
        private ITokenService _tokenService;

        public Action(IUserManager userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public override void Configure()
        {
            Get("/api/user/login");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "User Login";
                s.Description = "This endpoint allows users to log in and obtain an authentication token.";
                s.ExampleRequest = new LoginRequest { Email = "user@example.com", Password = "password123" };
                s.Responses[200] = "The user was logged in successfully.";
                s.Responses[404] = "Incorrect username or password.";
            });
        }

        public override async Task HandleAsyncImpl(LoginRequest req, CancellationToken ct)
        {
            var user = _userManager.Login(req.Email, req.Password);
            if (user?.Item1 == null)
            {
                AddError("Incorrect username or password");
                await SendErrorsAsync(404, ct);
                return;
            }

            var token = _tokenService.GenerateToken(user!.Value.Item1, user!.Value.Item2);
            await SendAsync(new()
            {
                Token = token.ToString(),
            }, cancellation: ct);
        }
    }
}
