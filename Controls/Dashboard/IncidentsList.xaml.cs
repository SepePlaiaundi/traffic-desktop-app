using System.Collections.Generic;
using System.Windows.Controls;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Dashboard
{
    public partial class IncidentsList : UserControl
    {
        public List<IncidentItem> Incidents { get; set; }

        public IncidentsList()
        {
            InitializeComponent();

            Incidents = new List<IncidentItem>
            {
                new IncidentItem("Accidente", "AP-8", "GI"),
                new IncidentItem("Obras", "GI-20", "GI"),
                new IncidentItem("Desvío temporal", "A-1", "GI"),
                new IncidentItem("Accidente grave", "Calle Lezo, Km-20", "GI"),
                new IncidentItem("Obras", "BI-13", "BI"),
                new IncidentItem("Obras", "AP-1", "AR"),
                new IncidentItem("Accidente", "A-1", "AR")
            };

            DataContext = this;
        }
    }
}