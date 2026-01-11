using System.Collections.ObjectModel;
using System.Windows.Controls;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardCamerasList.xaml
    /// </summary>
    public partial class CamerasList : UserControl
    {
        public ObservableCollection<CameraItem> Cameras { get; }

        public CamerasList()
        {
            InitializeComponent();

            Cameras = new ObservableCollection<CameraItem>
            {
                new CameraItem
                {
                    Name = "C10254D",
                    LastUpdate = "última actualización: 10/05/2025 20:00 UTC+1",
                    Image = "/Assets/camera.png"
                },
                new CameraItem
                {
                    Name = "C10254D",
                    LastUpdate = "última actualización: 10/05/2025 20:00 UTC+1",
                    Image = "/Assets/camera.png"
                },
                new CameraItem
                {
                    Name = "C10254D",
                    LastUpdate = "última actualización: 10/05/2025 20:00 UTC+1",
                    Image = "/Assets/camera.png"
                }
            };

            DataContext = this;
        }
    }
}
