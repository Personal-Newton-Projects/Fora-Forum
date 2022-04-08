using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class PostThreadModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int InterestId { get; set; }
        public int CreatorId { get; set; }
    }
}
