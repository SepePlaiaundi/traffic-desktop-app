namespace TrafficDesktopApp.Models
{
    public enum IncidentType
    {
        Obras,
        Accidente,
        Otro
    }

    public class Incident
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Road { get; set; }
        public string Province { get; set; }
        public IncidentType Type { get; set; }
        public string Date { get; set; }
    }
}

