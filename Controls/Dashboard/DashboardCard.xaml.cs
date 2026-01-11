using System.Windows.Controls;

namespace TrafficDesktopApp.Controls.Dashboard
{
    /// <summary>
    /// Lógica de interacción para DashboardCard.xaml
    /// </summary>
    public partial class DashboardCard : UserControl
    {
        public DashboardCard()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Title { get; set; }
        public string Value { get; set; }
        public string Subtitle { get; set; }
    }
}
