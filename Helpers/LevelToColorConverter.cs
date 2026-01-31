using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TrafficDesktopApp.Helpers
{
    public class LevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string level = value as string;
            if (string.IsNullOrEmpty(level)) return Brushes.Gray;

            switch (level.ToUpper())
            {
                case "BLANCO":
                    return (Brush)new BrushConverter().ConvertFrom("#9CA3AF"); // Gris azulado
                case "AMARILLO":
                    return (Brush)new BrushConverter().ConvertFrom("#F59E0B"); // Ámbar/Naranja
                case "ROJO":
                    return (Brush)new BrushConverter().ConvertFrom("#EF4444"); // Rojo
                case "NEGRO":
                    return Brushes.Black;
                case "VERDE":
                    return (Brush)new BrushConverter().ConvertFrom("#10B981"); // Verde
                default:
                    return Brushes.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
