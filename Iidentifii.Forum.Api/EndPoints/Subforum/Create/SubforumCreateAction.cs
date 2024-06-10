using Iidentifii.Forum.Api.EndPoints.User.Create;
using Iidentifii.Forum.Library.Auth;
using Iidentifii.Forum.Library.Subforums;

namespace Iidentifii.Forum.Api.EndPoints.Subforum.Create
{
    public class SubforumCreateAction : BaseAction<SubforumCreateRequest, SubforumCreateResponse>
    {
        private ISubforumService _subformService;
        public SubforumCreateAction(ISubforumService subformService)
        {
            this._subformService = subformService;
        }

        public override void Configure()
        {
            Post("/api/subforum/create");
            Roles("Moderator", "Owner");
            Summary(s => {
                s.Summary = "Create a Subforum";
                s.Description = "This endpoint allows moderators and owners to create a new subforum.";
                s.ExampleRequest = new SubforumCreateRequest { Name = "Subforum Name", Description = "Subforum Description" };
                s.ResponseExamples[200] = new SubforumCreateResponse { Id = 3 };
                s.Responses[200] = "The subforum was created successfully.";
                s.Responses[403] = "The user is not authorized to perform this action.";
                s.Responses[500] = "An error occurred while processing the request.";
            });
        }

        public override async Task HandleAsyncImpl(SubforumCreateRequest req, CancellationToken ct)
        {
            try
            {
                var subforumId = _subformService.Create(req.Name, req.Description);

                await SendAsync(new()
                {
                    Id = subforumId
                }, cancellation: ct);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                await SendErrorsAsync(500, ct);
            }
        }
    }
}
