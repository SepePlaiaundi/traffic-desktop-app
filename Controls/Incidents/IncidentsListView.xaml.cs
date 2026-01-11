using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Incidents
{
    /// <summary>
    /// Interaction logic for IncidentsListView.xaml
    /// </summary>
    public partial class IncidentsListView : UserControl
    {
        public ObservableCollection<Incident> AllIncidents { get; set; }
        public ObservableCollection<Incident> VisibleIncidents { get; set; }

        public IncidentsListView()
        {
            InitializeComponent();

            AllIncidents = new ObservableCollection<Incident>
        {
            new Incident{ Id=16345, Description="Obras en la calzada", Road="AP-8", Province="GI", Type=IncidentType.Obras, Date="Dic 5"},
            new Incident{ Id=16346, Description="Accidente", Road="A-1", Province="AR", Type=IncidentType.Accidente, Date="Dic 5"},
            new Incident{ Id=16347, Description="Desvío temporal", Road="GI-20", Province="GI", Type=IncidentType.Otro, Date="Dic 4"},
        };

            VisibleIncidents = new ObservableCollection<Incident>(AllIncidents);
            DataContext = this;
        }

        public void ApplyFilter(string filter)
        {
            VisibleIncidents.Clear();

            IEnumerable<Incident> result;

            if (filter == "Works")
                result = AllIncidents.Where(i => i.Type == IncidentType.Obras);
            else if (filter == "Accidents")
                result = AllIncidents.Where(i => i.Type == IncidentType.Accidente);
            else if (filter == "Others")
                result = AllIncidents.Where(i => i.Type == IncidentType.Otro);
            else
                result = AllIncidents;

            foreach (var i in result)
                VisibleIncidents.Add(i);
        }
    }
}
