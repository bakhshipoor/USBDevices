using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Devices;

public class DeviceDriverProperties
{
    public DeviceDriverProperties()
    {
        DrvPkg_Model = string.Empty;
        DrvPkg_VendorWebSite = string.Empty;
        DrvPkg_DetailedDescription = string.Empty;
        DrvPkg_DocumentationLink = string.Empty;

        DrvPkg_Icon = [];
        DrvPkg_BrandingIcon = [];

        OtherProperties = new();
    }

    // Device properties
    // These DEVPKEYs may be set by the driver package installed for a device.
    // # 6
    public string DrvPkg_Model { get; set; }
    public string DrvPkg_VendorWebSite { get; set; }
    public string DrvPkg_DetailedDescription { get; set; }
    public string DrvPkg_DocumentationLink { get; set; }
    public List<string> DrvPkg_Icon { get; set; }
    public List<string> DrvPkg_BrandingIcon { get; set; }

    public SortedList<string, object> OtherProperties { get; set; }
}
