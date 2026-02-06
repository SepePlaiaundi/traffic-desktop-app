using System;
using System.Collections.Generic;
using System.Windows;
using TrafficDesktopApp.Controls.Incidences;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Ventana que gestiona el listado completo de incidencias, permitiendo filtrado y cambio entre vista de lista y mapa.
    /// </summary>
    public partial class Incidences : Window
    {
        private List<Incidence> _allIncidences;
        private IncidencesMapView _incidencesMapView;
        private string _currentFilterType;

        public Incidences()
        {
            InitializeComponent();

            Header.SetActive("Incidences");

            Sidebar.FilterChanged += OnFilterChanged;

            LoadIncidences();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadIncidences();
        }

        private async void LoadIncidences()
        {
            try
            {
                _allIncidences = await IncidenceService.GetIncidencesAsync();

                IncidentsList.SetIncidences(_allIncidences);
                _incidencesMapView?.SetIncidences(_allIncidences);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnFilterChanged(string type)
        {
            _currentFilterType = type;
            IncidentsList.ApplyFilter(type);

            List<Incidence> filtered =
                type == null
                    ? _allIncidences
                    : _allIncidences.FindAll(i => i.StartDate.HasValue && i.Type == type);

            _incidencesMapView?.SetIncidences(filtered);
        }

        private void SwitchView_Click(object sender, RoutedEventArgs e)
        {
            if (BtnList.IsChecked == true)
            {
                IncidentsList.Visibility = Visibility.Visible;
                IncidentsMapHost.Visibility = Visibility.Collapsed;
            }
            else
            {
                IncidentsList.Visibility = Visibility.Collapsed;
                EnsureMapCreated();
                IncidentsMapHost.Visibility = Visibility.Visible;
                _incidencesMapView?.SetIncidences(GetFilteredIncidences());
            }
        }

        private void EnsureMapCreated()
        {
            if (_incidencesMapView != null)
                return;

            // Lazy-load the map so the window can open even if map deps fail in published builds.
            _incidencesMapView = new IncidencesMapView();
            IncidentsMapHost.Content = _incidencesMapView;
        }

        private List<Incidence> GetFilteredIncidences()
        {
            if (_allIncidences == null)
                return new List<Incidence>();

            return _currentFilterType == null
                ? _allIncidences
                : _allIncidences.FindAll(i => i.StartDate.HasValue && i.Type == _currentFilterType);
        }

        private void CreateIncidence_Click(object sender, RoutedEventArgs e)
        {
            var modal = new CreateIncidenceWindow
            {
                Owner = this
            };

            if (modal.ShowDialog() == true)
            {
                LoadIncidences();
            }
        }
    }
}
