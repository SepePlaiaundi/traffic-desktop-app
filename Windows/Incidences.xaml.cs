using System.Windows;
using TrafficDesktopApp.Controls.Dashboard;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Interaction logic for Incidents.xaml
    /// </summary>
    public partial class Incidences : Window
    {
        public Incidences()
        {
            InitializeComponent();

            Header.SetActive("Incidences");

            Sidebar.FilterChanged += IncidentsList.ApplyFilter;
        }

    }
}
