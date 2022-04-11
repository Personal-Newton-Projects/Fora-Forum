using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class UpdateThreadModel
    {
        public int ThreadId { get; set; }
        public string NewName { get; set; }
        public string? NewBody { get; set; }
    }
}
