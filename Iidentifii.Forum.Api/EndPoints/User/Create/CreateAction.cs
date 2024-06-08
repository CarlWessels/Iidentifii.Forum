using FastEndpoints;
using Iidentifii.Forum.Library;

namespace Iidentifii.Forum.Api.EndPoints.User.Create
{
    public class CreateAction : Endpoint<CreateRequest, CreateResponse>
    {
        private IUserManager _userManager;

        public CreateAction(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override void Configure()
        {
            Post("/api/user/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
        {

            try
            {
                var userId = _userManager.Create(req.User, req.Password);

                await SendAsync(new()
                {
                    Success = true
                }, cancellation: ct);
            }
            catch
            {
                await SendErrorsAsync(500, ct);
            }
        }
    }
}
