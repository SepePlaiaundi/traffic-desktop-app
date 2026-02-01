using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Incidences
{
    /// <summary>
    /// Barra lateral de navegación y filtrado para la sección de incidencias.
    /// </summary>
    public partial class IncidencesSidebar : UserControl
    {
        public event Action<string> FilterChanged;

        public IncidencesSidebar()
        {
            InitializeComponent();
            BuildItems();
            SetAllActive();
        }

        private void BuildItems()
        {
            foreach (var type in IncidenceTypes.All)
            {
                var dot = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.LightGray,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center
                };

                var text = new TextBlock
                {
                    Text = type,
                    Margin = new System.Windows.Thickness(10, 0, 0, 0)
                };

                var panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new System.Windows.Thickness(0, 0, 0, 12)
                };

                panel.Children.Add(dot);
                panel.Children.Add(text);

                panel.MouseLeftButtonUp += (_, __) =>
                {
                    SetActive(dot, text);
                    FilterChanged?.Invoke(type);
                };

                ItemsPanel.Children.Add(panel);
            }
        }

        private void All_Click(object sender, MouseButtonEventArgs e)
        {
            SetAllActive();
            FilterChanged?.Invoke(null);
        }

        private void SetActive(Ellipse activeDot, TextBlock activeText)
        {
            ResetActive();
            activeDot.Fill = Brushes.Gold;
            activeText.FontWeight = System.Windows.FontWeights.SemiBold;
        }

        private void SetAllActive()
        {
            ResetActive();
            AllDot.Fill = Brushes.Gold;
            AllText.FontWeight = System.Windows.FontWeights.SemiBold;
        }

        private void ResetActive()
        {
            AllDot.Fill = Brushes.LightGray;
            AllText.FontWeight = System.Windows.FontWeights.Normal;

            foreach (StackPanel panel in ItemsPanel.Children)
            {
                ((Ellipse)panel.Children[0]).Fill = Brushes.LightGray;
                ((TextBlock)panel.Children[1]).FontWeight = System.Windows.FontWeights.Normal;
            }
        }
    }
}
