using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Incidences
{
    /// <summary>
    /// Interaction logic for IncidencesSidebar.xaml
    /// </summary>
    public partial class IncidencesSidebar : UserControl
    {
        public event Action<IncidenceType?> FilterChanged;

        public IncidencesSidebar()
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
            FilterChanged?.Invoke(IncidenceType.Obras);
        }

        private void Accidents_Click(object sender, MouseButtonEventArgs e)
        {
            SetActive(AccidentsDot, AccidentsText);
            FilterChanged?.Invoke(IncidenceType.Accidente);
        }

        private void Others_Click(object sender, MouseButtonEventArgs e)
        {
            SetActive(OthersDot, OthersText);
            FilterChanged?.Invoke(IncidenceType.Otro);
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
