using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared.PostModels
{
    public class PostUserUpdateModel
    {
        public int Id { get; set; }
        public string? NewName { get; set; }
        public bool BanUser { get; set; }
        public bool DeleteUser { get; set; }

    }
}
