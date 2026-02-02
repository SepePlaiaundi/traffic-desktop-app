using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrafficDesktopApp.Controls.Dashboard;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Lógica de interacción para Users.xaml
    /// </summary>
    /// <summary>
    /// Ventana administrativa para la gestión (visualización, creación y edición) de los usuarios del sistema.
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();

            // Asegúrate de que tu control Header tenga este método definido
            Header.SetActive("Users");

            LoadUsersData();
        }

        private async void LoadUsersData()
        {
            try
            {
                // CORRECCIÓN: El método en el servicio es GetAllUsersAsync
                var taskUsers = UsersService.GetAllUsersAsync();
                var taskRoles = UsersService.GetRolesAsync();

                await Task.WhenAll(taskUsers, taskRoles);

                var allUsers = taskUsers.Result;
                var roles = taskRoles.Result;

                if (roles != null)
                {
                    UsersList.SetRoles(roles);
                }

                if (allUsers != null)
                {
                    // Asegúrate de que tu UserControl 'UsersList' (UsersListView) tenga este método
                    UsersList.SetUsers(allUsers);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

    }
}