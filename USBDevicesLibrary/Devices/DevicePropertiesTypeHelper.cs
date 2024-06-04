using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Devices;

public static class DevicePropertiesTypeHelper
{
    public enum DevicePropertiesTtype
    {
        DeviceProperties,
        ClassProperties,
        InterfaceProperties,
        ContainerProperties,
        DriverProperties,
    }
}
