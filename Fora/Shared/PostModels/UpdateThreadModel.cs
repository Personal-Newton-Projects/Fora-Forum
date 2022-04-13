using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class UpdateThreadModel
    {


        public int ThreadId { get; set; }
        [MinLength(5, ErrorMessage = "Title has to be atleast 5 characters long")]
        public string? NewName { get; set; }
        [MinLength(20, ErrorMessage = "Post Body has to be atleast 20 characters long")]
        public string? NewBody { get; set; }
        public bool RemoveThread { get; set; }
    }
}
