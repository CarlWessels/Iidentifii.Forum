using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iidentifii.Forum.Api.EndPoints.POC
{
    public class POCAction : BaseAction<POCRequest, POCResponse>
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
