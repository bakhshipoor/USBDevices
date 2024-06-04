using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.Win32API;
using static USBDevicesLibrary.Win32API.USBIOCtl;

namespace USBDevicesLibrary.USBDevices;

public class USBHub /*: Device*/
{
    public USBHub()
    {
        DeviceID = string.Empty;
        DevicePath = string.Empty;
        HubCharacteristics = new();
    }

    public string DeviceID {  get; set; }
    public string DevicePath { get; set; }

    // Number of downstream facing ports that this hub supports
    public byte NumberOfPorts {  get; set; }
    // Hub Characteristics
    public List<HUB_CHARACHTERISTICS> HubCharacteristics { get; set; }
    // Time (in 2 ms intervals) from the time the power-on sequence begins on a port until power is good on that port.
    // The USB System Software uses this value to determine how long to wait before accessing a powered-on port.
    public byte PowerOnToPowerGood { get; set; }
    // Maximum current requirements of the Hub Controller electronics in mA.
    public byte HubControlCurrent { get; set; }

    public bool HubIsHighSpeedCapable { get; set; }
    public bool HubIsHighSpeed { get; set; }
    public bool HubIsMultiTtCapable { get; set; }
    public bool HubIsMultiTt { get; set; }
    public bool HubIsRoot { get; set; }
    public bool HubIsArmedWakeOnConnect { get; set; }
    public bool HubIsBusPowered { get; set; }

}
