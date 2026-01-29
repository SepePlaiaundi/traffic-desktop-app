using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TrafficDesktopApp.Controls.Dashboard;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            Header.SetActive("Dashboard");
            LoadDashboardData();
        }

        private async void LoadDashboardData()
        {
            try
            {
                List<Camera> cameras = await CamerasService.GetCamerasAsync();
                List<Incidence> incidences = await IncidenceService.GetIncidencesAsync();

                UpdateCounters(cameras, incidences);
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

        private void UpdateCounters(
            IReadOnlyCollection<Camera> cameras,
            IReadOnlyCollection<Incidence> incidences)
        {
            CardTotalIncidencias.Value = incidences.Count.ToString();
            CardTotalCamaras.Value = cameras.Count.ToString();
        }


        private void UpdateCamerasList(List<Camera> cameras)
        {
            CamerasList.UpdateList(cameras.Take(5).ToList());
        }


        private void UpdateIncidencesList(List<Incidence> incidences)
        {
            IncidencesList.Incidents = incidences
                .OrderByDescending(i => i.StartDate)
                .Take(10)
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
