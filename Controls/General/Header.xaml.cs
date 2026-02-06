using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TrafficDesktopApp.Services;
using System.Windows.Media.Imaging;
using TrafficDesktopApp.Helpers;

namespace TrafficDesktopApp.Controls.General
{
    /// <summary>
    /// Barra de navegación superior (Header) que gestiona el menú principal y la sesión del usuario.
    /// </summary>
    public partial class Header : UserControl
    {
        // Propiedad para cambiar el título desde XAML
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(Header), new PropertyMetadata("Panel de control"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static readonly Brush ActiveColor = (Brush)new BrushConverter().ConvertFrom("#F5B400");

        public Header()
        {
            InitializeComponent();
            LoadUserData();
        }

        private async void LoadUserData()
        {
            try
            {
                var user = await UsersService.GetMyProfileAsync();
                if (user != null)
                {
                    // Actualizar iniciales
                    if (!string.IsNullOrEmpty(user.NombreCompleto))
                    {
                        UserInitials.Text = user.NombreCompleto.Substring(0, 1).ToUpper();
                    }

                    // Actualizar avatar si existe
                    if (!string.IsNullOrEmpty(user.Avatar))
                    {
                        try
                        {
                            UserAvatarBrush.ImageSource = new BitmapImage(new Uri(user.Avatar, UriKind.Absolute));
                            UserAvatarEllipse.Visibility = Visibility.Visible;
                            UserInitials.Visibility = Visibility.Collapsed;
                        }
                        catch { /* Ignorar errores de carga de imagen */ }
                    }
                }
            }
            catch { /* Silencioso en el header */ }
        }
        /// <summary>
        /// Resalta la página activa en el menú de navegación.
        /// </summary>
        public void SetActive(string page)
        {
            // Resetear todos a gris/transparente
            DashboardLink.Foreground = IncidencesLink.Foreground = CamerasLink.Foreground = UsersLink.Foreground = Brushes.Gray;
            DashboardUnderline.Background = IncidencesUnderline.Background = CamerasUnderline.Background = UsersUnderline.Background = Brushes.Transparent;

            // Activar el seleccionado
            switch (page)
            {
                case "Dashboard":
                    DashboardLink.Foreground = Brushes.Black;
                    DashboardUnderline.Background = ActiveColor;
                    break;
                case "Incidences":
                    IncidencesLink.Foreground = Brushes.Black;
                    IncidencesUnderline.Background = ActiveColor;
                    break;
                case "Cameras":
                    CamerasLink.Foreground = Brushes.Black;
                    CamerasUnderline.Background = ActiveColor;
                    break;
                case "Users":
                    UsersLink.Foreground = Brushes.Black;
                    UsersUnderline.Background = ActiveColor;
                    break;
            }
        }

        // Navegación entre ventanas
        // Usamos el nombre completo (TrafficDesktopApp.Windows) para evitar conflictos con los nombres de carpetas de controles
        private void Dashboard_Click(object sender, RoutedEventArgs e) => OpenWindowSafe(() => new TrafficDesktopApp.Windows.Dashboard(), "Navigate:Dashboard");
        private void Incidences_Click(object sender, RoutedEventArgs e) => OpenWindowSafe(() => new TrafficDesktopApp.Windows.Incidences(), "Navigate:Incidences");
        private void Cameras_Click(object sender, RoutedEventArgs e) => OpenWindowSafe(() => new TrafficDesktopApp.Windows.Cameras(), "Navigate:Cameras");
        private void Users_Click(object sender, RoutedEventArgs e) => OpenWindowSafe(() => new TrafficDesktopApp.Windows.Users(), "Navigate:Users");

        private void Avatar_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.ContextMenu != null)
            {
                element.ContextMenu.PlacementTarget = element;
                element.ContextMenu.IsOpen = true;
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new TrafficDesktopApp.Windows.UserProfileWindow();
            profileWindow.Owner = Window.GetWindow(this);
            profileWindow.ShowDialog();
            
            // Recargar datos por si han cambiado en la ventana de perfil
            LoadUserData();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            new TrafficDesktopApp.Windows.Login().Show();
            Window.GetWindow(this)?.Close();
        }

        /// <summary>
        /// Abre una nueva ventana manteniendo el tamaño y posición de la actual.
        /// </summary>
        private void OpenWindowSafe(Func<Window> newWindowFactory, string context)
        {
            Window current = Window.GetWindow(this);

            try
            {
                Window newWindow = newWindowFactory?.Invoke();
                if (newWindow == null)
                    throw new InvalidOperationException("No se pudo crear la ventana de destino (null).");

                if (current != null)
                {
                    newWindow.WindowState = current.WindowState;
                    if (current.WindowState == WindowState.Normal)
                    {
                        newWindow.Width = current.Width;
                        newWindow.Height = current.Height;
                        newWindow.Left = current.Left;
                        newWindow.Top = current.Top;
                    }
                }

                // Evitar que el cierre de la ventana inicial apague la app en algunos escenarios de publicación.
                Application.Current.MainWindow = newWindow;
                newWindow.Show();

                current?.Close();
            }
            catch (Exception ex)
            {
                string logPath = CrashLogger.LogException(ex, context);
                MessageBox.Show(
                    "No se pudo abrir la pantalla solicitada.\n\n" +
                    ex.Message +
                    (string.IsNullOrWhiteSpace(logPath) ? "" : "\n\nLog: " + logPath),
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}