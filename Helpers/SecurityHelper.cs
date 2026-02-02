using System;
using System.Security.Cryptography;
using System.Text;

namespace TrafficDesktopApp.Helpers
{
    /// <summary>
    /// Proporciona utilidades de seguridad, como el hasheo de contraseñas.
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// Genera un hash SHA-256 de la cadena proporcionada.
        /// </summary>
        /// <param name="password">La contraseña en texto plano.</param>
        /// <returns>El hash en formato hexadecimal.</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                
                return builder.ToString();
            }
        }
    }
}
