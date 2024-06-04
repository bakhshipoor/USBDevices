using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.USBSpec;

namespace USBDevicesLibrary.USBDevices;

public class USBConfigurationDescriptor
{
    public USBConfigurationDescriptor()
    {
        StringDescriptor_Configuration = string.Empty;
    }

    public USBConfigurationDescriptor(USB_CONFIGURATION_DESCRIPTOR configurationDescriptor) : this()
    {
        NumberOfInterfaces = configurationDescriptor.bNumInterfaces;
        ConfigurationValue = configurationDescriptor.bConfigurationValue;
        IndexOfConfiguration = configurationDescriptor.iConfiguration;
        MaxPower = (ushort)(configurationDescriptor.MaxPower * 2);
        RemoteWakeup = ((configurationDescriptor.bmAttributes & 0x20) != 0) ? true : false;
        SelfPowered = ((configurationDescriptor.bmAttributes & 0x40) != 0) ? true : false;
    }

    // Number of interfaces supported by this configuration
    public byte NumberOfInterfaces { get; set; }
    // Value to use as an argument to the SetConfiguration() request to select this configuration
    public byte ConfigurationValue { get; set; }
    // Index of string descriptor describing this configuration
    public byte IndexOfConfiguration { get; set; }
    // A device configuration that uses power from the bus and a local source reports a non-zero value in MaxPower to indicate the amount of bus power required and sets SelfPowered to true.
    public bool SelfPowered { get; set; }
    // If a device configuration supports remote wakeup, RemoteWakeup is set to true.
    public bool RemoteWakeup { get; set; }
    // Maximum power consumption of the USB device from the bus in this specific configuration when the device is fully operational.
    // Expressed in 2 mA units (i.e., 50 = 100 mA). Note: A device configuration reports whether the configuration is bus-powered or selfpowered.
    // Device status reports whether the device is currently self-powered.
    // If a device is disconnected from its external power source, it updates device status to indicate that it is no longer self-powered.
    // A device may not increase its power draw from the bus, when it loses its external power source, beyond the amount reported by its configuration.
    // If a device can continue to operate when disconnected from its external power source, it continues to do so.
    // If the device cannot continue to operate, it fails operations it can no longer support.The USB System Software may determine
    // the cause of the failure by checking the status and noting the loss of the device’s power source.
    public ushort MaxPower { get; set; } // **  Will multiply with 2 when get configuration descriptor

    public string StringDescriptor_Configuration { get; set; }
}
