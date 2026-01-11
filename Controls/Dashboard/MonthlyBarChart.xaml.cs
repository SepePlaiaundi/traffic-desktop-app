using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using System.Windows.Media;

namespace TrafficDesktopApp.Controls.Dashboard
{
    public partial class MonthlyBarChart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public MonthlyBarChart()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>
                    {
                        9, 10, 9, 8, 12, 17, 12, 14, 11, 9, 8, 2
                    },
                    Fill = Brushes.Black,
                    MaxColumnWidth = 32
                }
            };

            Labels = new[]
            {
                "30-ene", "31", "01-feb", "02", "03",
                "04", "05", "06", "07", "08", "09", "10"
            };

            DataContext = this;
        }
    }
}