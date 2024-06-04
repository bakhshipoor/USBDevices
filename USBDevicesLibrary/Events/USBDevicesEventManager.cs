using System.Windows.Threading;

namespace USBDevicesLibrary.Events;

public class USBDevicesEventManager
{
    public USBDevicesEventManager(USBDevicesList usbDevicesList)
    {
        usbDevices = usbDevicesList;
        dispatcherTimer = new()
        {
            Interval = new TimeSpan(0, 0, 0, 0, 500)
        };

        dispatcherTimer.Tick -= DispatcherTimer_Tick;
        dispatcherTimer.Tick += DispatcherTimer_Tick;
        dispatcherTimer.Start();

    }
    private readonly USBDevicesList usbDevices;
    private readonly DispatcherTimer dispatcherTimer;
    private bool isProcessingConnectedDevices = false;
    private bool isProcessingDisconnectedDevices = false;
    private bool isProcessingModifiedDevices = false;

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        DispatcherTimer? dispatcherTimer = (DispatcherTimer?)sender;
        if (dispatcherTimer != null)
        {

        }
    }

}
