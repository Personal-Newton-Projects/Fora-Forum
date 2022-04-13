using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class UpdateUserInfoModel
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
    }
}
