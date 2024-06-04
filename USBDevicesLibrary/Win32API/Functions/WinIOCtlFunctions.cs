using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static USBDevicesLibrary.Win32API.WinIOCtlData;

namespace USBDevicesLibrary.Win32API;

public static class WinIOCtlFunctions
{
    public static uint CTL_CODE(DEVICE_TYPES deviceType, uint function, METHOD_CODES method, FILE_ACCESS access)
    {
        return (uint)deviceType << 16 | (uint)access << 14 | (uint)function << 2 | (uint)method;
    }
}
