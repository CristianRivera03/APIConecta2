using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class UpdatePermissionsDTO
    {
        public int RoleId { get; set; }

        public List<int> ModuleIds { get; set; } = new List<int>();
    }
}
