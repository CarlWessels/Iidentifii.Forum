using Iidentifii.Forum.Library.Subforums.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Subforums
{
    public interface ISubforumService
    {
        public int Create(string name, string description);
        List<SubforumView>? GetAll(int pageNumber, int pageSize);
    }
}
