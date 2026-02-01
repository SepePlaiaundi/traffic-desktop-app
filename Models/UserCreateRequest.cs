using Newtonsoft.Json;

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Objeto de solicitud para la creación de un nuevo usuario en el sistema.
    /// </summary>
    public class UserCreateRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("nombreCompleto")]
        public string NombreCompleto { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }
    }
}