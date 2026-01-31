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
                    page.Margin(40);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Grey.Darken3));

                    page.Header().Element(header => ComposeHeader(header, data));
                    page.Content().Element(content => ComposeContent(content, data));
                    page.Footer().Element(footer => ComposeFooter(footer));
                });
            })
            .GeneratePdf(filePath);
        }

        static void ComposeHeader(IContainer container, ReportData data)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    // Intentar cargar logo desde varias ubicaciones posibles
                    string[] possiblePaths = {
                        System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Assets", "logo.png"),
                        System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "logo.png"),
                        "Assets/logo.png" // Ruta relativa al directorio de ejecución
                    };

                    string logoPath = null;
                    foreach (var path in possiblePaths)
                    {
                        if (System.IO.File.Exists(path))
                        {
                            logoPath = path;
                            break;
                        }
                    }

                    if (logoPath != null)
                    {
                         column.Item().Height(50).Image(logoPath, ImageScaling.FitHeight);
                    }
                    else
                    {
                         column.Item().Text("SEID").FontSize(28).SemiBold().FontColor("#F5B400");
                    }
                    
                    column.Item().Text($"Informe de Estado").FontSize(14).FontColor(Colors.Grey.Darken2);
                });

                row.ConstantItem(150).Column(column =>
                {
                    column.Item().AlignRight().Text(data.GeneratedAt.ToString("g"));
                    column.Item().AlignRight().Text("Generado Automáticamente").FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        }

        static void ComposeContent(IContainer container, ReportData data)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(30);

                // Summary Section
                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new MetricCard("Incidencias Hoy", data.IncidentsToday.ToString(), "#EF4444"));
                    row.ConstantItem(20);
                    row.RelativeItem().Component(new MetricCard("Cámaras Activas", data.ActiveCameras.ToString(), "#F5B400"));
                });

                // Charts Section
                column.Item().Column(c =>
                {
                    c.Spacing(15);
                    
                    if (data.DailyChartImage != null)
                    {
                        c.Item().Column(chartCol =>
                        {
                            chartCol.Item().Text("Evolución Diaria (últimas 24h)").SemiBold().FontSize(12);
                            chartCol.Item().PaddingTop(5).Image(data.DailyChartImage).FitWidth();
                        });
                    }
                    
                    if (data.MonthlyChartImage != null)
                    {
                        c.Item().Column(chartCol =>
                        {
                            chartCol.Item().Text("Histórico Mensual (últimos 30 días)").SemiBold().FontSize(12);
                            chartCol.Item().PaddingTop(5).Image(data.MonthlyChartImage).FitWidth();
                        });
                    }
                });


                // Incidents Table
                if (data.RecentIncidents != null && data.RecentIncidents.Count > 0)
                {
                    column.Item().Column(c =>
                    {
                        c.Spacing(10);
                        c.Item().Text("Últimas Incidencias").FontSize(16).SemiBold();
                        c.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(80); // Hora
                                columns.RelativeColumn();   // Tipo
                                columns.RelativeColumn();   // Causa/Obs
                                columns.ConstantColumn(100); // Provincia
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderCellStyle).Text("Hora");
                                header.Cell().Element(HeaderCellStyle).Text("Tipo");
                                header.Cell().Element(HeaderCellStyle).Text("Descripción");
                                header.Cell().Element(HeaderCellStyle).Text("Provincia");
                            });

                            foreach (var inc in data.RecentIncidents)
                            {
                                table.Cell().Element(CellStyle).Text(inc.StartDate.ToString("HH:mm"));
                                table.Cell().Element(CellStyle).Text(inc.Type ?? "N/A");
                                table.Cell().Element(CellStyle).Text(inc.Description ?? inc.Cause ?? "Sin detalles");
                                table.Cell().Element(CellStyle).Text(inc.Province ?? "-");
                            }
                        });
                    });
                }
            });
        }

        static IContainer HeaderCellStyle(IContainer container)
        {
            return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
        }

        static IContainer CellStyle(IContainer container)
        {
            return container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten3);
        }

        static void ComposeFooter(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                column.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeItem().Text("SEID - Sistema de Gestión de Tráfico").FontSize(9).FontColor(Colors.Grey.Medium);
                    row.RelativeItem().AlignRight().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });
        }

        private class MetricCard : IComponent
        {
            private string Title { get; }
            private string Value { get; }
            private string Accent { get; }

            public MetricCard(string title, string value, string accent)
            {
                Title = title;
                Value = value;
                Accent = accent;
            }

            public void Compose(IContainer container)
            {
                container
                    .Border(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Background(Colors.Grey.Lighten4)
                    .Padding(15)
                    .Column(column =>
                    {
                        column.Item().Text(Title).FontSize(12).FontColor(Colors.Grey.Darken2);
                        column.Item().Text(Value).FontSize(24).SemiBold().FontColor(Accent);
                    });
            }
        }
    }
}
