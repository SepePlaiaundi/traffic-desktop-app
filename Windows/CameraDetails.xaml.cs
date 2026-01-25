using System.Windows;
using TrafficDesktopApp.Models; 

namespace TrafficDesktopApp.Windows
{
    public partial class CameraDetails : Window
    {
        public CameraDetails(Camera camera)
        {
            InitializeComponent();
            this.DataContext = camera;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}