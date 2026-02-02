using System.Collections.Generic;
using System.Windows;
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

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorBorder.Visibility = Visibility.Visible;
        }
    }
}