using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Windows;

namespace TrafficDesktopApp.Controls.Cameras
{
    /// <summary>
    /// Vista de mapa interactiva que posiciona las cámaras de tráfico utilizando coordenadas geográficas o UTM convertidas.
    /// </summary>
    public partial class CamerasMapView : UserControl
    {
        private const double MarkerSize = 40;

        public CamerasMapView()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            Map.MapProvider = OpenStreetMapProvider.Instance;
            Map.Position = new PointLatLng(43.10, -2.50); // Centro aproximado

            Map.MinZoom = 8;
            Map.MaxZoom = 18;
            Map.Zoom = 10;

            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
            Map.IgnoreMarkerOnMouseWheel = true;
            Map.ShowCenter = false;
            Map.ShowTileGridLines = false;

            // Se ha eliminado el bloqueo de límites (Map.BoundsOfMap) para permitir movimiento libre
        }

        public void SetCameras(List<Camera> cameras)
        {
            Map.Markers.Clear();

            foreach (var cam in cameras)
            {
                if (!cam.Latitude.HasValue || !cam.Longitude.HasValue)
                    continue;

                double lat = cam.Latitude.Value;
                double lon = cam.Longitude.Value;

                // 1. INTENTO DE CONVERSIÓN UTM -> WGS84
                // Si los valores son muy altos, asumimos que son UTM y convertimos
                if (lat > 90 || lon > 180 || lat < -90 || lon < -180)
                {
                    try
                    {
                        var converted = Helpers.CoordinateConverter.UtmToWgs84(
                            x: lon,
                            y: lat
                        );
                        lat = converted.Latitude;
                        lon = converted.Longitude;
                    }
                    catch
                    {
                        // Si falla la conversión, saltamos esta cámara
                        continue;
                    }
                }

                // 2. FILTRO DE VALIDACIÓN GEOGRÁFICA (El Fix)
                // Descartamos coordenadas que, aunque sean numéricamente válidas,
                // apunten al mar, a Francia o al 0,0 por error de datos.
                // Rango aproximado extendido (Norte de España / Sur de Francia)
                if (!IsLocationValid(lat, lon))
                {
                    continue;
                }

                var marker = new GMapMarker(new PointLatLng(lat, lon))
                {
                    Shape = CreateCameraMarker(cam),
                    Offset = new Point(-MarkerSize / 2, -MarkerSize)
                };

                Map.Markers.Add(marker);
            }
        }

        /// <summary>
        /// Comprueba si la coordenada cae dentro de un rectángulo lógico alrededor del País Vasco.
        /// Esto evita que datos corruptos (como el de Jersey) aparezcan en el mapa.
        /// </summary>
        private bool IsLocationValid(double lat, double lon)
        {
            // Latitud aprox Euskadi: 42.4 a 43.5
            // Longitud aprox Euskadi: -3.5 a -1.7
            // Damos un margen amplio de seguridad:
            double minLat = 41.0;
            double maxLat = 44.0;
            double minLon = -5.0;
            double maxLon = -1.0;

            return (lat >= minLat && lat <= maxLat) && (lon >= minLon && lon <= maxLon);
        }

        private UIElement CreateCameraMarker(Camera cam)
        {
            var image = new Image
            {
                Width = MarkerSize,
                Height = MarkerSize,
                Stretch = Stretch.Uniform,
                Source = new BitmapImage(
                    new System.Uri("pack://application:,,,/Assets/camera.png")),
                RenderTransformOrigin = new Point(0.5, 1.0),
                Cursor = Cursors.Hand,
                ToolTip = cam.CameraName
            };

            // Hover suave (sin agrandar)
            image.MouseEnter += (_, __) => image.Opacity = 0.85;
            image.MouseLeave += (_, __) => image.Opacity = 1.0;

            // Click → detalle cámara
            image.MouseLeftButtonUp += (_, e) =>
            {
                var detailsWindow = new CameraDetails(cam)
                {
                    Owner = Window.GetWindow(this)
                };

                detailsWindow.ShowDialog();
                e.Handled = true;
            };

            return image;
        }
    }
}