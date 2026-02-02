using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Windows;

namespace TrafficDesktopApp.Controls.Incidences
{
    /// <summary>
    /// Vista de tabla detallada para el listado de incidencias con filtrado dinámico.
    /// </summary>
    public partial class IncidencesListView : UserControl
    {
        private List<Incidence> _allIncidents = new List<Incidence>();
        public ObservableCollection<Incidence> VisibleIncidents { get; }

        public IncidencesListView()
        {
            InitializeComponent();
            VisibleIncidents = new ObservableCollection<Incidence>();
            DataContext = this;
        }

        public void SetIncidences(List<Incidence> incidences)
        { 
            _allIncidents = incidences;
            ApplyFilter(null);
        }

        public void ApplyFilter(string type)
        {
            VisibleIncidents.Clear();

            IEnumerable<Incidence> filtered =
                type == null
                    ? _allIncidents
                    : _allIncidents.Where(i => i.Type == type);

            foreach (var item in filtered)
                VisibleIncidents.Add(item);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dg && dg.SelectedItem is Incidence incidence)
            {
                var details = new IncidenceDetails(incidence);
                details.Owner = Window.GetWindow(this);
                details.ShowDialog();
            }
        }
    }
}
