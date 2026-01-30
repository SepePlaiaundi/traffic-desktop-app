using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Controls.Users
{
    public partial class UsersListView : UserControl
    {
        // Lista de usuarios para la tabla
        public ObservableCollection<User> VisibleUsers { get; set; } = new ObservableCollection<User>();

        // Lista de roles disponibles traída del backend
        public ObservableCollection<RoleResponse> AvailableRoles { get; set; } = new ObservableCollection<RoleResponse>();

        public UsersListView()
        {
            InitializeComponent();
            DataContext = this;

            // Cargar datos al iniciar
            LoadRoles();
            LoadUsers(); // <--- IMPORTANTE: Cargar los usuarios
        }

        private async void LoadRoles()
        {
            var roles = await UsersService.GetRolesAsync();
            AvailableRoles.Clear();
            if (roles != null)
            {
                foreach (var r in roles) AvailableRoles.Add(r);
            }
        }

        // --- ESTE MÉTODO ES EL QUE FALTABA O SE LLAMABA MAL ---
        private async void LoadUsers()
        {
            // Nota: En el servicio se llama GetAllUsersAsync, no GetUsersAsync
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
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            else
            {
                MessageBox.Show("Error al actualizar el usuario. Verifique la conexión o los datos.");
            }
        }
    }
}