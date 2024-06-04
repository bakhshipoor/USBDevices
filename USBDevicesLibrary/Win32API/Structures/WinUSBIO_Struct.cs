using System.Runtime.InteropServices;

namespace USBDevicesLibrary.Win32API;

public static partial class WinUSBIO
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WINUSB_PIPE_INFORMATION
    {
        public USB.USBD_PIPE_TYPE PipeType;
        public byte PipeId;
        public ushort MaximumPacketSize;
        public byte Interval;
    }

    
}
