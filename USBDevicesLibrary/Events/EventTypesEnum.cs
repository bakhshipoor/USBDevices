using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Events;

public static class EventTypesEnum
{
    public enum EventTypeEnum : int
    {
        NONE = -1,
        Connected,
        Disconnected,
        Modified,
    }
}
