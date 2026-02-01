using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Respuesta del servidor tras un inicio de sesión exitoso, conteniendo el token JWT y el rol del usuario.
    /// </summary>
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}