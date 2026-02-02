using System.Windows;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Lógica de interacción para IncidenceDetails.xaml
    /// </summary>
    public partial class IncidenceDetails : Window
    {
        public IncidenceDetails(Incidence incidence)
        {
            InitializeComponent();
            DataContext = incidence;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
