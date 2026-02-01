using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TrafficDesktopApp.Controls.Dashboard;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    /// <summary>
    /// Ventana principal del Dashboard que muestra estadísticas, cámaras e incidencias recientes.
    /// Implementa INotifyPropertyChanged para permitir el enlace de datos (Binding).
    /// </summary>
    public partial class Dashboard : Window, System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        private int _activeCameras;
        public int ActiveCameras
        {
            get => _activeCameras;
            set { _activeCameras = value; PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ActiveCameras))); }
        }

        private int _incidentsToday;
        public int IncidentsToday
        {
            get => _incidentsToday;
            set { _incidentsToday = value; PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(IncidentsToday))); }
        }

        public Dashboard()
        {
            InitializeComponent();
            DataContext = this; // Important for Binding
            Header.SetActive("Dashboard");
            LoadDashboardData();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboardData();
        }

        /// <summary>
        /// Carga de manera asíncrona todos los datos necesarios para el Dashboard desde los servicios.
        /// </summary>
        private async void LoadDashboardData()
        {
            try
            {
                // Cargamos todos los datos en paralelo para mayor velocidad
                var camerasTask = CamerasService.GetCamerasAsync();
                var incidencesTask = IncidenceService.GetIncidencesAsync();
                var usersTask = UsersService.GetAllUsersAsync();

                await Task.WhenAll(camerasTask, incidencesTask, usersTask);

                List<Camera> cameras = await camerasTask;
                List<Incidence> incidences = await incidencesTask;
                List<User> users = await usersTask;

                UpdateCounters(cameras, incidences, users);
                UpdateCamerasList(cameras);
                UpdateIncidencesList(incidences);
                UpdateMonthlyChart(incidences);
                UpdateDailyChart(incidences);
            }
            catch
            {
                CardTotalCamaras.Value = "Err";
            }
        }

        /// <summary>
        /// Actualiza los contadores de la interfaz (Cards) y calcula variaciones respecto a días anteriores.
        /// </summary>
        private void UpdateCounters(
            IReadOnlyCollection<Camera> cameras,
            IReadOnlyCollection<Incidence> incidences,
            IReadOnlyCollection<User> users)
        {
            // 1. Incidencias Hoy and variation
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            int countToday = incidences.Count(i => i.StartDate.Date == today);
            int countYesterday = incidences.Count(i => i.StartDate.Date == yesterday);

            IncidentsToday = countToday; // Update property for Report Binding
            CardTotalIncidencias.Value = countToday.ToString();
            
            if (countYesterday == 0)
            {
                CardTotalIncidencias.Subtitle = "+ Inf% respecto ayer";
            }
            else
            {
                double variation = ((double)(countToday - countYesterday) / countYesterday) * 100;
                string sign = variation >= 0 ? "+" : "";
                CardTotalIncidencias.Subtitle = $"{sign}{variation:0}% respecto ayer";
            }

            // 2. Cameras
            ActiveCameras = cameras.Count; // Update property for Report Binding
            CardTotalCamaras.Value = cameras.Count.ToString();
            CardTotalCamaras.Subtitle = string.Empty;

            // 3. Users logic remains total count
            CardTotalUsuarios.Value = users.Count.ToString();
        }


        private void UpdateCamerasList(List<Camera> cameras)
        {
            CamerasList.UpdateList(cameras.Take(5).ToList());
        }


        private void UpdateIncidencesList(List<Incidence> incidences)
        {
            IncidencesList.Incidents = incidences
                .OrderByDescending(i => i.StartDate)
                .Take(5)
                .ToList();
        }


        private void UpdateMonthlyChart(List<Incidence> incidences)
        {
            MonthlyChart.SetIncidences(incidences);
        }

        private void UpdateDailyChart(List<Incidence> incidences)
        {
            var now = DateTime.Now;

            var hours = Enumerable.Range(0, 24)
                .Select(h => now.AddHours(-23 + h))
                .ToList();

            var grouped = incidences
                .Where(i => i.StartDate >= now.AddHours(-24))
                .GroupBy(i => i.StartDate.Hour)
                .ToDictionary(g => g.Key, g => g.Count());

            var points = hours
                .Select(h =>
                (
                    Label: $"{h.Hour:00}h",
                    Value: grouped.TryGetValue(h.Hour, out var c) ? c : 0
                ))
                .ToList();

            DailyChart.SetHourlyData(points);
        }


    }

}
