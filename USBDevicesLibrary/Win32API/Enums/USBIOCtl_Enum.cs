using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.USBIODef;
using static USBDevicesLibrary.Win32API.WinIOCtlData;

namespace USBDevicesLibrary.Win32API;

public static partial class USBIOCtl
{
    public enum USB_CONNECTION_STATUS : uint
    {
        NoDeviceConnected,
        DeviceConnected,

        /* failure codes, these map to fail reasons */
        DeviceFailedEnumeration,
        DeviceGeneralFailure,
        DeviceCausedOvercurrent,
        DeviceNotEnoughPower,
        DeviceNotEnoughBandwidth,
        DeviceHubNestedTooDeeply,
        DeviceInLegacyHub,
        DeviceEnumerating,
        DeviceReset
    }

    public enum USB_HUB_TYPE
    {
        UsbRootHub = 1,
        Usb20Hub = 2,
        Usb30Hub = 3
    }

    public enum HUB_CHARACHTERISTICS : ushort
    {
        // Logical Power Switching Mode
        GANGED_POWER_SWITCHING, // BIT: 0, Ganged power switching (all ports’ power at once)
        INDIVIDUAL_PORT_POWER_SWITCHING, // BIT: 0, Individual port power switching
        NO_POWER_SWITCHING, // BIT: 1, Reserved. Used only on 1.0 compliant hubs that implement no power switching
        // Identifies a Compound Device
        HUB_IS_NOT_PART_OF_COMPOUND_DEVICE, // BIT: 2, Hub is not part of a compound device.
        HUB_IS_PART_OF_COMPOUND_DEVICE, // BIT: 2, Hub is part of a compound device.
        // Over-current Protection Mode
        GLOBAL_OVER_CURRENT_PROTECTION, // BIT: 3-4, Global Over-current Protection. The hub reports over-current as a summation of all ports’ current draw, without a breakdown of individual port over-current status.
        INDIVIDUAL_PORT_OVER_CURRENT_PROTECTION, // BIT: 3-4 , Individual Port Over-current Protection. The hub reports over-current on a per-port basis. Each port has an over-current status.
        NO_OVER_CURRENT_PROTECTION, // BIT: 3-4, No Over-current Protection. This option is allowed only for bus-powered hubs that do not implement over-current protection.
        // TT Think TIme
        TT_REQUIRES_AT_MOST_8_FS_BIT_TIMES, // BIT: 5-6, TT requires at most 8 FS bit times of inter transaction gap on a full-/low-speed downstream bus.
        TT_REQUIRES_AT_MOST_16_FS_BIT_TIMES, // BIT: 5-6, TT requires at most 16 FS bit times.
        TT_REQUIRES_AT_MOST_24_FS_BIT_TIMES, // BIT: 5-6, TT requires at most 24 FS bit times.
        TT_REQUIRES_AT_MOST_32_FS_BIT_TIMES, // BIT: 5-6, TT requires at most 32 FS bit times.
        // Port Indicators Supported
        PORT_INDICATORS_ARE_NOT_SUPPORTED, // BIT: 7, Port Indicators are not supported on its downstream facing ports and the PORT_INDICATOR request has no effect.
        PORT_INDICATORS_ARE_SUPPORTED, // BIT: 7, Port Indicators are supported on its downstream facing ports and the PORT_INDICATOR request controls the indicators.
        // RESERVED BIT: 8-15
    }

    public enum HUB_CHARACHTERISTICS_Mask : ushort
    {
        // Logical Power Switching Mode
        LOGICAL_POWER_SWITCHING_MODE = 0x03,
        // Identifies a Compound Device
        IDENTIFIES_COMPOUND_DEVICE = 0x04,
        // Over-current Protection Mode
        OVER_CURRENT_PROTECTION_MODE = 0x18,
        // TT Think Time
        TT_THINK_TIME = 0x60,
        // Port Indicators Supported
        PORT_INDICATORS_SUPPORTED = 0x80
        // RESERVED = 0x00 <<8, D15...D8
    }

    [Flags]
    public enum USB_HUB_CAP_FLAGS : uint
    {
        HubIsHighSpeedCapable = 1<<0,
        HubIsHighSpeed = 1<<1,
        HubIsMultiTtCapable = 1<<2,
        HubIsMultiTt = 1<<3,
        HubIsRoot = 1<<4,
        HubIsArmedWakeOnConnect = 1<<5,
        HubIsBusPowered = 1<<6,
        //ReservedMBZ = 1<<7,
    }

