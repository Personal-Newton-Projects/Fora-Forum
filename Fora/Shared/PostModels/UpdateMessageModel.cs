using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class UpdateMessageModel
    {
        public int ThreadId { get; set; }
        public int MessageId { get; set; }
        [Required]
        [MinLength(20, ErrorMessage="Message has to be atleast 20 characters long")]
        [MaxLength(360, ErrorMessage="Message is too long")]
        public string NewMessage { get; set; }
        public bool RemoveMessage { get; set; }
    }
}
