using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace USBDevicesLibrary.Devices;

public class DeviceClassProperties
{
    public DeviceClassProperties()
    {
        NAME = string.Empty;
        DeviceClass_SecuritySDS = string.Empty;
        DeviceClass_Name = string.Empty;
        DeviceClass_ClassName = string.Empty;
        DeviceClass_Icon = string.Empty;
        DeviceClass_ClassInstaller = string.Empty;
        DeviceClass_PropPageProvider = string.Empty;
        DeviceClass_DefaultService = string.Empty;

        DeviceClass_UpperFilters = [];
        DeviceClass_LowerFilters = [];
        DeviceClass_IconPath = [];
        DeviceClass_ClassCoInstallers = [];

        OtherProperties = new();
    }
    // DEVPKEY_NAME
    // Common DEVPKEY used to retrieve the display name for an object.
    // # 1
    public string NAME { get; set; }

    // Device setup class properties
    // These DEVPKEYs correspond to the SetupAPI SPCRP_XXX setup class properties.
    // # 7
    public List<string> DeviceClass_UpperFilters { get; set; }
    public List<string> DeviceClass_LowerFilters { get; set; }
    public RawSecurityDescriptor? DeviceClass_Security { get; set; }
    public string DeviceClass_SecuritySDS { get; set; }
    public uint DeviceClass_DevType { get; set; }
    public bool DeviceClass_Exclusive { get; set; }
    public uint DeviceClass_Characteristics { get; set; }

    // Device setup class properties
    // # 11
    public string DeviceClass_Name { get; set; }
    public string DeviceClass_ClassName { get; set; }
    public string DeviceClass_Icon { get; set; }
    public string DeviceClass_ClassInstaller { get; set; }
    public string DeviceClass_PropPageProvider { get; set; }
    public bool DeviceClass_NoInstallClass { get; set; }
    public bool DeviceClass_NoDisplayClass { get; set; }
    public bool DeviceClass_SilentInstall { get; set; }
    public bool DeviceClass_NoUseClass { get; set; }
    public string DeviceClass_DefaultService { get; set; }
    public List<string> DeviceClass_IconPath { get; set; }

    // Other Device setup class properties
    // # 2
    public bool DeviceClass_DHPRebalanceOptOut { get; set; }
    public List<string> DeviceClass_ClassCoInstallers { get; set; }

    public SortedList<string, object> OtherProperties { get; set; }
}
