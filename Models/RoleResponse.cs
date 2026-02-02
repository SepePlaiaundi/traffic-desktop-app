using Newtonsoft.Json;

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Representa un rol de usuario devuelto por la API.
    /// </summary>
    public class RoleResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; } // Este será el SelectedValuePath

        [JsonProperty("description")]
        public string Description { get; set; } // Este será el DisplayMemberPath
    }
}