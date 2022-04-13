using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class PostMessageModel
    {
        public int PosterId { get; set; }
        public int ThreadId { get; set; }
        [Required]
        [MaxLength(360, ErrorMessage = "Message is too long")]
        [MinLength(20, ErrorMessage = "Message has to be atleast 20 characters long")]
        public string Message { get; set; }

    }
}
