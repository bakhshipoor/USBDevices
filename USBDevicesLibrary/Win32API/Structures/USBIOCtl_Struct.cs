using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static USBDevicesLibrary.Win32API.USBSpec;
using static USBDevicesLibrary.Win32API.WinUSBData;

namespace USBDevicesLibrary.Win32API;

public static partial class USBIOCtl
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_NODE_CONNECTION_INFORMATION_EX
    {
        public uint ConnectionIndex; // INPUT
        // usb device descriptor returned by this device during enumeration 
        public USB_DEVICE_DESCRIPTOR DeviceDescriptor; // OUTPUT 
        public byte CurrentConfigurationValue; // OUTPUT 
        public USB_DEVICE_SPEED Speed; // OUTPUT 
        public byte DeviceIsHub; // OUTPUT 
        public ushort DeviceAddress;  // OUTPUT 
        public uint NumberOfOpenPipes; // OUTPUT 
        public USB_CONNECTION_STATUS ConnectionStatus; // OUTPUT 
        //public USB_PIPE_INFO PipeList[0]; // OUTPUT 
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct USB_NODE_INFORMATION
    {
        public USB_HUB_NODE NodeType;
        public UsbNodeUnion u;
    }

    public enum USB_HUB_NODE : uint
    {
        UsbHub,
        UsbMIParent
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct UsbNodeUnion
    {
        [FieldOffset(0)]
        public USB_HUB_INFORMATION HubInformation;
        [FieldOffset(0)]
        public USB_MI_PARENT_INFORMATION MiParentInformation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct USB_HUB_INFORMATION
    {
        public USB_HUB_DESCRIPTOR HubDescriptor;
        public byte HubIsBusPowered;
    }

    public struct USB_HUB_INFORMATION_EX
    {
        public USB_HUB_TYPE HubType;

        // The higest valid port number on the hub
        public ushort HighestPortNumber;
        public USB_HUB_DESCRIPTOR_Union Descriptor;
    }
    
    [StructLayout(LayoutKind.Explicit)]
    public struct USB_HUB_DESCRIPTOR_Union
    {
        [FieldOffset(0)]
        public USB_HUB_DESCRIPTOR UsbHubDescriptor;
        [FieldOffset(0)]
        public USB_30_HUB_DESCRIPTOR Usb30HubDescriptor;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_HUB_DESCRIPTOR
    {
        public byte bDescriptorLength;
        public byte bDescriptorType;
        public byte bNumberOfPorts;
        public ushort wHubCharacteristics;
        public byte bPowerOnToPowerGood;
        public byte bHubControlCurrent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] bRemoveAndPowerMask;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_30_HUB_DESCRIPTOR
    {
        public byte bLength;
        public byte bDescriptorType;
        public byte bNumberOfPorts;
        public ushort wHubCharacteristics;
        public byte bPowerOnToPowerGood;
        public byte bHubControlCurrent;
        public byte bHubHdrDecLat;
        public ushort wHubDelay;
        public ushort DeviceRemovable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct USB_MI_PARENT_INFORMATION
    {
        public UInt32 NumberOfInterfaces;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_TOPOLOGY_ADDRESS
    {
        public ulong PciBusNumber;
        public ulong PciDeviceNumber;
        public ulong PciFunctionNumber;
        public ulong Reserved;
        public ushort RootHubPortNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public ushort[] HubPortNumber;
        public ushort Reserved2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HUB_DEVICE_CONFIG_INFO
    {
        public HUB_DEVICE_CONFIG_INFO()
        {
            HardwareIds = new();
            CompatibleIds = new();
            DeviceDescription = new();
            Reserved = new ulong[19];
            UxdSettings = new();
        }
        public ulong Version;
        public ulong Length;
        public ulong HubFlags;
        public USB_ID_STRING HardwareIds;
        public USB_ID_STRING CompatibleIds;
        public USB_ID_STRING DeviceDescription;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
        public ulong[] Reserved;
        public USB_HUB_DEVICE_UXD_SETTINGS UxdSettings;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_ID_STRING
    {
        public USB_ID_STRING()
        {
            LengthInBytes = 256;
            Buffer = string.Empty;
        }
        public ushort LanguageId;      // laguage id where apllicable
        public ushort Pad;
        public ulong LengthInBytes;    // length of <Buffer> in Bytes includes NULLs etc
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Buffer;

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_HUB_DEVICE_UXD_SETTINGS
    {
        public USB_HUB_DEVICE_UXD_SETTINGS()
        {
            Reserved = new ulong[5];
        }
        public ulong Version;
        public Guid PnpGuid;
        public Guid OwnerGuid;
        public ulong DeleteOnShutdown;
        public ulong DeleteOnReload;
        public ulong DeleteOnDisconnect;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public ulong[] Reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_NODE_CONNECTION_ATTRIBUTES
    {
        // one based port number 
        public uint ConnectionIndex;  // INPUT 
        // current USB connect status for the port
        public USB_CONNECTION_STATUS ConnectionStatus; // OUTPUT 
        public USB_PORTATTR PortAttributes; // OUTPUT 
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct USB_NODE_CONNECTION_NAME
    {
        public uint ConnectionIndex;
        public uint ActualLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string NodeName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct USB_NODE_CONNECTION_DRIVERKEY_NAME
    {
        public uint ConnectionIndex;
        public uint ActualLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string DriverKeyName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct USB_PORT_CONNECTOR_PROPERTIES
    {
        public uint ConnectionIndex;
        public uint ActualLength;
        public uint UsbPortProperties;
        public ushort CompanionIndex;
        public ushort CompanionPortNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string CompanionHubSymbolicLinkName;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct USB_PORT_PROPERTIES
    {
        [FieldOffset(0)]
        public ulong ul;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
    public struct USB_DESCRIPTOR_REQUEST
    {
        public uint ConnectionIndex;
        public WINUSB_SETUP_PACKET SetupPacket;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
        public byte[] Data;

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct USB_ROOT_HUB_NAME
    {
        public uint ActualLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string RootHubName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct USB_CYCLE_PORT_PARAMS
    {
        public uint ConnectionIndex;
        public uint StatusReturned;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_HUB_CAPABILITIES_EX
    {
        public uint CapabilityFlags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_DEVICE_CHARACTERISTICS
    {
        public ulong Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ulong[] Reserved;
        public ulong UsbDeviceCharacteristicsFlags;
        public ulong MaximumSendPathDelayInMilliSeconds;
        public ulong MaximumCompletionPathDelayInMilliSeconds;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct USB_NODE_CONNECTION_INFORMATION_EX_V2
    {
        public uint ConnectionIndex;
        public uint Length;
        public USB_PROTOCOLS SupportedUsbProtocols;
        public USB_NODE_CONNECTION_INFORMATION_EX_V2_FLAGS Flags;
    }
}
