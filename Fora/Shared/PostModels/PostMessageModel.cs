using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class PostMessageModel
    {
        public int PosterId { get; set; }
        public int ThreadId { get; set; }
        public string Message { get; set; }

    }
}
