using static USBDevicesLibrary.Win32API.SetupAPIData;

namespace USBDevicesLibrary.Devices;

public class DeviceContainerProperties
{
    public DeviceContainerProperties()
    {
        DeviceContainer_Icon = string.Empty;
        DeviceContainer_Version = string.Empty;
        DeviceContainer_MetadataPath = string.Empty;
        DeviceContainer_DeviceDescription1 = string.Empty;
        DeviceContainer_DeviceDescription2 = string.Empty;
        DeviceContainer_MetadataCabinet = string.Empty;
        DeviceContainer_Category_Icon = string.Empty;
        DeviceContainer_CategoryGroup_Icon = string.Empty;
        DeviceContainer_PrimaryCategory = string.Empty;
        DeviceContainer_FriendlyName = string.Empty;
        DeviceContainer_Manufacturer = string.Empty;
        DeviceContainer_ModelName = string.Empty;
        DeviceContainer_ModelNumber = string.Empty;

        DeviceContainer_Address = [];
        DeviceContainer_DiscoveryMethod = [];
        DeviceContainer_AssociationArray = [];
        DeviceContainer_Category = [];
        DeviceContainer_Category_Desc_Singular = [];
        DeviceContainer_Category_Desc_Plural = [];
        DeviceContainer_CategoryGroup_Desc = [];
        DeviceContainer_PrivilegedPackageFamilyNames = [];
        DeviceContainer_CustomPrivilegedPackageFamilyNames = [];
        DeviceContainer_ConfigFlags = [];

        OtherProperties = new();
    }

    // Device Container Properties
    // # 50
    public List<string> DeviceContainer_Address { get; set; }
    public List<string> DeviceContainer_DiscoveryMethod { get; set; }
    public bool DeviceContainer_IsEncrypted { get; set; }
    public bool DeviceContainer_IsAuthenticated { get; set; }
    public bool DeviceContainer_IsConnected { get; set; }
    public bool DeviceContainer_IsPaired { get; set; }
    public string DeviceContainer_Icon { get; set; }
    public string DeviceContainer_Version { get; set; }
    public DateTime DeviceContainer_Last_Seen { get; set; }
    public DateTime DeviceContainer_Last_Connected { get; set; }
    public bool DeviceContainer_IsShowInDisconnectedState { get; set; }
    public bool DeviceContainer_IsLocalMachine { get; set; }
    public string DeviceContainer_MetadataPath { get; set; }
    public bool DeviceContainer_IsMetadataSearchInProgress { get; set; }
    public byte DeviceContainer_MetadataChecksum { get; set; }
    public bool DeviceContainer_IsNotInterestingForDisplay { get; set; }
    public bool DeviceContainer_LaunchDeviceStageOnDeviceConnect { get; set; }
    public bool DeviceContainer_LaunchDeviceStageFromExplorer { get; set; }
    public Guid DeviceContainer_BaselineExperienceId { get; set; }
    public bool DeviceContainer_IsDeviceUniquelyIdentifiable { get; set; }
    public List<string> DeviceContainer_AssociationArray { get; set; }
    public string DeviceContainer_DeviceDescription1 { get; set; }
    public string DeviceContainer_DeviceDescription2 { get; set; }
    public bool DeviceContainer_HasProblem { get; set; }
    public bool DeviceContainer_IsSharedDevice { get; set; }
    public bool DeviceContainer_IsNetworkDevice { get; set; }
    public bool DeviceContainer_IsDefaultDevice { get; set; }
    public string DeviceContainer_MetadataCabinet { get; set; }
    public bool DeviceContainer_RequiresPairingElevation { get; set; }
    public Guid DeviceContainer_ExperienceId { get; set; }
    public List<string> DeviceContainer_Category { get; set; }
    public List<string> DeviceContainer_Category_Desc_Singular { get; set; }
    public List<string> DeviceContainer_Category_Desc_Plural { get; set; }
    public string DeviceContainer_Category_Icon { get; set; }
    public List<string> DeviceContainer_CategoryGroup_Desc { get; set; }
    public string DeviceContainer_CategoryGroup_Icon { get; set; }
    public string DeviceContainer_PrimaryCategory { get; set; }
    public bool DeviceContainer_UnpairUninstall { get; set; }
    public bool DeviceContainer_RequiresUninstallElevation { get; set; }
    public uint DeviceContainer_DeviceFunctionSubRank { get; set; }
    public bool DeviceContainer_AlwaysShowDeviceAsConnected { get; set; }
    public List<CONFIG_FLAGS> DeviceContainer_ConfigFlags { get; set; }
    public List<string> DeviceContainer_PrivilegedPackageFamilyNames { get; set; }
    public List<string> DeviceContainer_CustomPrivilegedPackageFamilyNames { get; set; }
    public bool DeviceContainer_IsRebootRequired { get; set; }
    public string DeviceContainer_FriendlyName { get; set; }
    public string DeviceContainer_Manufacturer { get; set; }
    public string DeviceContainer_ModelName { get; set; }
    public string DeviceContainer_ModelNumber { get; set; }
    public bool DeviceContainer_InstallInProgress { get; set; }

    public SortedList<string, object> OtherProperties { get; set; }
}
