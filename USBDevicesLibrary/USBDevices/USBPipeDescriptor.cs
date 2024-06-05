using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.USB;
using static USBDevicesLibrary.Win32API.USBSpec;
using static USBDevicesLibrary.Win32API.WinUSBIO;

namespace USBDevicesLibrary.USBDevices;

public class USBPipeDescriptor
{
    public USBPipeDescriptor()
    {
        PipePolicies = new();
    }

    public USBPipeDescriptor(WINUSB_PIPE_INFORMATION pipeInformation) : this()
    {
        PipeType = pipeInformation.PipeType;
        PipeId = pipeInformation.PipeId;
        MaximumPacketSize = pipeInformation.MaximumPacketSize;
        Interval = pipeInformation.Interval;
        Direction = ((pipeInformation.PipeId & (byte)USB_ENDPOINT_DIRECTION_MASK)!=0) ? USBEndpointDirection.DIRECTION_IN : USBEndpointDirection.DIRECTION_OUT  ;
    }

    public USBD_PIPE_TYPE PipeType { get; set; }
    public byte PipeId { get; set; }
    public ushort MaximumPacketSize { get; set; }
    public byte Interval { get; set; }
    public USBEndpointDirection Direction { get; set; }

    // Extra
    public USBPipePolicies PipePolicies { get; set; }
    public bool HaltStatus {  get; set; }
}
