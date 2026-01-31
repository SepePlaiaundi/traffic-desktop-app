using System;

namespace TrafficDesktopApp.Models
{
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
