using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class UserCreateDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string NameUser { get; set; } = null!;
        public string LastnameUser { get; set; } = null!;
    }
}
