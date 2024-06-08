using FastEndpoints;
using Iidentifii.Forum.Api.EndPoints.POC;
using Iidentifii.Forum.Library;

namespace Iidentifii.Forum.Api.EndPoints.User.Login
{
    public class Action : Endpoint<LoginRequest, LoginResponse>
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
            Post("/api/user/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            try
            {
                var user = _userManager.Login(req.Email, req.Password);
                if (user?.Id == null)
                    await SendErrorsAsync(404, ct);
                var token = _tokenService.GenerateToken(user!.Id!.Value, user!.Role);
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
