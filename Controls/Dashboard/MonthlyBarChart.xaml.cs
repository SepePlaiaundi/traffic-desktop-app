using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Dashboard
{
    /// <summary>
    /// Gráfico de barras que muestra el histórico mensual de incidencias (últimos 30 días).
    /// </summary>
    public partial class MonthlyBarChart : UserControl
    {
        public MonthlyBarChart()
        {
            InitializeComponent();
        }

        public void SetIncidences(IEnumerable<Incidence> incidences)
        {
            var today = DateTime.Today;
            var days = Enumerable.Range(0, 30)
                .Select(d => today.AddDays(-29 + d))
                .ToList();

            var countsByDay = incidences
                .GroupBy(i => i.StartDate.Value.Date)
                .ToDictionary(g => g.Key, g => g.Count());

            var values = days
                .Select(d => countsByDay.TryGetValue(d, out var c) ? c : 0)
                .ToList();

            AxisX.Labels = days
                .Select(d => d.ToString("dd-MMM"))
                .ToArray();

            Chart.Series = new SeriesCollection
            {
                CreateColumnSeries(values)
            };
        }

        private static ColumnSeries CreateColumnSeries(IEnumerable<int> values)
        {
            return new ColumnSeries
            {
                Values = new ChartValues<int>(values),
                Fill = Brushes.Black,
                MaxColumnWidth = 28
            };
        }
    }
}
