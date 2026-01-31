using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;
using TrafficDesktopApp.Windows;

namespace TrafficDesktopApp.Controls.Users
{
    public partial class UsersListView : UserControl
    {
        // Lista de usuarios para la tabla
        public ObservableCollection<User> VisibleUsers { get; set; } = new ObservableCollection<User>();

        // Lista de roles para el ComboBox de la tabla
        public ObservableCollection<RoleResponse> AvailableRoles { get; set; } = new ObservableCollection<RoleResponse>();

        public UsersListView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetRoles(List<RoleResponse> roles)
        {
            AvailableRoles.Clear();
            if (roles != null)
            {
                foreach (var r in roles) AvailableRoles.Add(r);
            }
        }

        public async void LoadUsers()
        {
            var users = await UsersService.GetAllUsersAsync();
            SetUsers(users);
        }

        public void SetUsers(List<User> users)
        {
            VisibleUsers.Clear();
            if (users != null)
            {
                foreach (var user in users)
                {
                    user.SetAsOriginal();
                    VisibleUsers.Add(user);
                }
            }
        }

        private async void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.DataContext as User;
            if (user == null) return;

            // 1. Mapear del Modelo de UI (User) al Modelo de Request
            var request = new UserUpdateRequest
            {
                Email = user.Email,
                NombreCompleto = user.NombreCompleto,
                Rol = user.Rol // Envía el string del rol (ej: "ADMIN")
            };

            // 2. Llamar al servicio
            // IMPORTANTE: Ahora el servicio devuelve un bool, no un objeto con .Success
            bool success = await UsersService.UpdateUserAsync(request);

            if (success)
            {
                user.SetAsOriginal();
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            else
            {
                MessageBox.Show("Error al actualizar el usuario. Verifique la conexión o los datos.");
            }
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            // Pasamos una lista nueva basada en la colección observable
            var rolesList = new List<RoleResponse>(AvailableRoles);
            
            var modal = new CreateUserWindow(rolesList)
            {
                Owner = Window.GetWindow(this)
            };

            if (modal.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }
    }
}