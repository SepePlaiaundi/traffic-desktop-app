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
        private void ShowError(string message) { GlobalToast.Show(message); }
        private void ToggleLoading(bool isLoading) 
        { 
            GlobalLoading.IsLoading = isLoading; 
            this.Cursor = isLoading ? System.Windows.Input.Cursors.Wait : System.Windows.Input.Cursors.Arrow; 
        } 

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
                CamerasMap.Visibility = Visibility.Collapsed;
            }
            else
            {
                CamerasGrid.Visibility = Visibility.Collapsed;
                CamerasMap.Visibility = Visibility.Visible;
            }
        }

        private async void LoadCamerasData()
        {
            ToggleLoading(true);
            try
            {
                var cameras = await CamerasService.GetCamerasAsync();
                if (cameras != null)
                {
                    // Update the Grid
                    CamerasGrid.SetCameras(cameras);
                    // Update the Map
                    CamerasMap.SetCameras(cameras);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error: " + ex.Message);
            }
            finally
            {
                ToggleLoading(false);
            }
        }
    }
}

   