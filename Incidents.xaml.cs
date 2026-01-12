using System.Windows;
using TrafficDesktopApp.Controls.Dashboard;

namespace TrafficDesktopApp
{
    /// <summary>
    /// Interaction logic for Incidents.xaml
    /// </summary>
    public partial class Incidents : Window
    {
        public Incidents()
        {
            InitializeComponent();

            Header.SetActive("Incidents");

            Header.DashboardClicked += () =>
            {
                new Dashboard().Show();
                Close();
            };

            Sidebar.FilterChanged += IncidentsList.ApplyFilter;
        }

    }
}
