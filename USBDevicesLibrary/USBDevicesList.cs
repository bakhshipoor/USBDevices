using System.Collections.ObjectModel;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.USBDevices;

namespace USBDevicesLibrary;

public class USBDevicesList 
{
    private ObservableCollection<USBHub> _USBHubs { get; set; }
    private ObservableCollection<Device> _USBDevicesFromSetupAPI { get; set; }
    public ObservableCollection<USBDevice> USBDevices { get; set; }
    
    public USBDevicesList()
    {
        _USBHubs = new();
        _USBDevicesFromSetupAPI = new();
        USBDevices = new();
    }

    public void Start()
    {
        USBDevicesListHelpers.UpdateHubCollection(_USBHubs);
        USBDevicesListHelpers.UpdateUSBDevicesFromSetupAPICollection(_USBDevicesFromSetupAPI);
        USBDevicesListHelpers.UpdateUSBDevicesCollection(_USBHubs, _USBDevicesFromSetupAPI, USBDevices);
    }
}
