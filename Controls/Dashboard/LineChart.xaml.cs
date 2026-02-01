using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TrafficDesktopApp.Controls.Dashboard
{
    /// <summary>
    /// Gráfico de líneas interactivo que muestra la evolución horaria de las incidencias.
    /// </summary>
    public partial class LineChart : UserControl
    {
        public LineChart()
        {
            InitializeComponent();
        }

        public void SetHourlyData(IEnumerable<(string Label, int Value)> points)
        {
            AxisX.Labels = points.Select(p => p.Label).ToArray();

            Chart.Series = new SeriesCollection
            {
                CreateLineSeries(points.Select(p => p.Value))
            };
        }

        private static LineSeries CreateLineSeries(IEnumerable<int> values)
        {
            return new LineSeries
            {
                Values = new ChartValues<int>(values),
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Transparent,
                PointGeometry = null,
                LineSmoothness = 0
            };
        }
    }
}
