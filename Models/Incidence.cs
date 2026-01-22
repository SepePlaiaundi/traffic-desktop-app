namespace TrafficDesktopApp.Models
{
    public enum IncidenceType
    {
        Obras,
        Accidente,
        Otro
    }

    public class Incidence
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Road { get; set; }
        public string Province { get; set; }
        public IncidenceType Type { get; set; }
        public string Date { get; set; }
    }
}

