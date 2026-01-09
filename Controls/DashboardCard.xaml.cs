using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrafficDesktopApp.Controls
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
