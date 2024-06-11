using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDVolData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntddvol/

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size =24, CharSet = CharSet.Unicode)]
    public struct DISK_EXTENT
    {
        public uint DiskNumber;
        public ulong StartingOffset;
        public ulong ExtentLength;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size =32*254*2, CharSet = CharSet.Unicode)]
    public struct VOLUME_DISK_EXTENTS
    {
        public uint NumberOfDiskExtents;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public DISK_EXTENT[] Extents;
    }
}
