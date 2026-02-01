using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Windows;

namespace TrafficDesktopApp.Controls.Cameras
{
    /// <summary>
    /// Vista de rejilla (Grid) que muestra las cámaras de tráfico en tarjetas visuales.
    /// </summary>
    public partial class CamerasGridView : UserControl
    {
        public ObservableCollection<Camera> VisibleCameras { get; set; } = new ObservableCollection<Camera>();

        public CamerasGridView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetCameras(List<Camera> cameras)
        {
            VisibleCameras.Clear();
            if (cameras != null)
            {
                foreach (var cam in cameras) VisibleCameras.Add(cam);
            }
        }

        private void CameraCard_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border card && card.DataContext is Camera camera)
            {
                var detailsWindow = new CameraDetails(camera);
                detailsWindow.Owner = Window.GetWindow(this); 
                detailsWindow.ShowDialog(); 
            }
        }
    }
}