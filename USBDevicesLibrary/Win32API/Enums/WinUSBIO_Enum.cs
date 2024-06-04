namespace USBDevicesLibrary.Win32API;

public static partial class WinUSBIO
{
    // Device Information types
    public const uint DEVICE_SPEED = 0x01;

    // Device Speeds
    public enum DeviceSpeeds : byte
    {
        UnKnown = 0x00,
        LowSpeed = 0x01,
        FullSpeed = 0x02,
        HighSpeed = 0x03,
    }

    // Pipe policy types
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/usbcon/winusb-functions-for-pipe-policy-modification
    public enum PIPE_POLICY_TYPES : uint
    {
        SHORT_PACKET_TERMINATE = 0x01,
        AUTO_CLEAR_STALL = 0x02,
        PIPE_TRANSFER_TIMEOUT = 0x03,
        IGNORE_SHORT_PACKETS = 0x04,
        ALLOW_PARTIAL_READS = 0x05,
        AUTO_FLUSH = 0x06,
        RAW_IO = 0x07,
        MAXIMUM_TRANSFER_SIZE = 0x08,
        RESET_PIPE_ON_RESUME = 0x09,
    }
    

}
