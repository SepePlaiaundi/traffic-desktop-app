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

                var marker = new GMapMarker(
                    new PointLatLng(cam.Latitude.Value, cam.Longitude.Value))
                {
                    Shape = CreateCameraMarker(cam),
                    Offset = new Point(-20, -40)
                };

                Map.Markers.Add(marker);
            }
        }

        private UIElement CreateCameraMarker(Camera cam)
        {
            var image = new Image
            {
                Width = 40,
                Height = 40,
                Source = new BitmapImage(
                    new System.Uri("pack://application:,,,/Assets/camera.png")),
                RenderTransformOrigin = new Point(0.5, 1.0),
                Cursor = Cursors.Hand,
                ToolTip = cam.CameraName
            };

            var scale = new ScaleTransform(1.0, 1.0);
            image.RenderTransform = scale;

            // Hover animation
            image.MouseEnter += (s, e) =>
            {
                scale.ScaleX = 1.25;
                scale.ScaleY = 1.25;
                image.Opacity = 0.95;
            };

            image.MouseLeave += (s, e) =>
            {
                scale.ScaleX = 1.0;
                scale.ScaleY = 1.0;
                image.Opacity = 1.0;
            };

            // CLICK → OPEN CAMERA DETAILS
            image.MouseLeftButtonUp += (s, e) =>
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
