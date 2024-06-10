using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static USBDevicesLibrary.Win32API.NTDDStorData;

namespace USBDevicesLibrary.Interfaces.Storage;

public class StorageDeviceDescriptor
{
    public StorageDeviceDescriptor()
    {
        VendorId = string.Empty;
        ProductId = string.Empty;
        ProductRevision = string.Empty;
        SerialNumber = string.Empty;
    }

    public byte DeviceType { get; set; }
    public byte DeviceTypeModifier { get; set; }
    public bool RemovableMedia { get; set; }
    public bool CommandQueueing { get; set; }
    public string VendorId { get; set; }
    public string ProductId { get; set; }
    public string ProductRevision { get; set; }
    public string SerialNumber { get; set; }
    public STORAGE_BUS_TYPE BusType { get; set; }
}
