using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Dashboard
{
    public partial class IncidentsList : UserControl
    {
        public List<Incidence> Incidents
        {
            get => (List<Incidence>)GetValue(IncidentsProperty);
            set => SetValue(IncidentsProperty, value);
        }

        public static readonly DependencyProperty IncidentsProperty =
            DependencyProperty.Register(
                nameof(Incidents),
                typeof(List<Incidence>),
                typeof(IncidentsList),
                new PropertyMetadata(null));

        public IncidentsList()
        {
            InitializeComponent();
        }
    }
}
