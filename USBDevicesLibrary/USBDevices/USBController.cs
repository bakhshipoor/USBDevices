using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Devices;

namespace USBDevicesLibrary.USBDevices;

public class USBController /*: Device*/
{
    public USBController()
    {
        DevicePath = string.Empty;
        RootHubName = string.Empty;
    }

    //public USBController(Device device) : this()
    //{
    //    DevicePath = device.DevicePath;
    //    DeviceProperties = device.DeviceProperties;
    //    ClassProperties = device.ClassProperties;
    //    InterfaceProperties = device.InterfaceProperties;
    //    ContainerProperties = device.ContainerProperties;
    //}

    public string DevicePath { get; set; }
    public string RootHubName {  get; set; }

}
