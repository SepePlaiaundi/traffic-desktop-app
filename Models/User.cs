using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Representa un usuario del sistema con soporte para notificación de cambios.
    /// </summary>
    public class User : INotifyPropertyChanged
    {
        private string _email;
        private string _nombreCompleto;
        private string _rol;
<<<<<<< HEAD
        private string _avatar;
=======
>>>>>>> af2a4adbe1ddbf3ffd6114b99aa7f8eba957d4c0

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

<<<<<<< HEAD
        [JsonProperty("avatar")]
        public string Avatar
        {
            get => _avatar;
            set { _avatar = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasChanges)); }
        }

        private string _originalEmail;
        private string _originalNombreCompleto;
        private string _originalRol;
        private string _originalAvatar;
=======
        private string _originalEmail;
        private string _originalNombreCompleto;
        private string _originalRol;
>>>>>>> af2a4adbe1ddbf3ffd6114b99aa7f8eba957d4c0

        /// <summary>
        /// Guarda el estado actual de las propiedades como los valores originales para detectar cambios futuros.
        /// </summary>
        public void SetAsOriginal()
        {
            _originalEmail = Email;
            _originalNombreCompleto = NombreCompleto;
            _originalRol = Rol;
<<<<<<< HEAD
            _originalAvatar = Avatar;
=======
>>>>>>> af2a4adbe1ddbf3ffd6114b99aa7f8eba957d4c0
            OnPropertyChanged(nameof(HasChanges));
        }

        /// <summary>
        /// Indica si alguna de las propiedades editables ha cambiado respecto a su valor original.
        /// </summary>
        [JsonIgnore]
        public bool HasChanges => 
            Email != _originalEmail || 
            NombreCompleto != _originalNombreCompleto || 
<<<<<<< HEAD
            Rol != _originalRol ||
            Avatar != _originalAvatar;
=======
            Rol != _originalRol;
>>>>>>> af2a4adbe1ddbf3ffd6114b99aa7f8eba957d4c0

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}