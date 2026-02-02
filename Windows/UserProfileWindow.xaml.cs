using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    public partial class UserProfileWindow : Window
    {
        private User _currentUser;

        public UserProfileWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentUser = await UsersService.GetMyProfileAsync();
                if (_currentUser != null)
                {
                    NameBox.Text = _currentUser.NombreCompleto;
                    EmailBox.Text = _currentUser.Email;
                    AvatarUrlBox.Text = _currentUser.Avatar;
                    UpdateAvatarPreview(_currentUser.Avatar);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error al cargar el perfil: " + ex.Message);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void SelectAvatar_Click(object sender, RoutedEventArgs e)
        {
            // Opcional: podrías abrir un OpenFileDialog aquí, subirlo a un hosting y obtener la URL
            MessageBox.Show("Por favor, introduce una URL de imagen en el campo de texto para cambiar tu avatar.", "Cambiar Avatar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AvatarUrlBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateAvatarPreview(AvatarUrlBox.Text);
        }

        private void UpdateAvatarPreview(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                AvatarImage.Visibility = Visibility.Collapsed;
                AvatarInitials.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    AvatarImage.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                    AvatarImage.Visibility = Visibility.Visible;
                    AvatarInitials.Visibility = Visibility.Collapsed;
                }
                catch
                {
                    AvatarImage.Visibility = Visibility.Collapsed;
                    AvatarInitials.Visibility = Visibility.Visible;
                }
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            ErrorBorder.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                ShowError("El nombre completo es obligatorio.");
                return;
            }

            var request = new UserProfileUpdateRequest
            {
                Email = EmailBox.Text,
                NombreCompleto = NameBox.Text,
                Avatar = AvatarUrlBox.Text,
                Password = PasswordBox.Password.Length > 0 ? PasswordBox.Password : null
            };

            try
            {
                bool success = await UsersService.UpdateMyProfileAsync(request);
                if (success)
                {
                    MessageBox.Show("Perfil actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    ShowError("No se pudo actualizar el perfil. Verifica los datos.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error al conectar con el servidor: " + ex.Message);
            }
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorBorder.Visibility = Visibility.Visible;
        }
    }
}
