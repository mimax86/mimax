#nullable enable

using System.Windows;

namespace Mimax.Wpf.Core3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class Address
    {
        public string? Building { get; set; }
    }
}
