using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;
using TrafficDesktopApp.Windows;

namespace TrafficDesktopApp.Controls.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardCamerasList.xaml
    /// </summary>
    public partial class CamerasList : UserControl
    {
        public ObservableCollection<Camera> Cameras { get; set; } =
            new ObservableCollection<Camera>();

        public CamerasList()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void UpdateList(List<Camera> cameras)
        {
            Cameras.Clear();
            foreach (var cam in cameras) Cameras.Add(cam);
        }

        private void BtnViewMore_Click(object sender, RoutedEventArgs e)
        {
            Windows.Cameras camerasWindow = new Windows.Cameras();

            camerasWindow.Show();

            Window.GetWindow(this)?.Close();
        }
    }
}
