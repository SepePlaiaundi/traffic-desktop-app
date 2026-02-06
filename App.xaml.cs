using System;
using System.Threading.Tasks;
using System.Windows;
using TrafficDesktopApp.Services;
using TrafficDesktopApp.Models;
using System.Configuration;
using System.Threading;
using TrafficDesktopApp.Helpers;

namespace TrafficDesktopApp
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AttachGlobalExceptionHandlers();

            // Forzar GMap a no usar caché local (SQLite) si hay problemas cargando la DLL
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
        }

        private void AttachGlobalExceptionHandlers()
        {
            // UI thread exceptions (WPF Dispatcher)
            this.DispatcherUnhandledException += (s, e) =>
            {
                string logPath = CrashLogger.LogException(e.Exception, "DispatcherUnhandledException");
                MessageBox.Show(
                    "Ha ocurrido un error inesperado.\n\n" +
                    e.Exception.Message +
                    (string.IsNullOrWhiteSpace(logPath) ? "" : "\n\nLog: " + logPath),
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                e.Handled = true; // avoid silent app exit
            };

            // Non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                CrashLogger.LogException(ex ?? new Exception("UnhandledException (non-Exception object)"), "AppDomain.UnhandledException");
                // Can't reliably show UI here; we just log.
            };

            // Unobserved task exceptions
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                CrashLogger.LogException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await EnsureAdminUserExistsAsync();
        }

        /// <summary>
        /// Comprueba si el usuario administrador configurado existe en la base de datos (vía API).
        /// Si no existe, lo crea automáticamente usando las credenciales de App.config.
        /// </summary>
        private async Task EnsureAdminUserExistsAsync()
        {
            try
            {
                string adminEmail = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];
                string adminPass = System.Configuration.ConfigurationManager.AppSettings["AdminPassword"];
                string adminName = System.Configuration.ConfigurationManager.AppSettings["AdminName"];

                if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPass))
                    return;

                // Obtenemos todos los usuarios para ver si el admin ya existe
                var users = await UsersService.GetAllUsersAsync();
                
                bool exists = false;
                if (users != null)
                {
                    foreach (var user in users)
                    {
                        if (user.Email != null && user.Email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase))
                        {
                            exists = true;
                            break;
                        }
                    }
                }

                if (!exists)
                {
                    var newAdmin = new UserCreateRequest
                    {
                        Email = adminEmail,
                        Password = adminPass,
                        NombreCompleto = adminName ?? "Admin",
                        Rol = "ADMIN", // Asumimos que el rol de admin es "ADMIN"
                        BypassEmail = true // No enviar correo de bienvenida para el admin inicial
                    };

                    await UsersService.CreateUserAsync(newAdmin);
                    System.Diagnostics.Debug.WriteLine($"Usuario administrador creado: {adminEmail}");
                }
            }
            catch (Exception ex)
            {
                // En una app real, registraríamos esto en un log.
                // Aquí solo imprimimos para depuración.
                System.Diagnostics.Debug.WriteLine($"Error al asegurar el usuario admin: {ex.Message}");
            }
        }
    }
}
