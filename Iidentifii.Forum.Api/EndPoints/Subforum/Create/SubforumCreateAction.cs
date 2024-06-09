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
