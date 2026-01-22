using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TrafficDesktopApp.Controls.General
{
    /// <summary>
    /// Lógica de interacción para DashboardHeader.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public event Action DashboardClicked;
        public event Action IncidentsClicked;

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

        public Header()
        {
            InitializeComponent();
        }

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
            else
            {
                UsersLink.Foreground = Brushes.Black;
                UsersUnderline.Background = (Brush)new BrushConverter().ConvertFrom("#F5B400");
            }
        }

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

        private void OpenWindow(Window newWindow)
        {
            Window current = Window.GetWindow(this);

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

                newWindow.Show();
                current.Close();
            }
        }

    }
}
