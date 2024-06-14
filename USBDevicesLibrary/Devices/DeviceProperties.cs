using System.Security.AccessControl;
using static USBDevicesLibrary.Win32API.SetupAPIData;
using static USBDevicesLibrary.Win32API.WinIOCtlData;

namespace USBDevicesLibrary.Devices;

public class DeviceProperties
{
    public DeviceProperties()
    {
        NAME = string.Empty;
        Device_DeviceDesc = string.Empty;
        Device_Service = string.Empty;
        Device_Class = string.Empty;
        Device_Driver = string.Empty;
        Device_Manufacturer = string.Empty;
        Device_FriendlyName = string.Empty;
        Device_LocationInfo = string.Empty;
        Device_PDOName = string.Empty;
        Device_EnumeratorName = string.Empty;
        Device_SecuritySDS = string.Empty;
        Device_UINumberDescFormat = string.Empty;
        Device_InstanceId = string.Empty;
        Device_Parent = string.Empty;
        Device_Model = string.Empty;
        Device_PrimaryCompanionApp = string.Empty;
        Device_BusReportedDeviceDesc = string.Empty;
        Device_ConfigurationId = string.Empty;
        Device_BiosDeviceName = string.Empty;
        Device_DriverProblemDesc = string.Empty;
        Device_FirmwareVersion = string.Empty;
        Device_FirmwareRevision = string.Empty;
        Device_FirmwareVendor = string.Empty;
        Device_DriverVersion = string.Empty;
        Device_DriverDesc = string.Empty;
        Device_DriverInfPath = string.Empty;
        Device_DriverInfSection = string.Empty;
        Device_DriverInfSectionExt = string.Empty;
        Device_MatchingDeviceId = string.Empty;
        Device_DriverProvider = string.Empty;
        Device_DriverPropPageProvider = string.Empty;
        Device_ResourcePickerTags = string.Empty;
        Device_ResourcePickerExceptions = string.Empty;

        Device_HardwareIds = [];
        Device_CompatibleIds = [];
        Device_UpperFilters = [];
        Device_LowerFilters = [];
        Device_LocationPaths = [];
        Device_EjectionRelations = [];
        Device_RemovalRelations = [];
        Device_PowerRelations = [];
        Device_BusRelations = [];
        Device_Children = [];
        Device_Siblings = [];
        Device_TransportRelations = [];
        Device_CompanionApps = [];
        Device_Stack = [];
        Device_ExtendedConfigurationIds = [];
        Device_DependencyProviders = [];
        Device_DependencyDependents = [];
        Device_DriverCoInstallers = [];
        Device_PowerData = [];
        Device_ConfigFlags = [];
        Device_Capabilities = [];
        Device_Characteristics = [];
        Device_DevNodeStatus = [];
        Device_DriverRank = [];

        OtherProperties = new();
    }

    public uint DevInst { get; set; }
    // DEVPKEY_NAME
    // Common DEVPKEY used to retrieve the display name for an object.
    // # 1
    public string NAME { get; set; }

    // Device properties
    // These DEVPKEYs correspond to the SetupAPI SPDRP_XXX device properties.
    // # 34
    public string Device_DeviceDesc { get; set; }
    public List<string> Device_HardwareIds { get; set; }
    public List<string> Device_CompatibleIds { get; set; }
    public string Device_Service { get; set; }
    public string Device_Class { get; set; }
    public Guid Device_ClassGuid { get; set; }
    public string Device_Driver { get; set; }
    public List<CONFIG_FLAGS> Device_ConfigFlags { get; set; }
    public string Device_Manufacturer { get; set; }
    public string Device_FriendlyName { get; set; }
    public string Device_LocationInfo { get; set; }
    public string Device_PDOName { get; set; }
    public List<CM_DRP_CAPABILITIES> Device_Capabilities { get; set; }
    public uint Device_UINumber { get; set; }
    public List<string> Device_UpperFilters { get; set; }
    public List<string> Device_LowerFilters { get; set; }
    public Guid Device_BusTypeGuid { get; set; }
    public INTERFACE_TYPE Device_LegacyBusType { get; set; }
    public uint Device_BusNumber { get; set; }
    public string Device_EnumeratorName { get; set; }
    public RawSecurityDescriptor? Device_Security { get; set; }
    public string Device_SecuritySDS { get; set; }
    public DEVICE_TYPES Device_DevType { get; set; }
    public bool Device_Exclusive { get; set; }
    public List<DEVICE_CHARACTERISTICS> Device_Characteristics { get; set; }
    public uint Device_Address { get; set; }
    public string Device_UINumberDescFormat { get; set; }
    public List<DEVICED_POWER_INFORMATION> Device_PowerData { get; set; }
    public CM_REMOVAL_POLICY Device_RemovalPolicy { get; set; }
    public CM_REMOVAL_POLICY Device_RemovalPolicyDefault { get; set; }
    public CM_REMOVAL_POLICY Device_RemovalPolicyOverride { get; set; }
    public DEVICE_INSTALL_STATE Device_InstallState { get; set; }
    public List<string> Device_LocationPaths { get; set; }
    public Guid Device_BaseContainerId { get; set; }

    // Device and Device Interface property
    // Common DEVPKEY used to retrieve the device instance id associated with devices and device interfaces.
    // # 1
    public string Device_InstanceId { get; set; }

