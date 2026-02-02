using System;
using System.Windows;
using System.Windows.Controls;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;

namespace TrafficDesktopApp.Controls.Report
{
    /// <summary>
    /// Botón personalizado que orquestra la captura de datos de la UI y la generación de informes PDF profesionales.
    /// </summary>
    public partial class ReportButton : UserControl
    {
        public ReportButton()
        {
            InitializeComponent();
        }

        // -----------------------------
        // Summary values
        // -----------------------------

        public static readonly DependencyProperty CameraCountProperty =
            DependencyProperty.Register(
                nameof(CameraCount),
                typeof(int),
                typeof(ReportButton),
                new PropertyMetadata(0));

        public int CameraCount
        {
            get { return (int)GetValue(CameraCountProperty); }
            set { SetValue(CameraCountProperty, value); }
        }

        public static readonly DependencyProperty IncidentsTodayProperty =
            DependencyProperty.Register(
                nameof(IncidentsToday),
                typeof(int),
                typeof(ReportButton),
                new PropertyMetadata(0));

        public int IncidentsToday
        {
            get { return (int)GetValue(IncidentsTodayProperty); }
            set { SetValue(IncidentsTodayProperty, value); }
        }

        // -----------------------------
        // Chart sources (LiveCharts)
        // -----------------------------

        public static readonly DependencyProperty DailyChartSourceProperty =
            DependencyProperty.Register(
                nameof(DailyChartSource),
                typeof(FrameworkElement),
                typeof(ReportButton),
                new PropertyMetadata(null));

        public FrameworkElement DailyChartSource
        {
            get { return (FrameworkElement)GetValue(DailyChartSourceProperty); }
            set { SetValue(DailyChartSourceProperty, value); }
        }

        public static readonly DependencyProperty MonthlyChartSourceProperty =
            DependencyProperty.Register(
                nameof(MonthlyChartSource),
                typeof(FrameworkElement),
                typeof(ReportButton),
                new PropertyMetadata(null));

        public FrameworkElement MonthlyChartSource
        {
            get { return (FrameworkElement)GetValue(MonthlyChartSourceProperty); }
            set { SetValue(MonthlyChartSourceProperty, value); }
        }

        public static readonly DependencyProperty RecentIncidentsProperty =
            DependencyProperty.Register(
                nameof(RecentIncidents),
                typeof(System.Collections.Generic.List<Incidence>),
                typeof(ReportButton),
                new PropertyMetadata(null));

        public System.Collections.Generic.List<Incidence> RecentIncidents
        {
            get { return (System.Collections.Generic.List<Incidence>)GetValue(RecentIncidentsProperty); }
            set { SetValue(RecentIncidentsProperty, value); }
        }

        // -----------------------------
        // Action
        // -----------------------------

        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = $"Reporte_Trafico_{DateTime.Now:yyyyMMdd}"
            };

            if (dialog.ShowDialog() != true)
                return;

            var dailyChartImage = UiElementCapturer.Capture(DailyChartSource);
            var monthlyChartImage = UiElementCapturer.Capture(MonthlyChartSource);

            var reportData = new ReportData
            {
                GeneratedAt = DateTime.Now,
                ActiveCameras = CameraCount,
                IncidentsToday = IncidentsToday,
                DailyChartImage = dailyChartImage,
                MonthlyChartImage = monthlyChartImage,
                RecentIncidents = RecentIncidents
            };

            PdfReportService.Generate(reportData, dialog.FileName);

            // Abrir el archivo automáticamente
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dialog.FileName) { UseShellExecute = true });
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Reporte generado con éxito en: {dialog.FileName}\n\n(No se pudo abrir automáticamente: {ex.Message})", "Reporte Generado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
