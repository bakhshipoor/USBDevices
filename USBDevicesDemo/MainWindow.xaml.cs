using System.Windows;
using USBDevicesLibrary;
using USBDevicesLibrary.USBDevices;

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
        USBDCollection.DeviceChanged += USBDCollection_DeviceChanged;
        Loaded += MainWindow_Loaded;
    }

    private void USBDCollection_DeviceChanged(object? sender, USBDevicesLibrary.Events.USBDevicesEventArgs e)
    {
        string VIDPID = string.Format("USB Devise VID: {0:X4}\r\nUSB Device PID: {1:X4}\r\nUSB Device Name: {2}",e.Device.IDVendor,e.Device.IDProduct,e.Device.StringDescriptor_Product);
        string msg = "Event Type: " + e.EventType.ToString() + "\r\n\r\n\r\n" + VIDPID;
        MessageBox.Show(msg, "USB Device", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        USBDCollection.Start();
        trvMain.ItemsSource = USBDCollection.USBDevices;
        trvMain.SelectedItemChanged += TrvMain_SelectedItemChanged;
    }

    public USBDevicesList USBDCollection { get; set; }

    private void TrvMain_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue != null)
        {
            List<PropertiesToList> filedsToList = [.. ((USBDevice)e.NewValue).PropertiesToList()];
            itemUSB.ItemsSource = filedsToList;
        }
    }
}

