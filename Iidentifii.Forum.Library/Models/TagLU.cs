using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Models
{
    public class TagLU
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

    }
}
