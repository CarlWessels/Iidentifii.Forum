
using Iidentifii.Forum.Api.EndPoints.Post.GetAll;
using Iidentifii.Forum.Library.Subforums;
using Iidentifii.Forum.Library.Subforums.Models;

namespace Iidentifii.Forum.Api.EndPoints.Subforum.Get
{
    public class SubformGetAction : BaseAction<SubformGetRequest, SubformGetResponse>
    {
        private ISubforumService _subforumService;

        public SubformGetAction(ISubforumService subforumService)
        {
            _subforumService = subforumService;
        }

        public override void Configure()
        {
            Get("/api/subforum/");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Get All Subforms";
                s.Description = "This endpoint retrieves all subforms.";
                s.ExampleRequest = new SubformGetRequest { PageNumber = 1, PageSize = 10 };
                s.ResponseExamples[200] = new SubformGetResponse
                {
                    Data = new List<SubforumView>()
                    {
                        new SubforumView()
                        {
                            Id = 123,
                            Name = "General discussions",
                            Description = "The default subform"
                        }
                    }
                };
                s.Responses[200] = "The posts were retrieved successfully.";
            });
        }
        public override async Task HandleAsyncImpl(SubformGetRequest req, CancellationToken ct)
        {
            var output = _subforumService.GetAll(req.PageNumber, req.PageSize);

            await SendAsync(new()
            {
                Data = output
            }, cancellation: ct);
        }
    }
}