    public enum USB_DEVICE_SPEED : byte
    {
        UsbLowSpeed = 0,
        UsbFullSpeed,
        UsbHighSpeed,
        UsbSuperSpeed
    }

    [Flags]
    public enum USB_PORTATTR : uint
    {
        USB_PORTATTR_NO_CONNECTOR = 0x00000001,
        USB_PORTATTR_SHARED_USB2 = 0x00000002,
        USB_PORTATTR_MINI_CONNECTOR = 0x00000004,
        USB_PORTATTR_OEM_CONNECTOR = 0x00000008,
        USB_PORTATTR_OWNED_BY_CC = 0x01000000,
        USB_PORTATTR_NO_OVERCURRENT_UI = 0x02000000,
    }

    [Flags]
    public enum USB_PROTOCOLS : uint
    {
        Usb110 = 1 << 0,
        Usb200 = 1 << 1,
        Usb300 = 1 << 2,
    }

    [Flags]
    public enum USB_NODE_CONNECTION_INFORMATION_EX_V2_FLAGS : uint
    {
        DeviceIsOperatingAtSuperSpeedOrHigher = 1 << 0,
        DeviceIsSuperSpeedCapableOrHigher = 1 << 1,
        DeviceIsOperatingAtSuperSpeedPlusOrHigher = 1 << 2,
        DeviceIsSuperSpeedPlusCapableOrHigher = 1 << 3,
    }

