using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Ventana modal para el registro de nuevos usuarios administrativos en el sistema.
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow(List<RoleResponse> roles)
        {
            InitializeComponent();
            
            if (roles != null)
            {
                RoleBox.ItemsSource = roles;
                if (roles.Count > 0) RoleBox.SelectedIndex = 0;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Ya no cargamos roles aquí
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                ShowError("Todos los campos son obligatorios.");
                return;
            }

            var selectedRole = RoleBox.SelectedItem as RoleResponse;

            if (selectedRole == null)
            {
                ShowError("Seleccione un rol válido.");
                return;
            }

            SetLoading(true);

            var request = new UserCreateRequest
            {
                NombreCompleto = NameBox.Text,
                Email = EmailBox.Text,
                Password = PasswordBox.Password,
                // --- CORRECCIÓN AQUÍ ---
                // Usamos .Name en lugar de .Code
                Rol = selectedRole.Name
            };

            bool success = await UsersService.CreateUserAsync(request);

            SetLoading(false);

            if (success)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                ShowError("Error al crear el usuario. Verifique la conexión o si el email ya existe.");
            }
        }

        private void SetLoading(bool isLoading)
        {
            LoadingOverlay.Visibility = isLoading ? Visibility.Visible : Visibility.Collapsed;
            // Deshabilitamos los campos para evitar cambios mientras se procesa
            NameBox.IsEnabled = !isLoading;
            EmailBox.IsEnabled = !isLoading;
            RoleBox.IsEnabled = !isLoading;
            PasswordBox.IsEnabled = !isLoading;
            
            var sb = (Storyboard)FindResource("SpinAnimation");
            if (isLoading) sb.Begin();
            else sb.Stop();
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorBorder.Visibility = Visibility.Visible;
        }
    }
}