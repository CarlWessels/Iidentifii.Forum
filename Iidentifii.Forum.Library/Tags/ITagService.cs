using Iidentifii.Forum.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Tags
{
    public interface ITagService
    {
        void Create(int postId, int tagId, int userId);
        int CreateTagLU(string name, string? description);
        IEnumerable<TagLU>? GetTagLUs();
    }
}
