using Newtonsoft.Json; // Asegúrate de tener este using

namespace TrafficDesktopApp.Models
{
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