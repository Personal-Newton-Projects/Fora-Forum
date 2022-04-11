using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class UpdateMessageModel
    {
        public int ThreadId { get; set; }
        public int MessageId { get; set; }
        public string NewMessage { get; set; }
    }
}
