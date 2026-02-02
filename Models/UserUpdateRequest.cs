using Newtonsoft.Json; // Asegúrate de tener este using

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Objeto de solicitud para la actualización de un usuario existente.
    /// </summary>
    public class UserUpdateRequest
    {
        [JsonProperty("email")] // <--- ESTO ES VITAL
        public string Email { get; set; }

        [JsonProperty("nombreCompleto")]
        public string NombreCompleto { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }
    }
}