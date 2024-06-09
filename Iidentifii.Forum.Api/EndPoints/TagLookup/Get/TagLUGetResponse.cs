using Iidentifii.Forum.Library.Models;

namespace Iidentifii.Forum.Api.EndPoints.TagLookup.Get
{
    public class TagLUGetResponse : BaseResponse
    {
        public IEnumerable<TagLU>? TagLookups { get; set; }
    }
}
