using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class SessionDTO
    {
        public Guid IdUser { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? NameUser { get; set; }
        public string? LastnameUser { get; set; }
        public string? RoleName { get; set; }

        // mostrar los roles disponibles
        public List<ModuleDTO> AllowedModules { get; set; } = null!;
    }
}
