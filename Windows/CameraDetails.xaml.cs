using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    public partial class CameraDetails : Window
    {
        // New event to notify parent that data changed
        public event EventHandler CameraUpdated;

        public CameraDetails(Camera camera)
        {
            InitializeComponent();
            this.DataContext = camera;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void EstadoSwitch_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            var camera = this.DataContext as Camera;

            if (toggleButton == null || camera == null) return;

            bool isChecked = toggleButton.IsChecked == true;
            string nuevoEstado = isChecked ? "ACTIVA" : "INACTIVA";

            toggleButton.IsEnabled = false;

            bool exito = await CamerasService.SetCameraStateAsync(camera.Id, nuevoEstado);

            if (exito)
            {
                camera.Estado = nuevoEstado;
                
                // TRIGGER THE EVENT: Notify parent windows to refresh
                CameraUpdated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                toggleButton.IsChecked = !isChecked;
                MessageBox.Show("No se pudo actualizar el estado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            toggleButton.IsEnabled = true;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var camera = this.DataContext as Camera;
            if (camera == null) return;

            // 1. Confirmation Modal
            MessageBoxResult result = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar la cámara \"{camera.CameraName}\"?\nEsta acción no se puede deshacer.",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Disable buttons during async operation
                var btn = sender as Button;
                btn.IsEnabled = false;

                // 2. Call Service
                bool exito = await CamerasService.DeleteCameraAsync(camera.Id);

                if (exito)
                {
                    MessageBox.Show("Cámara eliminada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // 3. Notify parent to refresh the list and close
                    CameraUpdated?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la cámara. Verifique los permisos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    btn.IsEnabled = true;
                }
            }
        }
    }
}