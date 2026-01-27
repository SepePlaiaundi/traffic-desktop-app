using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Services
{
    public static class PdfReportService
    {
        public static void Generate(ReportData data, string filePath)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text("Traffic Monitoring Report")
                        .FontSize(20)
                        .SemiBold()
                        .AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Spacing(20);

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Cámaras activas: {data.ActiveCameras}");
                            row.RelativeItem().Text($"Incidencias hoy: {data.IncidentsToday}");
                        });

                        if (data.DailyChartImage != null)
                        {
                            col.Item().Text("Incidencias últimas 24h").SemiBold();
                            col.Item().Image(data.DailyChartImage).FitWidth();
                        }

                        if (data.MonthlyChartImage != null)
                        {
                            col.Item().Text("Incidencias mensuales").SemiBold();
                            col.Item().Image(data.MonthlyChartImage).FitWidth();
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generado: {data.GeneratedAt:g}");
                });
            })
            .GeneratePdf(filePath);
        }
    }
}
