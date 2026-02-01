using System;
using System.Windows;
using System.Windows.Input;
using TrafficDesktopApp.Services; // Ensure you have this namespace

namespace TrafficDesktopApp.Windows
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // 1. Get inputs
            string email = TxtEmail.Text;
            string password = TxtPassword.Password; // PasswordBox uses .Password property

            // 2. Basic Validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Por favor, introduce email y contraseña.");
                return;
            }

            // 3. Disable UI while loading (optional but good practice)
            ToggleLoading(true);

            try
            {
                // 4. Call the Backend
                bool isSuccess = await UsersService.LoginAsync(email, password);

                if (isSuccess)
                {
                    // 5. SUCCESS: Open the main app window
                    var dashboard = new TrafficDesktopApp.Windows.Dashboard();
                    dashboard.Show();

                    // Close the login window
                    this.Close();
                }
                else
                {
                    ShowError("Credenciales incorrectas.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error de conexión: " + ex.Message);
            }
            finally
            {
                ToggleLoading(false);
            }
        }



        private void ShowError(string message)
        {
            GlobalToast.Show(message);
        }

        private void ToggleLoading(bool isLoading)
        {
            GlobalLoading.IsLoading = isLoading;
            this.Cursor = isLoading ? Cursors.Wait : Cursors.Arrow;
            this.IsEnabled = !isLoading;
        }
    }
}