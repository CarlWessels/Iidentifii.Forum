//using FastEndpoints;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Iidentifii.Forum.Api.EndPoints.POC
//{
//    public class POCAction : BaseAction<POCRequest, POCResponse>
//    {
//        public override void Configure()
//        {
//            Post("/api/POC/create");
//            Roles("Moderator");
//            Summary(s => {
//                s.Summary = "short summary goes here";
//                s.Description = "long description goes here";
//                s.ExampleRequest = new POCRequest { Id = 123 };
//                s.ResponseExamples[200] = new POCResponse {  };
//                s.Responses[200] = "ok response description goes here";
//                s.Responses[403] = "forbidden response description goes here";
//            });
//        }
    
//        public override async Task HandleAsyncImpl(POCRequest req, CancellationToken ct)
//        {
//            await SendAsync(new()
//            {
//            }, cancellation: ct);
//        }
//    }
//}
