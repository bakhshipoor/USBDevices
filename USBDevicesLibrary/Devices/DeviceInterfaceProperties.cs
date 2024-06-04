using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Devices;

public class DeviceInterfaceProperties
{
    public DeviceInterfaceProperties()
    {
        DeviceInterface_FriendlyName = string.Empty;
        DeviceInterface_ReferenceString = string.Empty;
        DeviceInterface_SchematicName = string.Empty;
        DeviceInterfaceClass_DefaultInterface = string.Empty;
        DeviceInterfaceClass_Name = string.Empty;

        DeviceInterface_UnrestrictedAppCapabilities = [];

        OtherProperties = new();
    }

    // Device interface properties
    // # 7
    public string DeviceInterface_FriendlyName { get; set; }
    public bool DeviceInterface_Enabled { get; set; }
    public Guid DeviceInterface_ClassGuid { get; set; }
    public string DeviceInterface_ReferenceString { get; set; }
    public bool DeviceInterface_Restricted { get; set; }
    public List<string> DeviceInterface_UnrestrictedAppCapabilities { get; set; }
    public string DeviceInterface_SchematicName { get; set; }

    // Device interface class properties
    // # 2
    public string DeviceInterfaceClass_DefaultInterface { get; set; }
    public string DeviceInterfaceClass_Name { get; set; }

    public SortedList<string, object> OtherProperties { get; set; }
}
