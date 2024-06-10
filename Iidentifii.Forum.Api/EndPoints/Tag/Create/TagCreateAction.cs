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
            Summary(s => {
                s.Summary = "Create a Tag";
                s.Description = "This endpoint allows moderators and owners to create a new tag.";
                s.ExampleRequest = new TagCreateRequest { PostId = 123, TagId = 2 };
                s.Responses[200] = "The tag was created successfully.";
                s.Responses[403] = "The user is not authorized to perform this action.";
            });
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
