using FastEndpoints;

namespace Iidentifii.Forum.Api.EndPoints.POC
{
    public class Action : Endpoint<Request, Response>
    {
        public override void Configure()
        {
            Post("/api/POC/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            await SendAsync(new()
            {
            }, cancellation: ct);
        }
    }
}
