using FastEndpoints;

namespace Iidentifii.Forum.Api.EndPoints.POC
{
    public class POCSummary : EndpointSummary
    {
        public POCSummary()
        {
            Summary = "short summary goes here";
            Description = "long description goes here";
            ExampleRequest = new POCRequest { Id =1234};
            Responses[200] = "success response description goes here";
            Responses[403] = "forbidden response description goes here";
        }
    }
}