    public enum USB_IOCTL_Enum
    {
        // The IOCTL_INTERNAL_USB_CYCLE_PORT I/O request simulates a device unplug and replug on the port associated with the PDO.
        // IOCTL_INTERNAL_USB_CYCLE_PORT is a kernel-mode I/O control request. This request targets the USB hub PDO. This request must be sent at an IRQL of PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_CYCLE_PORT,
        // The IOCTL_INTERNAL_USB_ENABLE_PORT IOCTL has been deprecated. Do not use.
        IOCTL_INTERNAL_USB_ENABLE_PORT,
        // 
        IOCTL_INTERNAL_USB_FAIL_GET_STATUS_FROM_DEVICE,
        // The IOCTL_INTERNAL_USB_GET_BUS_INFO I/O request queries the bus driver for certain bus information.
        // OCTL_INTERNAL_USB_GET_BUS_INFO is a kernel-mode I/O control request. This request targets the USB hub PDO. This request must be sent at an IRQL of PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_GET_BUS_INFO,
        // The IOCTL_INTERNAL_USB_GET_BUSGUID_INFO IOCTL has been deprecated. Do not use.
        IOCTL_INTERNAL_USB_GET_BUSGUID_INFO,
        // The IOCTL_INTERNAL_USB_GET_CONTROLLER_NAME I/O request queries the bus driver for the device name of the USB host controller.
        // IOCTL_INTERNAL_USB_GET_CONTROLLER_NAME is a kernel-mode I/O control request. This request targets the USB hub PDO. This request must be sent at an IRQL of PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_GET_CONTROLLER_NAME,
        // The IOCTL_INTERNAL_USB_GET_DEVICE_CONFIG_INFO I/O request returns information about a USB device and the hub it is attached to.
        // IOCTL_INTERNAL_USB_GET_DEVICE_CONFIG_INFO is a kernel-mode I/O control request. This request targets the USB hub PDO. This request must be sent at an IRQL of DISPATCH_LEVEL or lower.
        IOCTL_INTERNAL_USB_GET_DEVICE_CONFIG_INFO,
        // The IOCTL_INTERNAL_USB_GET_DEVICE_HANDLE IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_GET_DEVICE_HANDLE,
        // The IOCTL_INTERNAL_USB_GET_DEVICE_HANDLE_EX IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_GET_DEVICE_HANDLE_EX,
        // The IOCTL_INTERNAL_USB_GET_HUB_COUNT IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_GET_HUB_COUNT,
        // The IOCTL_INTERNAL_USB_GET_HUB_NAME I/O request is used by drivers to retrieve the UNICODE symbolic name for the target PDO if the PDO is for a hub. Otherwise a NULL string is returned.
        // IOCTL_INTERNAL_USB_GET_HUB_NAME is a kernel-mode I/O control request. This request targets the USB hub PDO. This request must be sent at an IRQL of PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_GET_HUB_NAME,
        // The IOCTL_INTERNAL_USB_GET_PARENT_HUB_INFO is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_GET_PARENT_HUB_INFO,
        // The IOCTL_INTERNAL_USB_GET_PORT_STATUS I/O request queries the status of the PDO.
        // IOCTL_INTERNAL_USB_GET_PORT_STATUS is a kernel-mode I/O control request. This request targets the USB hub PDO. This IOCTL must be sent at IRQL = PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_GET_PORT_STATUS,
        // The IOCTL_INTERNAL_USB_GET_ROOTHUB_PDO IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_GET_ROOTHUB_PDO,
        // The IOCTL_INTERNAL_USB_GET_TOPOLOGY_ADDRESS I/O request returns information about the host controller the USB device is attached to, and the device's location in the USB device tree.
        // IOCTL_INTERNAL_USB_GET_TOPOLOGY_ADDRESS is a kernel-mode I/O control request. This request targets the USB hub PDO. This request must be sent at an IRQL of DISPATCH_LEVEL or lower.
        IOCTL_INTERNAL_USB_GET_TOPOLOGY_ADDRESS,
        // The IOCTL_INTERNAL_USB_GET_TT_DEVICE_HANDLE is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_GET_TT_DEVICE_HANDLE,
        // The IOCTL_INTERNAL_USB_NOTIFY_IDLE_READY IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_NOTIFY_IDLE_READY,
        // The IOCTL_INTERNAL_USB_RECORD_FAILURE IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_RECORD_FAILURE,
        // The IOCTL_INTERNAL_USB_REGISTER_COMPOSITE_DEVICE I/O request registers the driver of a USB multi-function device (composite driver) with the underlying USB driver stack.
        // IOCTL_INTERNAL_USB_REGISTER_COMPOSITE_DEVICE is a kernel-mode I/O control request. This request targets the USB hub physical device object (PDO). This request must be sent at an interrupt request level (IRQL) of PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_REGISTER_COMPOSITE_DEVICE,
        // The IOCTL_INTERNAL_USB_REQ_GLOBAL_RESUME IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_REQ_GLOBAL_RESUME,
        // The IOCTL_INTERNAL_USB_REQ_GLOBAL_SUSPEND IOCTL is used by the USB hub driver. Do not use.
        IOCTL_INTERNAL_USB_REQ_GLOBAL_SUSPEND,
        // The IOCTL_INTERNAL_USB_REQUEST_REMOTE_WAKE_NOTIFICATION I/O request is sent by the driver of a Universal Serial Bus (USB) multi-function device (composite driver) to request remote wake-up notifications from a specific function in the device.
        // IOCTL_INTERNAL_USB_REQUEST_REMOTE_WAKE_NOTIFICATION is a kernel-mode I/O control request. This request targets the USB hub physical device object (PDO).
        IOCTL_INTERNAL_USB_REQUEST_REMOTE_WAKE_NOTIFICATION,
        // The IOCTL_INTERNAL_USB_RESET_PORT I/O control request is used by a driver to reset the upstream port of the device it manages. After a successful reset, the bus driver reselects the configuration and any alternative interface settings that the device had before the reset occurred. All pipe handles, configuration handles and interface handles remain valid.
        // IOCTL_INTERNAL_USB_RESET_PORT is a kernel-mode I/O control request. This request targets the USB hub PDO.
        IOCTL_INTERNAL_USB_RESET_PORT,
        // The IOCTL_INTERNAL_USB_SUBMIT_IDLE_NOTIFICATION I/O request is used by drivers to inform the USB bus driver that a device is idle and can be suspended.
        // IOCTL_INTERNAL_USB_SUBMIT_IDLE_NOTIFICATION is a kernel-mode I/O control request. This request targets the USB hub PDO. This request must be sent at an IRQL of PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_SUBMIT_IDLE_NOTIFICATION,
        // The IOCTL_INTERNAL_USB_SUBMIT_URB I/O control request is used by drivers to submit an URB to the bus driver.
        // IOCTL_INTERNAL_USB_SUBMIT_URB is a kernel-mode I/O control request. This request targets the USB hub PDO.
        IOCTL_INTERNAL_USB_SUBMIT_URB,
        // The IOCTL_INTERNAL_USB_UNREGISTER_COMPOSITE_DEVICE I/O request unregisters the driver of a USB multi-function device (composite driver) and releases all resources that are associated with registration. The request is successful only if the composite driver was previously registered with the underlying USB driver stack through the IOCTL_INTERNAL_USB_REGISTER_COMPOSITE_DEVICE request.
        // IOCTL_INTERNAL_USB_UNREGISTER_COMPOSITE_DEVICE is a kernel-mode I/O control request. This request targets the Universal Serial Bus (USB) hub physical device object (PDO). This request must be sent at an interrupt request level (IRQL) of PASSIVE_LEVEL.
        IOCTL_INTERNAL_USB_UNREGISTER_COMPOSITE_DEVICE,
        // The IOCTL_GET_HCD_DRIVERKEY_NAME I/O control request retrieves the driver key name in the registry for a USB host controller driver.
        // This request targets the USB host controller (GUID_DEVINTERFACE_USB_HOST_CONTROLLER).
        IOCTL_GET_HCD_DRIVERKEY_NAME,
        // The IOCTL_USB_HCD_DISABLE_PORT IOCTL has been deprecated. Do not use.
        IOCTL_USB_HCD_DISABLE_PORT,
        // The IOCTL_USB_HCD_ENABLE_PORT IOCTL has been deprecated. Do not use.
        IOCTL_USB_HCD_ENABLE_PORT,
        // The IOCTL_USB_HCD_GET_STATS_1 IOCTL has been deprecated. Do not use.
        IOCTL_USB_HCD_GET_STATS_1,
        // The IOCTL_USB_HCD_GET_STATS_2 IOCTL has been deprecated. Do not use.
        IOCTL_USB_HCD_GET_STATS_2,
        // The IOCTL_USB_DIAG_IGNORE_HUBS_OFF I/O control has been deprecated. Do not use.
        IOCTL_USB_DIAG_IGNORE_HUBS_OFF,
        // The IOCTL_USB_DIAG_IGNORE_HUBS_ON I/O control has been deprecated. Do not use.
        IOCTL_USB_DIAG_IGNORE_HUBS_ON,
        // The IOCTL_USB_DIAGNOSTIC_MODE_OFF I/O control has been deprecated. Do not use.
        IOCTL_USB_DIAGNOSTIC_MODE_OFF,
        // The IOCTL_USB_DIAGNOSTIC_MODE_ON I/O control has been deprecated. Do not use.
        IOCTL_USB_DIAGNOSTIC_MODE_ON,
        // The client driver sends this request to determine general characteristics about a USB device, such as maximum send and receive delays for any request.
        IOCTL_USB_GET_DEVICE_CHARACTERISTICS,
        // The IOCTL_USB_GET_PORT_CONNECTOR_PROPERTIES I/O control request is sent by an application to retrieve information about a specific port on a USB hub.
        // IOCTL_USB_GET_PORT_CONNECTOR_PROPERTIES is a user-mode I/O control request. This request targets the Universal Serial Bus (USB) hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_PORT_CONNECTOR_PROPERTIES,
        // The IOCTL_USB_GET_HUB_CAPABILITIES I/O control request retrieves the capabilities of a USB hub. Note  This request is replaced by IOCTL_USB_GET_HUB_CAPABILITIES_EX in Windows Vista.
        // IOCTL_USB_GET_HUB_CAPABILITIES is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_HUB_CAPABILITIES,
        // The IOCTL_USB_GET_HUB_CAPABILITIES_EX I/O control request retrieves the capabilities of a USB hub.
        // IOCTL_USB_GET_HUB_CAPABILITIES_EX is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_HUB_CAPABILITIES_EX,
        // The IOCTL_USB_GET_HUB_INFORMATION_EX I/O control request is sent by an application to retrieve information about a USB hub in a USB_HUB_INFORMATION_EX structure.
        // IOCTL_USB_GET_HUB_INFORMATION_EX is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_HUB_INFORMATION_EX,
        // The IOCTL_USB_HUB_CYCLE_PORT I/O control request power-cycles the port that is associated with the PDO that receives the request.
        // IOCTL_USB_HUB_CYCLE_PORT is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_HUB_CYCLE_PORT,
        // The IOCTL_USB_RESET_HUB IOCTL is used by the USB driver stack. Do not use.
        IOCTL_USB_RESET_HUB,
        // The IOCTL_USB_GET_ROOT_HUB_NAME I/O control request is used with the USB_ROOT_HUB_NAME structure to retrieve the symbolic link name of the root hub.
        // IOCTL_USB_GET_ROOT_HUB_NAME is a user-mode I/O control request. This request targets the USB host controller (GUID_DEVINTERFACE_USB_HOST_CONTROLLER).
        IOCTL_USB_GET_ROOT_HUB_NAME,
        // The IOCTL_USB_GET_NODE_INFORMATION I/O control request is used with the USB_NODE_INFORMATION structure to retrieve information about a parent device.
        // IOCTL_USB_GET_NODE_INFORMATION is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_INFORMATION,
        // The IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION I/O control request retrieves one or more descriptors for the device that is associated with the indicated port index.
        // IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION,
        // The IOCTL_USB_GET_NODE_CONNECTION_ATTRIBUTES I/O control request retrieves the Microsoft-extended port attributes for a specific port.
        // IOCTL_USB_GET_NODE_CONNECTION_ATTRIBUTES is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_CONNECTION_ATTRIBUTES,
        // The IOCTL_USB_GET_NODE_CONNECTION_DRIVERKEY_NAME I/O control request retrieves the driver registry key name that is associated with the device that is connected to the indicated port.
        // IOCTL_USB_GET_NODE_CONNECTION_DRIVERKEY_NAME is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_CONNECTION_DRIVERKEY_NAME,
        // The IOCTL_USB_GET_NODE_CONNECTION_INFORMATION request retrieves information about the indicated USB port and the device that is attached to the port, if there is one.
        // IOCTL_USB_GET_NODE_CONNECTION_INFORMATION is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_CONNECTION_INFORMATION,
        // The IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX request retrieves information about a USB port and the device that is attached to the port, if there is one.
        // IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX,
        // The IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX_V2 I/O control is sent by an application to retrieve information about the protocols that are supported by a particular USB port on a hub. The request also retrieves the speed capability of the port.
        // IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX_V2 is a user-mode I/O control request. This request targets the Universal Serial Bus (USB) hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX_V2,
        // The IOCTL_USB_GET_NODE_CONNECTION_NAME I/O control request is used with the USB_NODE_CONNECTION_NAME structure to retrieve the symbolic link name of the hub that is attached to the downstream port.
        // IOCTL_USB_GET_NODE_CONNECTION_NAME is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_CONNECTION_NAME,
        // The IOCTL_USB_GET_NODE_CONNECTION_SUPERSPEEDPLUS_INFORMATION request retrieves USB port super-speed lane information.
        // IOCTL_USB_GET_NODE_CONNECTION_SUPERSPEEDPLUS_INFORMATION is a user-mode I/O control request. This request targets the USB hub device (GUID_DEVINTERFACE_USB_HUB).
        IOCTL_USB_GET_NODE_CONNECTION_SUPERSPEEDPLUS_INFORMATION,
        // Retrieves the system query performance counter (QPC) value synchronized with the frame and microframe.
        IOCTL_USB_GET_FRAME_NUMBER_AND_QPC_FOR_TIME_SYNC,
        // The client driver sends this request to retrieve the transport characteristics.
        IOCTL_USB_GET_TRANSPORT_CHARACTERISTICS,
        // This request notifies the caller of change in transport characteristics.
        IOCTL_USB_NOTIFY_ON_TRANSPORT_CHARACTERISTICS_CHANGE,
        // This request registers for notifications about the changes in transport characteristics.
        IOCTL_USB_REGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE,
        // This request unregisters the caller from getting notifications about transport characteristics changes.
        IOCTL_USB_UNREGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE,
        // This request registers the caller with USB driver stack for time sync services.
        IOCTL_USB_START_TRACKING_FOR_TIME_SYNC,
        // This request unregisters the caller with USB driver stack for time sync services.
        IOCTL_USB_STOP_TRACKING_FOR_TIME_SYNC,
        
    }

