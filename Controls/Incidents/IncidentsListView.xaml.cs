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

        private List<Incident> _allIncidents;
        public ObservableCollection<Incident> VisibleIncidents { get; set; }

        public IncidentsListView()
        {
            InitializeComponent();

            _allIncidents = new List<Incident>
            {
                new Incident{ Id=16345, Description="Obras en la calzada", Road="AP-8", Province="GI", Type=IncidentType.Obras, Date="Dic 5"},
                new Incident{ Id=16346, Description="Accidente", Road="A-1", Province="AR", Type=IncidentType.Accidente, Date="Dic 5"},
                new Incident{ Id=16347, Description="Desvío temporal", Road="GI-20", Province="GI", Type=IncidentType.Otro, Date="Dic 4"},
            };

            VisibleIncidents = new ObservableCollection<Incident>(_allIncidents);
            DataContext = this;
        }


        public void ApplyFilter(IncidentType? filter)
        {
            VisibleIncidents.Clear();

            var filtered = filter == null
                ? _allIncidents
                : _allIncidents.Where(i => i.Type == filter);

            foreach (var item in filtered)
                VisibleIncidents.Add(item);
        }
    }
}
