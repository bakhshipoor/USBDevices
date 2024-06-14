using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Win32API;

public static partial class CfgMgrData
{

    // Configuration Manager CONFIGRET return status codes
    public enum CONFIGRET : uint
    {
        CR_SUCCESS = 0x00000000,
        CR_DEFAULT = 0x00000001,
        CR_OUT_OF_MEMORY = 0x00000002,
        CR_INVALID_POINTER = 0x00000003,
        CR_INVALID_FLAG = 0x00000004,
        CR_INVALID_DEVNODE = 0x00000005,
        CR_INVALID_DEVINST = CR_INVALID_DEVNODE,   
        CR_INVALID_RES_DES = 0x00000006,
        CR_INVALID_LOG_CONF = 0x00000007,
        CR_INVALID_ARBITRATOR = 0x00000008,
        CR_INVALID_NODELIST = 0x00000009,
        CR_DEVNODE_HAS_REQS = 0x0000000A,
        CR_DEVINST_HAS_REQS = CR_DEVNODE_HAS_REQS,   
        CR_INVALID_RESOURCEID = 0x0000000B,
        CR_DLVXD_NOT_FOUND = 0x0000000C,   // WIN 95 ONLY
        CR_NO_SUCH_DEVNODE = 0x0000000D,
        CR_NO_SUCH_DEVINST = CR_NO_SUCH_DEVNODE,   
        CR_NO_MORE_LOG_CONF = 0x0000000E,
        CR_NO_MORE_RES_DES = 0x0000000F,
        CR_ALREADY_SUCH_DEVNODE = 0x00000010,
        CR_ALREADY_SUCH_DEVINST = CR_ALREADY_SUCH_DEVNODE,   
        CR_INVALID_RANGE_LIST = 0x00000011,
        CR_INVALID_RANGE = 0x00000012,
        CR_FAILURE = 0x00000013,
        CR_NO_SUCH_LOGICAL_DEV = 0x00000014,
        CR_CREATE_BLOCKED = 0x00000015,
        CR_NOT_SYSTEM_VM = 0x00000016,   // WIN 95 ONLY
        CR_REMOVE_VETOED = 0x00000017,
        CR_APM_VETOED = 0x00000018,
        CR_INVALID_LOAD_TYPE = 0x00000019,
        CR_BUFFER_SMALL = 0x0000001A,
        CR_NO_ARBITRATOR = 0x0000001B,
        CR_NO_REGISTRY_HANDLE = 0x0000001C,
        CR_REGISTRY_ERROR = 0x0000001D,
        CR_INVALID_DEVICE_ID = 0x0000001E,
        CR_INVALID_DATA = 0x0000001F,
        CR_INVALID_API = 0x00000020,
        CR_DEVLOADER_NOT_READY = 0x00000021,
        CR_NEED_RESTART = 0x00000022,
        CR_NO_MORE_HW_PROFILES = 0x00000023,
        CR_DEVICE_NOT_THERE = 0x00000024,
        CR_NO_SUCH_VALUE = 0x00000025,
        CR_WRONG_TYPE = 0x00000026,
        CR_INVALID_PRIORITY = 0x00000027,
        CR_NOT_DISABLEABLE = 0x00000028,
        CR_FREE_RESOURCES = 0x00000029,
        CR_QUERY_VETOED = 0x0000002A,
        CR_CANT_SHARE_IRQ = 0x0000002B,
        CR_NO_DEPENDENT = 0x0000002C,
        CR_SAME_RESOURCES = 0x0000002D,
        CR_NO_SUCH_REGISTRY_KEY = 0x0000002E,
        CR_INVALID_MACHINENAME = 0x0000002F,   // NT ONLY
        CR_REMOTE_COMM_FAILURE = 0x00000030,   // NT ONLY
        CR_MACHINE_UNAVAILABLE = 0x00000031,   // NT ONLY
        CR_NO_CM_SERVICES = 0x00000032,   // NT ONLY
        CR_ACCESS_DENIED = 0x00000033,   // NT ONLY
        CR_CALL_NOT_IMPLEMENTED = 0x00000034,
        CR_INVALID_PROPERTY = 0x00000035,
        CR_DEVICE_INTERFACE_ACTIVE = 0x00000036,
        CR_NO_SUCH_DEVICE_INTERFACE = 0x00000037,
        CR_INVALID_REFERENCE_STRING = 0x00000038,
        CR_INVALID_CONFLICT_LIST = 0x00000039,
        CR_INVALID_INDEX = 0x0000003A,
        CR_INVALID_STRUCTURE_SIZE = 0x0000003B,
    }

    // If the PnP manager rejects a request to perform an operation, the PNP_VETO_TYPE enumeration is used to identify the reason for the rejection.
    public enum PNP_VETO_TYPE : uint
    {
        // The specified operation was rejected for an unknown reason.
        // Veto Text String: None.
        PNP_VetoTypeUnknown,
        // The device does not support the specified PnP operation.
        // Veto Text String: A device instance path.
        PNP_VetoLegacyDevice,
        // The specified operation cannot be completed because of a pending close operation.
        // Veto Text String: A device instance path.
        PNP_VetoPendingClose,
        // A Microsoft Win32 application vetoed the specified operation.
        // Veto Text String: An application module name.
        PNP_VetoWindowsApp,
        // A Win32 service vetoed the specified operation.
        // Veto Text String: A Windows service name.
        PNP_VetoWindowsService,
        // The requested operation was rejected because of outstanding open handles.
        // Veto Text String: A device instance path.
        PNP_VetoOutstandingOpen,
        // The device supports the specified operation, but the device rejected the operation.
        // Veto Text String: A device instance path.
        PNP_VetoDevice,
        // The driver supports the specified operation, but the driver rejected the operation.
        // Veto Text String: A driver name.
        PNP_VetoDriver,
        // The device does not support the specified operation.
        // Veto Text String: A device instance path.
        PNP_VetoIllegalDeviceRequest,
        // There is insufficient power to perform the requested operation.
        // Veto Text String: None.
        PNP_VetoInsufficientPower,
        // The device cannot be disabled.
        // Veto Text String: A device instance path.
        PNP_VetoNonDisableable,
        // The driver does not support the specified PnP operation.
        // Veto Text String: A Windows service name.
        PNP_VetoLegacyDriver,
        // The caller has insufficient privileges to complete the operation.
        // Veto Text String: 
        PNP_VetoInsufficientRights,
        // 
        // Veto Text String: 
        PNP_VetoAlreadyRemoved
    }

    // Flags for CM_Query_And_Remove_SubTree
    [Flags]
    public enum Query_And_Remove_SubTree_FLAGS : uint
    {
        // The function allows a user dialog box to be displayed to indicate the reason for the veto. This is the default flag setting.
        CM_REMOVE_UI_OK = 0x00000000,
        // The function suppresses the display of a user dialog box that indicates the reason for the veto.
        CM_REMOVE_UI_NOT_OK = 0x00000001,
        // If this flag is set, the function configures the device status such that the device cannot be restarted until after the device status is reset.
        CM_REMOVE_NO_RESTART = 0x00000002,
    }
}