    public static readonly Dictionary<USB_IOCTL_Enum, uint> USB_IOCTL = new Dictionary<USB_IOCTL_Enum, uint>()
    {
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_SUBMIT_URB,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_SUBMIT_URB, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_RESET_PORT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_RESET_PORT, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_ROOTHUB_PDO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_ROOTHUB_PDO, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_PORT_STATUS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_PORT_STATUS, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_ENABLE_PORT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_ENABLE_PORT, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_HUB_COUNT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_HUB_COUNT, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_CYCLE_PORT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_CYCLE_PORT, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_HUB_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_HUB_NAME, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_BUS_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_BUS_INFO, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_CONTROLLER_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_CONTROLLER_NAME, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_BUSGUID_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_BUSGUID_INFO, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_PARENT_HUB_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_PARENT_HUB_INFO, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_SUBMIT_IDLE_NOTIFICATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_IDLE_NOTIFICATION, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_DEVICE_HANDLE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_DEVICE_HANDLE, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_NOTIFY_IDLE_READY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_IDLE_NOTIFICATION_EX, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_REQ_GLOBAL_SUSPEND,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_REQ_GLOBAL_SUSPEND, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_REQ_GLOBAL_RESUME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_REQ_GLOBAL_RESUME, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_RECORD_FAILURE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_RECORD_FAILURE, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_DEVICE_HANDLE_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_DEVICE_HANDLE_EX, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_TT_DEVICE_HANDLE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_TT_DEVICE_HANDLE, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_TOPOLOGY_ADDRESS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_TOPOLOGY_ADDRESS, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_GET_DEVICE_CONFIG_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_HUB_CONFIG_INFO, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_REGISTER_COMPOSITE_DEVICE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USBEX, (uint)USBIODef_Enum.USB_REGISTER_COMPOSITE_DEVICE, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_UNREGISTER_COMPOSITE_DEVICE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USBEX, (uint)USBIODef_Enum.USB_UNREGISTER_COMPOSITE_DEVICE, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_REQUEST_REMOTE_WAKE_NOTIFICATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USBEX, (uint)USBIODef_Enum.USB_REQUEST_REMOTE_WAKE_NOTIFICATION, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_INTERNAL_USB_FAIL_GET_STATUS_FROM_DEVICE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_FAIL_GET_STATUS, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_HCD_GET_STATS_1,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_GET_STATS_1, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_HCD_GET_STATS_2,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_GET_STATS_2, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_HCD_DISABLE_PORT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_DISABLE_PORT, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_HCD_ENABLE_PORT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_ENABLE_PORT, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_DIAGNOSTIC_MODE_ON,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_DIAGNOSTIC_MODE_ON, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_DIAGNOSTIC_MODE_OFF,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_DIAGNOSTIC_MODE_OFF, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_ROOT_HUB_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_GET_ROOT_HUB_NAME, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_GET_HCD_DRIVERKEY_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.HCD_GET_DRIVERKEY_NAME, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_INFORMATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_INFORMATION, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_INFORMATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_CONNECTION_INFORMATION, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_CONNECTION_NAME, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_DIAG_IGNORE_HUBS_ON,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_DIAG_IGNORE_HUBS_ON, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_DIAG_IGNORE_HUBS_OFF,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_DIAG_IGNORE_HUBS_OFF, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_DRIVERKEY_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_CONNECTION_DRIVERKEY_NAME, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_HUB_CAPABILITIES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_HUB_CAPABILITIES, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_HUB_CYCLE_PORT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_HUB_CYCLE_PORT, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_CONNECTION_ATTRIBUTES, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_CONNECTION_INFORMATION_EX, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_RESET_HUB,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_RESET_HUB, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_HUB_CAPABILITIES_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_HUB_CAPABILITIES_EX, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_HUB_INFORMATION_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_HUB_INFORMATION_EX, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        { USB_IOCTL_Enum.IOCTL_USB_GET_PORT_CONNECTOR_PROPERTIES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_PORT_CONNECTOR_PROPERTIES, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX_V2,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_CONNECTION_INFORMATION_EX_V2, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_TRANSPORT_CHARACTERISTICS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_TRANSPORT_CHARACTERISTICS, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        { 
            USB_IOCTL_Enum.IOCTL_USB_REGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_REGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_NOTIFY_ON_TRANSPORT_CHARACTERISTICS_CHANGE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_NOTIFY_ON_TRANSPORT_CHARACTERISTICS_CHANGE, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_UNREGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_UNREGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_START_TRACKING_FOR_TIME_SYNC,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_START_TRACKING_FOR_TIME_SYNC, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_FRAME_NUMBER_AND_QPC_FOR_TIME_SYNC,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_FRAME_NUMBER_AND_QPC_FOR_TIME_SYNC, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_STOP_TRACKING_FOR_TIME_SYNC,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_STOP_TRACKING_FOR_TIME_SYNC, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_DEVICE_CHARACTERISTICS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_DEVICE_CHARACTERISTICS, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },
        {
            USB_IOCTL_Enum.IOCTL_USB_GET_NODE_CONNECTION_SUPERSPEEDPLUS_INFORMATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.FILE_DEVICE_USB, (uint)USBIODef_Enum.USB_GET_NODE_CONNECTION_SUPERSPEEDPLUS_INFORMATION, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS )
        },

    };
}
