using System.Collections.ObjectModel;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.Events;
using USBDevicesLibrary.USBDevices;

namespace USBDevicesLibrary;

public class USBDevicesList 
{
    public event EventHandler<USBDevicesEventArgs>? DeviceChanged;
    public event EventHandler? InitialCollectionsComplete;

    public ObservableCollection<USBHub> USBHubs { get; set; }
    public ObservableCollection<Device> USBDevicesFromSetupAPI { get; set; }
    public ObservableCollection<USBDevice> USBDevices { get; set; }

    private readonly USBDevicesEventManager eventMnager;

    public bool InitialCompleted;

    public USBDevicesList()
    {
        USBHubs = [];
        USBDevicesFromSetupAPI = [];
        USBDevices = [];
        eventMnager = new(this);
        InitialCompleted = false;
    }

    public void Start()
    {
        USBDevicesListHelpers.UpdateHubCollection(USBHubs);
        USBDevicesListHelpers.UpdateUSBDevicesFromSetupAPICollection(USBDevicesFromSetupAPI);
        USBDevicesListHelpers.UpdateUSBDevicesCollection(USBHubs, USBDevicesFromSetupAPI, USBDevices);
        InitialCompleted = true;
        OnInitialCollectionsComplete(EventArgs.Empty);
    }

    protected virtual void OnInitialCollectionsComplete(EventArgs e)
    {
        InitialCollectionsComplete?.Invoke(this, e);
    }

    internal virtual void OnDeviceChanged(USBDevicesEventArgs e)
    {
        DeviceChanged?.Invoke(this, e);
    }
}
