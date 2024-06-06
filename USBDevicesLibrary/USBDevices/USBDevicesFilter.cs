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
        
    }

    public ushort IDVendor { get; set; }
    public ushort IDProduct { get; set; }
}
