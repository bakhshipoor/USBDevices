using static USBDevicesLibrary.Win32API.NTDDDiskData;
using System.Runtime.InteropServices;

namespace USBDevicesLibrary.Win32API;

public static partial class WinIOCtlData
{
    [StructLayout(LayoutKind.Sequential, Size = 4, CharSet = CharSet.Unicode)]
    public struct VOLUME_IS_DIRTY
    {
        public uint DirtyData;
    }
}
