using System.Collections.Generic;
using System.Windows;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<RoleResponse> roles = await UsersService.GetRolesAsync();
                RoleBox.ItemsSource = roles;

                // --- CORRECCIÓN AQUÍ ---
                // Usamos las propiedades que coinciden con el Backend Java
                RoleBox.DisplayMemberPath = "Description"; // Lo que ve el usuario (ej: "Administrador")
                RoleBox.SelectedValuePath = "Name";        // El valor interno (ej: "ADMIN")

                if (roles.Count > 0) RoleBox.SelectedIndex = 0;
            }
            catch
            {
                ShowError("No se pudieron cargar los roles.");
            }
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
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}