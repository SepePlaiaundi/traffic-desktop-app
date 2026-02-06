using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrafficDesktopApp.Controls.Cameras;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Lógica de interacción para Cameras.xaml
    /// </summary>
    /// <summary>
    /// Ventana que gestiona la visualización de cámaras tanto en formato rejilla (Grid) como en mapa.
    /// </summary>
    public partial class Cameras : Window
    {
        private CamerasMapView _camerasMapView;
        private List<Camera> _allCameras;

        public Cameras()
        {
            InitializeComponent();

            Header.SetActive("Cameras");

            LoadCamerasData();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCamerasData();
        }

        private void SwitchView_Click(object sender, RoutedEventArgs e)
        {
            if (BtnGrid.IsChecked == true)
            {
                CamerasGrid.Visibility = Visibility.Visible;
                CamerasMapHost.Visibility = Visibility.Collapsed;
            }
            else
            {
                CamerasGrid.Visibility = Visibility.Collapsed;
                EnsureMapCreated();
                CamerasMapHost.Visibility = Visibility.Visible;
                if (_allCameras != null)
                    _camerasMapView?.SetCameras(_allCameras);
            }
        }

        private async void LoadCamerasData()
        {
            try
            {
                var cameras = await CamerasService.GetCamerasAsync();
                if (cameras != null)
                {
                    _allCameras = cameras;
                    // Update the Grid
                    CamerasGrid.SetCameras(cameras);
                    // Update the Map
                    _camerasMapView?.SetCameras(cameras);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void EnsureMapCreated()
        {
            if (_camerasMapView != null)
                return;

            // Lazy-load the map so the window can open even if map deps fail in published builds.
            _camerasMapView = new CamerasMapView();
            CamerasMapHost.Content = _camerasMapView;
        }
    }
}

   