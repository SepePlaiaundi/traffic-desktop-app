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
    }
}
