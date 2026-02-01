using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TrafficDesktopApp.Controls.General
{
    public partial class ToastNotification : UserControl
    {
        public ToastNotification()
        {
            InitializeComponent();
        }

        public async void Show(string message, int durationSeconds = 3)
        {
            ToastText.Text = message;
            ToastBorder.Visibility = Visibility.Visible;

            // Fade In
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            ToastBorder.BeginAnimation(OpacityProperty, fadeIn);

            await Task.Delay(TimeSpan.FromSeconds(durationSeconds));

            // Fade Out
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            fadeOut.Completed += (s, e) => ToastBorder.Visibility = Visibility.Collapsed;
            ToastBorder.BeginAnimation(OpacityProperty, fadeOut);
        }
    }
}
