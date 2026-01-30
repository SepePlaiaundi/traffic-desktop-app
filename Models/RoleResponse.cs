using Newtonsoft.Json;

namespace TrafficDesktopApp.Models
{
    public class RoleResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; } // Este será el SelectedValuePath

        [JsonProperty("description")]
        public string Description { get; set; } // Este será el DisplayMemberPath
    }
}