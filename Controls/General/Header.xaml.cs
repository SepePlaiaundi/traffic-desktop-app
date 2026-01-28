using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TrafficDesktopApp.Services; // Ensure you have this for AuthService
using TrafficDesktopApp.Windows;  // Ensure this for MainWindow (Login)

namespace TrafficDesktopApp.Controls.General
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        // --- EXISTING EVENTS ---
        public event Action DashboardClicked;
        public event Action IncidentsClicked;

        // --- DEPENDENCY PROPERTIES ---
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(Header),
                new PropertyMetadata("Panel de control"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        // --- CONSTRUCTOR ---
        public Header()
        {
            InitializeComponent();
        }


        // --- EXISTING: NAVIGATION HIGHLIGHTING ---
        public void SetActive(string page)
        {
            DashboardLink.Foreground = Brushes.Gray;
            IncidencesLink.Foreground = Brushes.Gray;
            CamerasLink.Foreground = Brushes.Gray;
            UsersLink.Foreground = Brushes.Gray;

            DashboardUnderline.Background = Brushes.Transparent;
            IncidencesUnderline.Background = Brushes.Transparent;
            CamerasUnderline.Background = Brushes.Transparent;
            UsersUnderline.Background = Brushes.Transparent;

            if (page == "Dashboard")
            {
                DashboardLink.Foreground = Brushes.Black;
                DashboardUnderline.Background = (Brush)new BrushConverter().ConvertFrom("#F5B400");
            }
            else if (page == "Incidences")
            {
                IncidencesLink.Foreground = Brushes.Black;
                IncidencesUnderline.Background = (Brush)new BrushConverter().ConvertFrom("#F5B400");
            }
            else if (page == "Cameras")
            {
                CamerasLink.Foreground = Brushes.Black;
                CamerasUnderline.Background = (Brush)new BrushConverter().ConvertFrom("#F5B400");
            }
            else if (page == "Users")
            {
                UsersLink.Foreground = Brushes.Black;
                UsersUnderline.Background = (Brush)new BrushConverter().ConvertFrom("#F5B400");
            }
        }

        // --- EXISTING: NAVIGATION CLICKS ---
        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new TrafficDesktopApp.Windows.Dashboard());
        }

        private void Incidences_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new TrafficDesktopApp.Windows.Incidences());
        }

        private void Cameras_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new TrafficDesktopApp.Windows.Cameras());
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new TrafficDesktopApp.Windows.Users());
        }

        // --- NEW: AVATAR & LOGOUT LOGIC ---

        private void Avatar_Click(object sender, MouseButtonEventArgs e)
        {
            // Find the ContextMenu defined in XAML resources or inline
            // Note: Ensure your XAML Border has the ContextMenu defined inside it
            // and the Border sender is cast correctly.

            if (sender is FrameworkElement element && element.ContextMenu != null)
            {
                element.ContextMenu.PlacementTarget = element;
                element.ContextMenu.IsOpen = true;
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Configuración de perfil (Próximamente)", "Mi Perfil", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // 1. Clear Session
            AuthService.Logout();

            // 2. Open Login Window (MainWindow)
            Login loginWindow = new Login();
            loginWindow.Show();

            // 3. Close the current window (Dashboard/Cameras/etc.)
            Window currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }

        // --- EXISTING: WINDOW SWAPPING ---
        private void OpenWindow(Window newWindow)
        {
            Window current = Window.GetWindow(this);

            if (current != null)
            {
                // Preserve state (Maximized/Normal) and position
                newWindow.WindowState = current.WindowState;

                if (current.WindowState == WindowState.Normal)
                {
                    newWindow.Width = current.Width;
                    newWindow.Height = current.Height;
                    newWindow.Left = current.Left;
                    newWindow.Top = current.Top;
                }

                newWindow.Show();
                current.Close();
            }
        }
    }
}