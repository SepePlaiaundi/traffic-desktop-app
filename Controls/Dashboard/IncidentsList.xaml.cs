using System.Collections.Generic;
using System.Windows.Controls;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Dashboard
{
    public partial class IncidentsList : UserControl
    {
        public List<IncidenceItem> Incidents { get; set; }

        public IncidentsList()
        {
            InitializeComponent();

            Incidents = new List<IncidenceItem>
            {
                new IncidenceItem("Accidente", "AP-8", "GI"),
                new IncidenceItem("Obras", "GI-20", "GI"),
                new IncidenceItem("Desvío temporal", "A-1", "GI"),
                new IncidenceItem("Accidente grave", "Calle Lezo, Km-20", "GI"),
                new IncidenceItem("Obras", "BI-13", "BI"),
                new IncidenceItem("Obras", "AP-1", "AR"),
                new IncidenceItem("Accidente", "A-1", "AR")
            };

            DataContext = this;
        }
    }
}