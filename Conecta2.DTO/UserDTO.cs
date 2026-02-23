using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class UserDTO
    {
        public Guid IdUser { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? NameUser { get; set; }

        public string? LastnameUser { get; set; }

        public bool? IsActive { get; set; }
    }
}
