using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Incidences
{
    /// <summary>
    /// Interaction logic for IncidentsListView.xaml
    /// </summary>
    public partial class IncidencesListView : UserControl
    {

        private List<Incidence> _allIncidents;
        public ObservableCollection<Incidence> VisibleIncidents { get; set; }

        public IncidencesListView()
        {
            InitializeComponent();

            _allIncidents = new List<Incidence>
            {
                new Incidence{ Id=16345, Description="Obras en la calzada", Road="AP-8", Province="GI", Type=IncidenceType.Obras, Date="Dic 5"},
                new Incidence{ Id=16346, Description="Accidente", Road="A-1", Province="AR", Type=IncidenceType.Accidente, Date="Dic 5"},
                new Incidence{ Id=16347, Description="Desvío temporal", Road="GI-20", Province="GI", Type=IncidenceType.Otro, Date="Dic 4"},
            };

            VisibleIncidents = new ObservableCollection<Incidence>(_allIncidents);
            DataContext = this;
        }


        public void ApplyFilter(IncidenceType? filter)
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
