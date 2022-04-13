using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class PostUserUpdateModel
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "New Username has to be atleast 3 characters long")]
        [MaxLength(20, ErrorMessage = "New Username is too long")]
        public string? NewName { get; set; }
        public bool BanUser { get; set; }
        public bool DeleteUser { get; set; }

    }
}
