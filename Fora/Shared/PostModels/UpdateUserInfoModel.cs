using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class UpdateUserInfoModel
    {
        public int Id { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public string VerifyNewPassword { get; set; }
        [Required]
        public string OldPassword { get; set; }
    }
}
