using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// https://learn.microsoft.com/en-us/windows/win32/api/winusb/

namespace USBDevicesLibrary.Win32API;

public static partial class WinUSBData
{
    public enum RequestType_DirectionFlags : byte
    {
        DataTransferDirectionHostToDevice = 0x00,
        DataTransferDirectionDeviceToHost = 0x80,
    }

    public enum RequestType_TypeFlags : byte
    {
        TypeStandard = 0x00,
        TypeClass = 0x20,
        TypeVendor = 0x40,
        TypeReserved = 0x60,
    }

    public enum RequestType_RecipientFlags : byte
    {
        RecipientDevice = 0x00,
        RecipientInterface = 0x01,
        RecipientEndpoint = 0x02,
        RecipientOther = 0x03,
        RecipientReserved = 0x04, // to 0x1F
    }

    public enum StandardRequestCodes : byte
    {
        GET_STATUS = 0x00,
        CLEAR_FEATURE = 0x01,
        Reserved_0 = 0x02,
        SET_FEATURE = 0x03,
        Reserved_1 = 0x04,
        SET_ADDRESS = 0x05,
        GET_DESCRIPTOR = 0x06,
        SET_DESCRIPTOR = 0x07,
        GET_CONFIGURATION = 0x08,
        SET_CONFIGURATION = 0x09,
        GET_INTERFACE = 0x0A,
        SET_INTERFACE = 0x0B,
        SYNCH_FRAME = 0x0C,
    }

    public enum USBDescriptorTypes : byte
    {
        DEVICE = 0x01,
        CONFIGURATION = 0x02,
        STRING = 0x03,
        INTERFACE = 0x04,
        ENDPOINT = 0x05,
        DEVICE_QUALIFIER = 0x06,
        OTHER_SPEED_CONFIGURATION = 0x07,
        INTERFACE_POWER1 = 0x08,
    }
}
