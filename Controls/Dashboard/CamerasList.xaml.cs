using System.Collections.ObjectModel;
using System.Windows.Controls;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

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

            Load();
        }

        private async void Load()
        {
            var cameras = await CamerasService.GetCamerasAsync();

            Cameras.Clear();
            foreach (Camera cam in cameras)
            {
                Cameras.Add(cam);
            }
        }
    }
}
