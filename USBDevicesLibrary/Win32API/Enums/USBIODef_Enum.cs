namespace USBDevicesLibrary.Win32API;

public static partial class USBIODef
{
    public enum USBIODef_Enum
    {
        // Function codes for kernel mode IOCTLs with DeviceType : DEVICE_TYPE_USB (a.k.a DEVICE_TYPE_UNKNOWN)
        // The following codes are valid only if passed as in the icControlCode parameter for IRP_MJ_INTERNAL_DEVICE_CONTROL
        USB_SUBMIT_URB = 0,
        USB_RESET_PORT = 1,
        USB_GET_ROOTHUB_PDO = 3,
        USB_GET_PORT_STATUS = 4,
        USB_ENABLE_PORT = 5,
        USB_GET_HUB_COUNT = 6,
        USB_CYCLE_PORT = 7,
        USB_GET_HUB_NAME = 8,
        USB_IDLE_NOTIFICATION = 9,
        USB_RECORD_FAILURE = 10,
        USB_GET_BUS_INFO = 264,
        USB_GET_CONTROLLER_NAME = 265,
        USB_GET_BUSGUID_INFO = 266,
        USB_GET_PARENT_HUB_INFO = 267,
        USB_GET_DEVICE_HANDLE = 268,
        USB_GET_DEVICE_HANDLE_EX = 269,
        USB_GET_TT_DEVICE_HANDLE = 270,
        USB_GET_TOPOLOGY_ADDRESS = 271,
        USB_IDLE_NOTIFICATION_EX = 272,
        USB_REQ_GLOBAL_SUSPEND = 273,
        USB_REQ_GLOBAL_RESUME = 274,
        USB_GET_HUB_CONFIG_INFO = 275,
        USB_FAIL_GET_STATUS = 280,

        // Function codes for kernel mode IOCTLs with DeviceType : DEVICE_TYPE_USBEX
        // The following codes are valid only if passed as in the icControlCode parameter for IRP_MJ_INTERNAL_DEVICE_CONTROL
        // The range 0 - 2047 is reserved for use by Microsoft. The range 0 - 1023 is used for Public Ioctls defined by Microsoft.
        USB_REGISTER_COMPOSITE_DEVICE = 0,
        USB_UNREGISTER_COMPOSITE_DEVICE = 1,
        USB_REQUEST_REMOTE_WAKE_NOTIFICATION = 2,


        // Function codes for user mode IOCTLs
        // The following codes are valid only if passed as in the icControlCode parameter for IRP_MJ_DEVICE_CONTROL
        // hence, they are callable by user mode applications
        HCD_GET_STATS_1 = 255,
        HCD_DIAGNOSTIC_MODE_ON = 256,
        HCD_DIAGNOSTIC_MODE_OFF = 257,
        HCD_GET_ROOT_HUB_NAME = 258,
        HCD_GET_DRIVERKEY_NAME = 265,
        HCD_GET_STATS_2 = 266,
        HCD_DISABLE_PORT = 268,
        HCD_ENABLE_PORT = 269,
        HCD_USER_REQUEST = 270,
        HCD_TRACE_READ_REQUEST = 275,

        USB_GET_NODE_INFORMATION = 258,
        USB_GET_NODE_CONNECTION_INFORMATION = 259,
        USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION = 260,
        USB_GET_NODE_CONNECTION_NAME = 261,
        USB_DIAG_IGNORE_HUBS_ON = 262,
        USB_DIAG_IGNORE_HUBS_OFF = 263,
        USB_GET_NODE_CONNECTION_DRIVERKEY_NAME = 264,
        USB_GET_HUB_CAPABILITIES = 271,
        USB_GET_NODE_CONNECTION_ATTRIBUTES = 272,
        USB_HUB_CYCLE_PORT = 273,
        USB_GET_NODE_CONNECTION_INFORMATION_EX = 274,
        USB_RESET_HUB = 275,
        USB_GET_HUB_CAPABILITIES_EX = 276,
        USB_GET_HUB_INFORMATION_EX = 277,
        USB_GET_PORT_CONNECTOR_PROPERTIES = 278,
        USB_GET_NODE_CONNECTION_INFORMATION_EX_V2 = 279,
        USB_GET_TRANSPORT_CHARACTERISTICS = 281,
        USB_REGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE = 282,
        USB_NOTIFY_ON_TRANSPORT_CHARACTERISTICS_CHANGE = 283,
        USB_UNREGISTER_FOR_TRANSPORT_CHARACTERISTICS_CHANGE = 284,
        USB_START_TRACKING_FOR_TIME_SYNC = 285,
        USB_GET_FRAME_NUMBER_AND_QPC_FOR_TIME_SYNC = 286,
        USB_STOP_TRACKING_FOR_TIME_SYNC = 287,
        USB_GET_DEVICE_CHARACTERISTICS = 288,
        USB_GET_NODE_CONNECTION_SUPERSPEEDPLUS_INFORMATION = 289,

        // IOCTL codes starting here and beyond are for windows' internal use
        USB_RESERVED_USER_BASE = 1024,
    }
}
