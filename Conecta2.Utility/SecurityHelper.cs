using System;
using System.Collections.Generic;
using System.Text;

using BCrypt.Net;

namespace Conecta2.Utility
{
    public static class SecurityHelper
    {
        //creacion de usuario
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //inicio de sesion
        public static bool VerifyPassword(string password , string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password , hashedPassword);
        }
    }
}
