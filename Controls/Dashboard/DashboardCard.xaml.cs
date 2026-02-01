using System.Windows;
using System.Windows.Controls;

namespace TrafficDesktopApp.Controls.Dashboard
{
    /// <summary>
    /// Lógica de interacción para DashboardCard.xaml
    /// </summary>
    /// <summary>
    /// Tarjeta visual del Dashboard que muestra un título, valor numérico y un subtítulo opcional.
    /// </summary>
    public partial class DashboardCard : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(string), typeof(DashboardCard), new PropertyMetadata("—"));

        public DashboardCard()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Title { get; set; }
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public string Subtitle { get; set; }
    }
}
