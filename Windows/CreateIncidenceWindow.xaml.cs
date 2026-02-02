using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficDesktopApp.Windows
{
    /// <summary>
    /// Ventana modal para la creación y registro de nuevas incidencias de tráfico.
    /// Ahora incluye un mapa interactivo para seleccionar las coordenadas.
    /// </summary>
    public partial class CreateIncidenceWindow : Window
    {
        private GMapMarker _selectionMarker;

        public CreateIncidenceWindow()
        {
            InitializeComponent();
            StartDatePicker.SelectedDate = DateTime.Now;
            InitializeMap();
            LoadResources();
        }

        private async void LoadResources()
        {
            var recursos = await RecursoService.GetAllAsync();
            RecursoComboBox.ItemsSource = recursos;
            if (recursos.Count > 0)
            {
                RecursoComboBox.SelectedIndex = 0;
            }
        }

        private void InitializeMap()
        {
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            MapSelection.MapProvider = OpenStreetMapProvider.Instance;
            
            // Posición inicial (País Vasco)
            MapSelection.Position = new PointLatLng(43.10, -2.50);
            MapSelection.MinZoom = 8;
            MapSelection.MaxZoom = 18;
            MapSelection.Zoom = 9;

            MapSelection.CanDragMap = true;
            MapSelection.DragButton = MouseButton.Left;
            MapSelection.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            MapSelection.ShowCenter = false;
        }

        private void MapSelection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Obtener coordenadas del punto clickeado
            Point mousePos = e.GetPosition(MapSelection);
            PointLatLng point = MapSelection.FromLocalToLatLng((int)mousePos.X, (int)mousePos.Y);

            // Actualizar campos de texto
            LatBox.Text = point.Lat.ToString("F6");
            LngBox.Text = point.Lng.ToString("F6");

            // Actualizar o crear marcador
            UpdateSelectionMarker(point);
        }

        private void UpdateSelectionMarker(PointLatLng point)
        {
            if (_selectionMarker == null)
            {
                _selectionMarker = new GMapMarker(point)
                {
                    Shape = new Ellipse
                    {
                        Width = 16,
                        Height = 16,
                        Fill = (Brush)new BrushConverter().ConvertFrom("#F5B400"),
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        Effect = new System.Windows.Media.Effects.DropShadowEffect { BlurRadius = 4, ShadowDepth = 2, Opacity = 0.5 }
                    },
                    Offset = new Point(-8, -8)
                };
                MapSelection.Markers.Add(_selectionMarker);
            }
            else
            {
                _selectionMarker.Position = point;
            }
        }


        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validación básica
                if (string.IsNullOrWhiteSpace(CauseBox.Text) || string.IsNullOrWhiteSpace(RoadBox.Text))
                {
                    ShowError("Por favor, rellene los campos obligatorios (Causa, Carretera).");
                    return;
                }

                if (RecursoComboBox.SelectedItem == null)
                {
                    ShowError("Por favor, seleccione una fuente o recurso.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(LatBox.Text))
                {
                    ShowError("Por favor, seleccione una ubicación en el mapa.");
                    return;
                }

                double lat = 0, lng = 0;
                double.TryParse(LatBox.Text, out lat);
                double.TryParse(LngBox.Text, out lng);

                var selectedRecurso = (Recurso)RecursoComboBox.SelectedItem;
                
                // Buscamos el primer ID disponible para este recurso empezando desde 0
                int nextId = await FindNextAvailableId(selectedRecurso.Id);

                var incidence = new Incidence
                {
                    Id = nextId,
                    Cause = CauseBox.Text,
                    Road = RoadBox.Text,
                    City = CityBox.Text,
                    Province = ProvinceBox.Text,
                    Direction = DirectionBox.Text,
                    Latitude = lat,
                    Longitude = lng,
                    Level = LevelBox.Text,
                    Type = TypeBox.Text,
                    Description = DescriptionBox.Text,
                    StartDate = StartDatePicker.SelectedDate ?? DateTime.Now,
                    EndDate = EndDatePicker.SelectedDate,
                    Recurso = selectedRecurso,
                    IsNew = true
                };

                bool success = await IncidenceService.CreateIncidenceAsync(incidence);

                if (success)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    ShowError("Error al crear la incidencia. Verifique la conexión con el servidor.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error: " + ex.Message);
            }
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorBorder.Visibility = Visibility.Visible;
        }

        private async Task<int> FindNextAvailableId(int recursoId)
        {
            // Consultamos las incidencias para ver cuáles están ocupadas
            // Usamos un filtro de fecha amplio para pillar el max histórico si es necesario
            var existing = await IncidenceService.GetIncidencesAsync(1, 1, 2020);
            
            if (existing == null || existing.Count == 0) return 0;

            // Obtenemos los IDs ocupados para este recurso específico
            var occupiedIds = existing
                .Where(i => i.Recurso != null && i.Recurso.Id == recursoId)
                .Select(i => i.Id)
                .OrderBy(id => id)
                .ToList();

            // Buscamos el primer hueco empezando desde 0
            int candidate = 0;
            foreach (var id in occupiedIds)
            {
                if (id == candidate)
                {
                    candidate++;
                }
                else if (id > candidate)
                {
                    break; // Hemos encontrado un hueco
                }
            }
            return candidate;
        }
    }
}
