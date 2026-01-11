using System.Windows;
using System.Windows.Controls;

namespace TrafficDesktopApp.Controls
{
    /// <summary>
    /// Lógica de interacción para DashboardHeader.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(Header),
                new PropertyMetadata("Panel de control"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Header()
        {
            InitializeComponent();
        }
    }
}
