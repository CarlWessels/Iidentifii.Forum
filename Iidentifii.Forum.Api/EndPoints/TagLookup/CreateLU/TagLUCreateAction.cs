using Iidentifii.Forum.Api.EndPoints.Tag.Create;
using Iidentifii.Forum.Library.Tags;

namespace Iidentifii.Forum.Api.EndPoints.TagLookup.CreateLU
{
    public class TagLUCreateAction : BaseAction<TagLUCreateRequest, TagLUCreateResponse>
    {
        private ITagService _tagService;

        public TagLUCreateAction(ITagService tagService)
        {
            _tagService = tagService;
        }

        public override void Configure()
        {
            Post("/api/taglookup/create");
            Roles("Owner");
        }
        public override async Task HandleAsync(TagLUCreateRequest req, CancellationToken ct)
        {
            try
            {
                _tagService.CreateTagLU(req.Name, req.Description);
                await SendAsync(new()
                {
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
