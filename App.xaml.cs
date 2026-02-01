using System;
using System.Windows;

namespace TrafficDesktopApp
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Forzar GMap a no usar caché local (SQLite) si hay problemas cargando la DLL
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Comprobamos si hay un token guardado (o lógica de "Recordarme")
            // De momento, asumimos que siempre empieza deslogueado si se cierra la app,
            // o podríamos mirar si AuthService tiene algo (que al inicio estará vacío).
            
            // Si quisieras persistencia, aquí leerías de Properties.Settings.Default o un archivo local
            bool isLoggedIn = !string.IsNullOrEmpty(TrafficDesktopApp.Services.AuthService.CurrentToken);

            if (isLoggedIn)
            {
                // Si ya estamos logueados, vamos al Dashboard
                var dashboard = new TrafficDesktopApp.Windows.Dashboard();
                dashboard.Show();
            }
            else
            {
                // Si no, al Login
                var login = new TrafficDesktopApp.Windows.Login();
                login.Show();
            }
        }
    }
}
