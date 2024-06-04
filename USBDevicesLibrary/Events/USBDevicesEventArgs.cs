using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Events.EventTypesEnum;

namespace USBDevicesLibrary.Events;

public class USBDevicesEventArgs : EventArgs
{
    public USBDevicesEventArgs(USBDevice my_USBDevice, EventTypeEnum eventType)
    {
        Device = my_USBDevice;
        EventType = eventType;
    }

    public USBDevice Device { get; set; }
    public EventTypeEnum EventType { get; set; }
}
