using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TrafficDesktopApp.Services
{
    /// <summary>
    /// Utilidad para capturar la apariencia visual de un elemento de la interfaz (UIElement) y convertirlo en un array de bytes (imagen PNG).
    /// </summary>
    public static class UiElementCapturer
    {
        public static byte[] Capture(FrameworkElement element)
        {
            if (element == null)
                return null;

            var size = new Size(element.ActualWidth, element.ActualHeight);
            if (size.Width == 0 || size.Height == 0)
                return null;

            // Forzar actualización de layout para asegurar que todo esté renderizado (especialmente etiquetas de ejes)
            element.Measure(size);
            element.Arrange(new Rect(size));
            element.UpdateLayout();

            var renderBitmap = new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96,
                96,
                PixelFormats.Pbgra32);

            renderBitmap.Render(element);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
