namespace TrafficDesktopApp.Models
{
    public class IncidenceItem
    {
        public string Type { get; }
        public string Road { get; }
        public string Province { get; }

        public IncidenceItem(string type, string road, string province)
        {
            Type = type;
            Road = road;
            Province = province;
        }
    }
}
