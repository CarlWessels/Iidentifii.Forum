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
            Summary(s => {
                s.Summary = "Create a Tag Lookup Entry";
                s.Description = "This endpoint allows owners to create a new entry in the tag lookup table.";
                s.ExampleRequest = new TagLUCreateRequest { Name = "Tag Name", Description = "Tag Description" };
                s.Responses[200] = "The tag lookup entry was created successfully.";
                s.Responses[403] = "The user is not authorized to perform this action.";
            });
        }

        public override async Task HandleAsyncImpl(TagLUCreateRequest req, CancellationToken ct)
        {
            _tagService.CreateTagLU(req.Name, req.Description);
            await SendAsync(new()
            {
            }, cancellation: ct);
        }
    }
}
