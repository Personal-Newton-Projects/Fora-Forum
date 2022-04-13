using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared
{
    public class LoginModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username has to be atleast 3 characters long")]
        [MaxLength(20, ErrorMessage = "Username too long")]
        public string Username { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Password has to be atleast 3 characters long")]
        [MaxLength(100, ErrorMessage = "Password too long")]
        public string Password { get; set; }
    }
}
