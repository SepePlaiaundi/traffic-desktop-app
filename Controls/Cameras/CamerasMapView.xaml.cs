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
            Map.Position = new PointLatLng(43.10, -2.50);

            Map.MinZoom = 8;
            Map.MaxZoom = 18;
            Map.Zoom = 10;

            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
            Map.IgnoreMarkerOnMouseWheel = true;
            Map.ShowCenter = false;
            Map.ShowTileGridLines = false;

            Map.BoundsOfMap = new RectLatLng(
                43.45,
                -3.45,
                1.75,
                0.55
            );
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

                // Conversión UTM → WGS84 si aplica
                if (lat > 90 || lon > 180)
                {
                    var converted = Helpers.CoordinateConverter.UtmToWgs84(
                        x: lon,
                        y: lat
                    );

                    lat = converted.Latitude;
                    lon = converted.Longitude;
                }

                var marker = new GMapMarker(new PointLatLng(lat, lon))
                {
                    Shape = CreateCameraMarker(cam),
                    Offset = new Point(-MarkerSize / 2, -MarkerSize)
                };

                Map.Markers.Add(marker);
            }
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
