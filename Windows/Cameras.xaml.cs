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
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Lógica de interacción para Cameras.xaml
    /// </summary>
    public partial class Cameras : Window
    {
        public Cameras()
        {
            InitializeComponent();

            Header.SetActive("Cameras");

            LoadCamerasData();
        }

        private async void LoadCamerasData()
        {
            try
            {
                var cameras = await CamerasService.GetCamerasAsync();
                if (cameras != null)
                {
                    CamerasGrid.SetCameras(cameras);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cámaras: " + ex.Message);
            }
        }

    }
}

   