using FastEndpoints;
using Iidentifii.Forum.Api.EndPoints.POC;
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
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            try
            {
                var user = _userManager.Login(req.Email, req.Password);
                if (user?.Item1 == null)
                {
                    AddError("Incorrect username of password");
                    await SendErrorsAsync(404, ct);
                    return;
                }

                var token = _tokenService.GenerateToken(user!.Value.Item1, user!.Value.Item2);
                await SendAsync(new()
                {
                    Token = token.ToString(),
                }, cancellation: ct);
            }
            catch
            {
                await SendErrorsAsync(500, ct);
            }
        }
    }
}
