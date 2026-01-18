using System;
using System.Linq;
using System.Windows;
using TrafficDesktopApp.Controls.Dashboard;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        public Dashboard()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private async void LoadDashboardData()
        {
            try
            {
                var allCameras = await CamerasService.GetCamerasAsync();
                CardTotalCamaras.Value = allCameras.Count.ToString();
                CamerasList.UpdateList(allCameras.Take(5).ToList());
            }
            catch (Exception ex)
            {
                CardTotalCamaras.Value = "Err";
                // ...
            }
        }
    }
}
