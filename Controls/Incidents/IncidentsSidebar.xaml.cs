using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Incidents
{
    /// <summary>
    /// Interaction logic for IncidentsSidebar.xaml
    /// </summary>
    public partial class IncidentsSidebar : UserControl
    {
        public event Action<IncidentType?> FilterChanged;

        public IncidentsSidebar()
        {
            InitializeComponent();
        }

        private void All_Click(object sender, MouseButtonEventArgs e)
        {
            SetActive(AllDot, AllText);
            FilterChanged?.Invoke(null);
        }

        private void Works_Click(object sender, MouseButtonEventArgs e)
        {
            SetActive(WorksDot, WorksText);
            FilterChanged?.Invoke(IncidentType.Obras);
        }

        private void Accidents_Click(object sender, MouseButtonEventArgs e)
        {
            SetActive(AccidentsDot, AccidentsText);
            FilterChanged?.Invoke(IncidentType.Accidente);
        }

        private void Others_Click(object sender, MouseButtonEventArgs e)
        {
            SetActive(OthersDot, OthersText);
            FilterChanged?.Invoke(IncidentType.Otro);
        }


        private void SetActive(Ellipse activeDot, TextBlock activeText)
        {
            AllDot.Fill = WorksDot.Fill = AccidentsDot.Fill = OthersDot.Fill = Brushes.LightGray;
            AllText.FontWeight = WorksText.FontWeight = AccidentsText.FontWeight = OthersText.FontWeight = FontWeights.Normal;

            activeDot.Fill = Brushes.Gold;
            activeText.FontWeight = FontWeights.SemiBold;
        }

    }
}
