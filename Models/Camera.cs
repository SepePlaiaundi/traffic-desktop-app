using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrafficDesktopApp.Models
{
    public class Camera : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _estado;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string CameraName { get; set; }

        [JsonProperty("carretera")]
        public string Road { get; set; }

        [JsonProperty("urlImage")]
        public string Image { get; set; }

        [JsonProperty("latitud")]
        public double? Latitude { get; set; }

        [JsonProperty("longitud")]
        public double? Longitude { get; set; }

        [JsonProperty("estado")]
        public string Estado
        {
            get => _estado;
            set
            {
                _estado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsActive)); // Important: Notify IsActive changed too
            }
        }

        [JsonIgnore]
        public bool IsActive
        {
            get => Estado == "ACTIVA";
            set => Estado = value ? "ACTIVA" : "INACTIVA";
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}