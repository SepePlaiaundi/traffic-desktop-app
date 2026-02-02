using Newtonsoft.Json;

namespace TrafficDesktopApp.Models
{
    public class UserProfileUpdateRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("nombreCompleto")]
        public string NombreCompleto { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}
