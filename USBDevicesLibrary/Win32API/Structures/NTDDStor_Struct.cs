using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Data;
using USBDevicesLibrary.Devices;
using static USBDevicesLibrary.Win32API.WinIOCtlData;
using System.Dynamic;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Diagnostics.Metrics;
using System.Windows.Media;
using System.Windows;

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDStorData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntddstor/
    public struct STORAGE_DEVICE_NUMBER
    {
        public DEVICE_TYPES DeviceType;
        public uint DeviceNumber;
        public uint PartitionNumber;
    }
}
