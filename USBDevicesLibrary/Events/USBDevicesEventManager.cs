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
    private bool isProcessingConnectedDevices = false;
    private bool isProcessingDisconnectedDevices = false;
    private bool isProcessingModifiedDevices = false;

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        DispatcherTimer? dispatcherTimer = (DispatcherTimer?)sender;
        if (dispatcherTimer != null && usbDevices.InitialCompleted)
        {
            dispatcherTimer.Stop();

            ObservableCollection<Device> usbDevicesFromSetupAPI = new();
            USBDevicesListHelpers.UpdateUSBDevicesFromSetupAPICollection(usbDevicesFromSetupAPI);

            ObservableCollection<Device> disconnectedDevices = new();
            ObservableCollection<Device> connectedDevices = new();

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
        usbDevices.USBDevicesFromSetupAPI.Clear();
        usbDevices.USBHubs.Clear();
        USBDevicesListHelpers.UpdateUSBDevicesFromSetupAPICollection(usbDevices.USBDevicesFromSetupAPI);
        USBDevicesListHelpers.UpdateHubCollection(usbDevices.USBHubs);
        ObservableCollection<USBDevice> disconnectedUSBDevices = new();
        foreach (Device itemDisconnectedDevice in disconnectedDevices)
        {
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
            // Trigger Event
            usbDevices.OnDeviceChanged(new USBDevicesEventArgs(itemUSBDevice, EventTypeEnum.Disconnected));
        }
    }

    private void OnDevicesConnected(ObservableCollection<Device> connectedDevices)
    {
        usbDevices.USBDevicesFromSetupAPI.Clear();
        usbDevices.USBHubs.Clear();
        usbDevices.USBDevices.Clear();
        USBDevicesListHelpers.UpdateUSBDevicesFromSetupAPICollection(usbDevices.USBDevicesFromSetupAPI);
        USBDevicesListHelpers.UpdateHubCollection(usbDevices.USBHubs);
        // To Do : Update Only Changes
        USBDevicesListHelpers.UpdateUSBDevicesCollection(usbDevices.USBHubs, usbDevices.USBDevicesFromSetupAPI, usbDevices.USBDevices);
        foreach (USBDevice itemUSBDevice in usbDevices.USBDevices)
        {
            foreach (Device itemDevice in connectedDevices)
            {
                if (itemUSBDevice.BaseDeviceProperties.Device_InstanceId.Equals(itemDevice.DeviceProperties.Device_InstanceId,StringComparison.OrdinalIgnoreCase))
                {
                    // Trigger Event
                    usbDevices.OnDeviceChanged(new USBDevicesEventArgs(itemUSBDevice, EventTypeEnum.Connected));
                }
            }
        }
    }
}
