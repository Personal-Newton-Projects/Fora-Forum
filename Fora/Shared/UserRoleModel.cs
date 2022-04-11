using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared
{
    public class UserRoleModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int RoleId { get; set; } = 1;
        public RoleModel Role { get; set; }
    }
}
