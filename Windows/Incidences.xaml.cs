using System;
using System.Collections.Generic;
using System.Windows;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Windows
{
    public partial class Incidences : Window
    {
        private List<Incidence> _allIncidences;

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
                IncidentsMap.SetIncidences(_allIncidences);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnFilterChanged(string type)
        {
            IncidentsList.ApplyFilter(type);

            List<Incidence> filtered =
                type == null
                    ? _allIncidences
                    : _allIncidences.FindAll(i => i.Type == type);

            IncidentsMap.SetIncidences(filtered);
        }

        private void SwitchView_Click(object sender, RoutedEventArgs e)
        {
            if (BtnList.IsChecked == true)
            {
                IncidentsList.Visibility = Visibility.Visible;
                IncidentsMap.Visibility = Visibility.Collapsed;
            }
            else
            {
                IncidentsList.Visibility = Visibility.Collapsed;
                IncidentsMap.Visibility = Visibility.Visible;
            }
        }
    }
}