    // Device properties
    // These DEVPKEYs correspond to a device's status and problem code.
    // # 2
    public List<DEVICE_INSTANCE_STATUS> Device_DevNodeStatus { get; set; }
    public uint Device_ProblemCode { get; set; }

    // Device properties
    // These DEVPKEYs correspond to a device's relations.
    // # 8
    public List<string> Device_EjectionRelations { get; set; }
    public List<string> Device_RemovalRelations { get; set; }
    public List<string> Device_PowerRelations { get; set; }
    public List<string> Device_BusRelations { get; set; }
    public string Device_Parent { get; set; }
    public List<string> Device_Children { get; set; }
    public List<string> Device_Siblings { get; set; }
    public List<string> Device_TransportRelations { get; set; }

    // Device property
    // This DEVPKEY corresponds to a the status code that resulted in a device to be in a problem state.
    // # 1
    public uint Device_ProblemStatus { get; set; }

    // Device properties
    // These DEVPKEYs are set for the corresponding types of root-enumerated devices.
    // # 2
    public bool Device_Reported { get; set; }
    public bool Device_Legacy { get; set; }

    // Device Container Id
    // # 2
    public Guid Device_ContainerId { get; set; }
    public bool Device_InLocalMachineContainer { get; set; }

    // Device property
    // This DEVPKEY correspond to a device's model.
    // # 1
    public string Device_Model { get; set; }

    // Device Experience related Keys
    // # 7
    public Guid Device_ModelId { get; set; }
    public uint Device_FriendlyNameAttributes { get; set; }
    public uint Device_ManufacturerAttributes { get; set; }
    public bool Device_PresenceNotForDevice { get; set; }
    public int Device_SignalStrength { get; set; }
    public bool Device_IsAssociateableByUserAction { get; set; }
    public bool Device_ShowInUninstallUI {  get; set; }

    // Hardware Support App (HSA) properties
    // # 2
    public List<string> Device_CompanionApps { get; set; }
    public string Device_PrimaryCompanionApp { get; set; }

    // Other Device properties
    // # 27
    public uint Device_Numa_Proximity_Domain { get; set; }
    public uint Device_DHP_Rebalance_Policy { get; set; }
    public uint Device_Numa_Node { get; set; }
    public string Device_BusReportedDeviceDesc { get; set; }
    public bool Device_IsPresent { get; set; }
    public bool Device_HasProblem { get; set; }
    public string Device_ConfigurationId { get; set; }
    public uint Device_ReportedDeviceIdsHash { get; set; }
    public byte Device_PhysicalDeviceLocation { get; set; }
    public string Device_BiosDeviceName { get; set; }
    public string Device_DriverProblemDesc { get; set; }
    public uint Device_DebuggerSafe { get; set; }
    public bool Device_PostInstallInProgress { get; set; }
    public List<string> Device_Stack { get; set; }
    public List<string> Device_ExtendedConfigurationIds { get; set; }
    public bool Device_IsRebootRequired {  get; set; }
    public DateTime Device_FirmwareDate { get; set; }
    public string Device_FirmwareVersion { get; set; }
    public string Device_FirmwareRevision { get; set; }
    public List<string> Device_DependencyProviders { get; set; }
    public List<string> Device_DependencyDependents { get; set; }
    public bool Device_SoftRestartSupported { get; set; }
    public UInt64 Device_ExtendedAddress { get; set; }
    public bool Device_AssignedToGuest { get; set; }
    public uint Device_CreatorProcessId { get; set; }
    public string Device_FirmwareVendor { get; set; }
    public uint Device_SessionId { get; set; }

    // Device activity timestamp properties
    // # 4
    public DateTime Device_InstallDate { get; set; }
    public DateTime Device_FirstInstallDate { get; set; }
    public DateTime Device_LastArrivalDate { get; set; }
    public DateTime Device_LastRemovalDate { get; set; }

    // Device driver properties
    // # 14
    public DateTime Device_DriverDate { get; set; }
    public string Device_DriverVersion { get; set; }
    public string Device_DriverDesc { get; set; }
    public string Device_DriverInfPath { get; set; }
    public string Device_DriverInfSection { get; set; }
    public string Device_DriverInfSectionExt { get; set; }
    public string Device_MatchingDeviceId { get; set; }
    public string Device_DriverProvider { get; set; }
    public string Device_DriverPropPageProvider { get; set; }
    public List<string> Device_DriverCoInstallers { get; set; }
    public string Device_ResourcePickerTags { get; set; }
    public string Device_ResourcePickerExceptions { get; set; }
    public List<SP_DRVINSTALL_PARAMS> Device_DriverRank { get; set; }
    public uint Device_DriverLogoLevel { get; set; }

    // Device properties
    // These DEVPKEYs may be set by the driver package installed for a device.
    // # 3
    public bool Device_NoConnectSound { get; set; }
    public bool Device_GenericDriverInstalled { get; set; }
    public bool Device_AdditionalSoftwareRequested { get; set; }

    // Device safe-removal properties
    // 2
    public bool Device_SafeRemovalRequired { get; set; }
    public bool Device_SafeRemovalRequiredOverride { get; set; }
    
    // DevQuery properties
    // # 1
    public uint DevQuery_ObjectType { get; set; }

    public SortedList<string, object> OtherProperties { get; set; }
}
