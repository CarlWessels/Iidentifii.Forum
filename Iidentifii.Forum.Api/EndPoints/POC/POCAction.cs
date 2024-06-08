using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace Iidentifii.Forum.Api.EndPoints.POC
{
    //[Authorize(Roles = "User")]
    public class POCAction : Endpoint<POCRequest, POCResponse>
    {
        public override void Configure()
        {
            Post("/api/POC/create");
            Roles("Moderator");
        }
    

        public override async Task HandleAsync(POCRequest req, CancellationToken ct)
        {
            await SendAsync(new()
            {
            }, cancellation: ct);
        }
    }
}
