using System;
using System.Windows;
using System.Windows.Input;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    public partial class CreateIncidenceWindow : Window
    {
        public CreateIncidenceWindow()
        {
            InitializeComponent();
            StartDatePicker.SelectedDate = DateTime.Now;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validación básica
                if (string.IsNullOrWhiteSpace(CauseBox.Text) || string.IsNullOrWhiteSpace(RoadBox.Text))
                {
                    ShowError("Por favor, rellene los campos obligatorios (Causa, Carretera).");
                    return;
                }

                double lat = 0, lng = 0;
                double.TryParse(LatBox.Text, out lat);
                double.TryParse(LngBox.Text, out lng);

                int resourceId = 1;
                int.TryParse(ResourceIdBox.Text, out resourceId);

                var incidence = new Incidence
                {
                    Cause = CauseBox.Text,
                    Road = RoadBox.Text,
                    City = CityBox.Text,
                    Province = ProvinceBox.Text,
                    Direction = DirectionBox.Text,
                    Latitude = lat,
                    Longitude = lng,
                    Level = LevelBox.Text,
                    Type = TypeBox.Text,
                    Description = DescriptionBox.Text,
                    StartDate = StartDatePicker.SelectedDate ?? DateTime.Now,
                    EndDate = EndDatePicker.SelectedDate,
                    Recurso = new Recurso { Id = resourceId },
                    IsNew = true
                };

                bool success = await IncidenceService.CreateIncidenceAsync(incidence);

                if (success)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    ShowError("Error al crear la incidencia. Verifique la conexión con el servidor.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error: " + ex.Message);
            }
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorBorder.Visibility = Visibility.Visible;
        }
    }
}
