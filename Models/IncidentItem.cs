namespace TrafficDesktopApp.Models
{
    public class IncidentItem
    {
        public string Type { get; }
        public string Road { get; }
        public string Province { get; }

        public IncidentItem(string type, string road, string province)
        {
            Type = type;
            Road = road;
            Province = province;
        }
    }
}
