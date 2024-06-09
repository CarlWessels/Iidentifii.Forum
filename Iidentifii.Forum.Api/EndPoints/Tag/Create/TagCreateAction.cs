using Iidentifii.Forum.Api.EndPoints.Subforum.Create;
using Iidentifii.Forum.Library.Tags;

namespace Iidentifii.Forum.Api.EndPoints.Tag.Create
{
    public class TagCreateAction : BaseAction<TagCreateRequest, TagCreateResponse>
    {
        private ITagService _tagService;

        public TagCreateAction(ITagService tagService)
        {
            _tagService = tagService;
        }

        public override void Configure()
        {
            Post("/api/tag/create");
            Roles("Moderator", "Owner");
        }

        public override async Task HandleAsyncImpl(TagCreateRequest req, CancellationToken ct)
        {
            _tagService.Create(req.PostId, req.TagId, UserId);

            await SendAsync(new()
            {
            }, cancellation: ct);
        }
    }
}
