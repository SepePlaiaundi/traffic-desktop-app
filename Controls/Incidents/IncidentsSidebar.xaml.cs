using System;
using System.Windows;
using System.Windows.Controls;

namespace TrafficDesktopApp.Controls.Incidents
{
    /// <summary>
    /// Interaction logic for IncidentsSidebar.xaml
    /// </summary>
    public partial class IncidentsSidebar : UserControl
    {
        public event Action<string> FilterChanged;

        public IncidentsSidebar()
        {
            InitializeComponent();
        }

        private void All_Click(object sender, RoutedEventArgs e) => FilterChanged?.Invoke("All");
        private void Works_Click(object sender, RoutedEventArgs e) => FilterChanged?.Invoke("Works");
        private void Accidents_Click(object sender, RoutedEventArgs e) => FilterChanged?.Invoke("Accidents");
        private void Others_Click(object sender, RoutedEventArgs e) => FilterChanged?.Invoke("Others");
    }
}
