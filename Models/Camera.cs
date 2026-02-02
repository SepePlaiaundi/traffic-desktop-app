using Newtonsoft.Json;

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Representa una cámara de tráfico conectada al sistema de monitorización.
    /// </summary>
    public class Camera
    {
        // 1. JSON envía "nombre" -> XAML pide "CameraName"
        [JsonProperty("nombre")]
        public string CameraName { get; set; }

        // 2. JSON envía "carretera" -> XAML pide "Road"
        [JsonProperty("carretera")]
        public string Road { get; set; }

        // 3. JSON envía "urlImage" -> XAML pide "Image"
        // WPF es listo: si le pasas una URL string al ImageSource, él descarga la imagen solo.
        [JsonProperty("urlImage")]
        public string Image { get; set; }

        // Otros campos opcionales que venían en tu JSON
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("latitud")]
        public double? Latitude { get; set; }

        [JsonProperty("longitud")]
        public double? Longitude { get; set; }
    }
}