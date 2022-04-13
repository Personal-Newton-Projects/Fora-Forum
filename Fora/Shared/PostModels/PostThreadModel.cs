using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class PostThreadModel
    {

        [Required]
        [MinLength(5, ErrorMessage = "Title has to be atleast 5 characters long")]
        public string Title { get; set; }
        [Required]
        [MinLength(20, ErrorMessage = "Post Body has to be atleast 20 characters long")]
        public string Description { get; set; }
        public int InterestId { get; set; }
        public int CreatorId { get; set; }
    }
}
