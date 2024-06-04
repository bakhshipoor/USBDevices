using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/usb/

namespace USBDevicesLibrary.Win32API;

public static class USB
{
    public enum USBD_PIPE_TYPE
    {
        UsbdPipeTypeControl,
        UsbdPipeTypeIsochronous,
        UsbdPipeTypeBulk,
        UsbdPipeTypeInterrupt
    }

    public enum USBD_GET_CONFIGURATION
    { 
        Unknown=-1,
        Not_Configured=0x00,
        Configured=0x01,
    }

   
}
