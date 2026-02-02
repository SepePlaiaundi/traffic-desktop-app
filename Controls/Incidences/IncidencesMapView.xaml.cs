using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Windows;

namespace TrafficDesktopApp.Controls.Incidences
{
    /// <summary>
    /// Vista de mapa interactiva para la visualización geoespacial de incidencias de tráfico.
    /// </summary>
    public partial class IncidencesMapView : UserControl
    {
        private const double MarkerSize = 48;

        public IncidencesMapView()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            Map.MapProvider = OpenStreetMapProvider.Instance;
            Map.Position = new PointLatLng(43.10, -2.50);

            Map.MinZoom = 8;
            Map.MaxZoom = 18;
            Map.Zoom = 10;

            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.ShowCenter = false;
            Map.IgnoreMarkerOnMouseWheel = true;
            Map.ShowTileGridLines = false;
        }

        public void SetIncidences(List<Incidence> incidences)
        {
            Map.Markers.Clear();

            foreach (var inc in incidences)
            {
                // Validación de coordenadas (evitar 0,0 o puntos fuera de rango lógico)
                if (inc.Latitude == 0 || inc.Longitude == 0 || !IsLocationValid(inc.Latitude, inc.Longitude))
                    continue;

                var marker = new GMapMarker(
                    new PointLatLng(inc.Latitude, inc.Longitude))
                {
                    Shape = CreateIncidenceMarker(inc),
                    Offset = new Point(-MarkerSize / 2, -MarkerSize)
                };

                Map.Markers.Add(marker);
            }
        }

        private bool IsLocationValid(double lat, double lon)
        {
            // Rango aproximado para filtrar datos erróneos (similar a cámaras)
            return lat >= 41.0 && lat <= 44.0 && lon >= -5.0 && lon <= -1.0;
        }

        private UIElement CreateIncidenceMarker(Incidence inc)
        {
            var image = new Image
            {
                Width = MarkerSize,
                Height = MarkerSize,
                Stretch = Stretch.Uniform,
                Source = new BitmapImage(
                    new Uri(Helpers.IncidenceIconResolver.GetIcon(inc.Type))),
                Cursor = Cursors.Hand,
                ToolTip = $"{inc.Type} · {inc.Road}",
                RenderTransformOrigin = new Point(0.5, 1.0)
            };

            image.MouseEnter += (_, __) => image.Opacity = 0.85;
            image.MouseLeave += (_, __) => image.Opacity = 1.0;

            image.MouseLeftButtonUp += (s, e) =>
            {
                var details = new IncidenceDetails(inc)
                {
                    Owner = Window.GetWindow(this)
                };
                details.ShowDialog();
                e.Handled = true;
            };

            return image;
        }
    }
}
