using System;

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Estructura de datos utilizada para la generación de informes técnicos.
    /// </summary>
    public class ReportData
    {
        public DateTime GeneratedAt { get; set; }

        public int ActiveCameras { get; set; }
        public int IncidentsToday { get; set; }

        public byte[] DailyChartImage { get; set; }
        public byte[] MonthlyChartImage { get; set; }

        public System.Collections.Generic.List<Incidence> RecentIncidents { get; set; }
    }
}
