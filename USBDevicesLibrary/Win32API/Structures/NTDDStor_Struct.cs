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
using System.Runtime.InteropServices;

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDStorData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntddstor/
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct STORAGE_DEVICE_NUMBER
    {
        public DEVICE_TYPES DeviceType;
        public uint DeviceNumber;
        public uint PartitionNumber;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct MEDIA_SERIAL_NUMBER_DATA
    {
        public uint SerialNumberLength;
        public uint Result;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Reserved;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string SerialNumberData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct STORAGE_PROPERTY_QUERY
    {
        public STORAGE_PROPERTY_ID PropertyId;
        public STORAGE_QUERY_TYPE QueryType;
        public uint AdditionalParameters;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size =1060, CharSet = CharSet.Unicode)]
    public struct STORAGE_DEVICE_DESCRIPTOR
    {
        public uint Version;
        public uint Size;
        public byte DeviceType;
        public byte DeviceTypeModifier;
        public byte RemovableMedia;
        public byte CommandQueueing;
        public uint VendorIdOffset;
        public uint ProductIdOffset;
        public uint ProductRevisionOffset;
        public uint SerialNumberOffset;
        public STORAGE_BUS_TYPE BusType;
        public uint RawPropertiesLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] RawDeviceProperties;
    }
}
