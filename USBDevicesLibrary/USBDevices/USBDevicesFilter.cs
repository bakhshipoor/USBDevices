using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.USBDevices;

public class USBDevicesFilter
{
    public USBDevicesFilter()
    {
        IDVendor = string.Empty;
        IDProduct = string.Empty;
        Service = string.Empty;
    }

    public string IDVendor { get; set; }
    public string IDProduct { get; set; }
    public string Service {  get; set; }
}
