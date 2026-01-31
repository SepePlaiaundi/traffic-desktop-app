using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrafficDesktopApp.Models
{
    public class User : INotifyPropertyChanged
    {
        private string _email;
        private string _nombreCompleto;
        private string _rol;

        [JsonProperty("email")]
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasChanges)); }
        }

        [JsonProperty("nombreCompleto")]
        public string NombreCompleto
        {
            get => _nombreCompleto;
            set { _nombreCompleto = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasChanges)); }
        }

        [JsonProperty("rol")]
        public string Rol
        {
            get => _rol;
            set { _rol = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasChanges)); }
        }

        private string _originalEmail;
        private string _originalNombreCompleto;
        private string _originalRol;

        public void SetAsOriginal()
        {
            _originalEmail = Email;
            _originalNombreCompleto = NombreCompleto;
            _originalRol = Rol;
            OnPropertyChanged(nameof(HasChanges));
        }

        [JsonIgnore]
        public bool HasChanges => 
            Email != _originalEmail || 
            NombreCompleto != _originalNombreCompleto || 
            Rol != _originalRol;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}