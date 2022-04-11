using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared
{
    public enum RoleEnum
    {
        USER,
        ADMIN
    }
    public class RoleModel
    {
        public int Id { get; set; }
        public RoleEnum Role { get; set; }

        public List<UserRoleModel> UserRoles { get; set; }

    }
}
