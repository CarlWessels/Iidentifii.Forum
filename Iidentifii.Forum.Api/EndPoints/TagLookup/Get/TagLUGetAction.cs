using Iidentifii.Forum.Api.EndPoints.TagLookup.CreateLU;
using Iidentifii.Forum.Library.Tags;

namespace Iidentifii.Forum.Api.EndPoints.TagLookup.Get
{
    public class TagLUGetAction : BaseAction<TagLUGetRequest, TagLUGetResponse>
    {
        private ITagService _tagService;

        public TagLUGetAction(ITagService tagService)
        {
            _tagService = tagService;
        }

        public override void Configure()
        {
            Get("/api/taglookup/get");
            AllowAnonymous();
        }
        public override async Task HandleAsyncImpl(TagLUGetRequest req, CancellationToken ct)
        {
            var tagLUs = _tagService.GetTagLUs();
            await SendAsync(new()
            {
                TagLookups = tagLUs
            }, cancellation: ct);
        }
    }
}
