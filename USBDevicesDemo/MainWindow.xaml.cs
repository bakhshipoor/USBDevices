using System.Windows;
using USBDevicesLibrary;

namespace USBDevicesDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        USBDCollection = new();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        USBDCollection.Start();
    }

    public USBDevicesList USBDCollection { get; set; }
}

