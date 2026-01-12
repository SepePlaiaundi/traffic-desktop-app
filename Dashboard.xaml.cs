using System.Windows;

namespace TrafficDesktopApp
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

            Header.IncidentsClicked += () =>
            {
                new Incidents().Show();
                Close();
            };
        }

    }
}
