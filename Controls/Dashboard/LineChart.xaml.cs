using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using System.Windows.Media;

namespace TrafficDesktopApp.Controls.Dashboard
{
    public partial class LineChart : UserControl
    {
        public SeriesCollection Series { get; set; }
        public string[] Labels { get; set; }

        public LineChart()
        {
            InitializeComponent();

            Labels = new[] { "23 h", "4", "9", "14", "3", "4", "5", "30" };

            Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int> { 0, 1, 2, 4, 6, 5, 7, 10 },
                    Stroke = Brushes.Black,
                    StrokeThickness = 3,
                    Fill = Brushes.Transparent,
                    PointGeometry = null, // sin puntos
                    LineSmoothness = 0    // línea recta, no curva
                }
            };

            DataContext = this;
        }
    }
}
