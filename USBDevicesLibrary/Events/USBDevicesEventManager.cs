using System.Collections.ObjectModel;
using System.Windows.Threading;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Events.EventTypesEnum;

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
    //private bool isProcessingConnectedDevices = false;
    //private bool isProcessingDisconnectedDevices = false;
    //private bool isProcessingModifiedDevices = false;

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        DispatcherTimer? dispatcherTimer = (DispatcherTimer?)sender;
        if (dispatcherTimer != null && usbDevices.InitialCompleted)
        {
            dispatcherTimer.Stop();

            ObservableCollection<Device> usbDevicesFromSetupAPI = [];
            USBDevicesListHelpers.UpdateUSBDevicesFromSetupAPICollection(usbDevicesFromSetupAPI);

            ObservableCollection<Device> disconnectedDevices = [];
            ObservableCollection<Device> connectedDevices = [];

            // Check for disconnected devices
            foreach (Device itemOldDevice in usbDevices.USBDevicesFromSetupAPI)
            {
                bool find = false;
                foreach (Device itemNewDevice in usbDevicesFromSetupAPI)
                {
                    if (itemOldDevice.DeviceProperties.Device_InstanceId.Equals(itemNewDevice.DeviceProperties.Device_InstanceId, StringComparison.OrdinalIgnoreCase))
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    // Device Disconnected
                    disconnectedDevices.Add(itemOldDevice);
                }
            }
            if (disconnectedDevices.Count > 0)
                OnDevicesDisconnected(disconnectedDevices);

            // Check for connected devices
            foreach (Device itemNewDevice in usbDevicesFromSetupAPI)
            {
                bool find = false;
                foreach (Device itemOldDevice in usbDevices.USBDevicesFromSetupAPI)
                {
                    if (itemNewDevice.DeviceProperties.Device_InstanceId.Equals(itemOldDevice.DeviceProperties.Device_InstanceId, StringComparison.OrdinalIgnoreCase))
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    // Device Connected
                    connectedDevices.Add(itemNewDevice);
                }
            }
            if (connectedDevices.Count > 0)
                OnDevicesConnected(connectedDevices);

            dispatcherTimer.Start();
        }
    }

    private void OnDevicesDisconnected(ObservableCollection<Device> disconnectedDevices)
    {
        usbDevices.USBHubs.Clear();
        USBDevicesListHelpers.UpdateHubCollection(usbDevices.USBHubs);
        ObservableCollection<USBDevice> disconnectedUSBDevices = [];
        foreach (Device itemDisconnectedDevice in disconnectedDevices)
        {
            usbDevices.USBDevicesFromSetupAPI.Remove(itemDisconnectedDevice);
            foreach (USBDevice itemUSBDevice in usbDevices.USBDevices)
            {
                if (itemDisconnectedDevice.DeviceProperties.Device_InstanceId.Equals(itemUSBDevice.BaseDeviceProperties.Device_InstanceId, StringComparison.OrdinalIgnoreCase))
                {
                    disconnectedUSBDevices.Add(itemUSBDevice);
                }
            }
        }
        foreach (USBDevice itemUSBDevice in disconnectedUSBDevices)
        {
            // Remove Device From Collection
            usbDevices.USBDevices.Remove(itemUSBDevice);
        }
    }

    private void OnDevicesConnected(ObservableCollection<Device> connectedDevices)
    {
        usbDevices.USBHubs.Clear();
        USBDevicesListHelpers.UpdateHubCollection(usbDevices.USBHubs);
        foreach (Device itemDevice in connectedDevices)
        {
            usbDevices.USBDevicesFromSetupAPI.Add(itemDevice);
            USBDevice newUSBDevice = USBDevicesListHelpers.CreateUSBDevice(usbDevices.USBHubs, itemDevice);
            usbDevices.USBDevices.Add(newUSBDevice);
        }
    }
}
