using static USBDevicesLibrary.Win32API.SetupAPIData;

namespace USBDevicesLibrary.Win32API;

public static class DevicePropKeys
{
    public enum DEVPKEY_ENUM
    {
        // DEVPKEY_NAME
        // Common DEVPKEY used to retrieve the display name for an object.
        // FormatID = 
        DEVPKEY_NAME,

        // Device properties
        // These DEVPKEYs correspond to the SetupAPI SPDRP_XXX device properties.
        // FormatID = 
        DEVPKEY_Device_DeviceDesc,
        DEVPKEY_Device_HardwareIds,
        DEVPKEY_Device_CompatibleIds,
        DEVPKEY_Device_Service,
        DEVPKEY_Device_Class,
        DEVPKEY_Device_ClassGuid,
        DEVPKEY_Device_Driver,
        DEVPKEY_Device_ConfigFlags,
        DEVPKEY_Device_Manufacturer,
        DEVPKEY_Device_FriendlyName,
        DEVPKEY_Device_LocationInfo,
        DEVPKEY_Device_PDOName,
        DEVPKEY_Device_Capabilities,
        DEVPKEY_Device_UINumber,
        DEVPKEY_Device_UpperFilters,
        DEVPKEY_Device_LowerFilters,
        DEVPKEY_Device_BusTypeGuid,
        DEVPKEY_Device_LegacyBusType,
        DEVPKEY_Device_BusNumber,
        DEVPKEY_Device_EnumeratorName,
        DEVPKEY_Device_Security,
        DEVPKEY_Device_SecuritySDS,
        DEVPKEY_Device_DevType,
        DEVPKEY_Device_Exclusive,
        DEVPKEY_Device_Characteristics,
        DEVPKEY_Device_Address,
        DEVPKEY_Device_UINumberDescFormat,
        DEVPKEY_Device_PowerData,
        DEVPKEY_Device_RemovalPolicy,
        DEVPKEY_Device_RemovalPolicyDefault,
        DEVPKEY_Device_RemovalPolicyOverride,
        DEVPKEY_Device_InstallState,
        DEVPKEY_Device_LocationPaths,
        DEVPKEY_Device_BaseContainerId,

        // Device and Device Interface property
        // Common DEVPKEY used to retrieve the device instance id associated with devices and device interfaces.
        // FormatID = 
        DEVPKEY_Device_InstanceId,

        // Device properties
        // These DEVPKEYs correspond to a device's status and problem code.
        // FormatID = 
        DEVPKEY_Device_DevNodeStatus,
        DEVPKEY_Device_ProblemCode,

        // Device properties
        // These DEVPKEYs correspond to a device's relations.
        // FormatID = 
        DEVPKEY_Device_EjectionRelations,
        DEVPKEY_Device_RemovalRelations,
        DEVPKEY_Device_PowerRelations,
        DEVPKEY_Device_BusRelations,
        DEVPKEY_Device_Parent,
        DEVPKEY_Device_Children,
        DEVPKEY_Device_Siblings,
        DEVPKEY_Device_TransportRelations,

        // Device property
        // This DEVPKEY corresponds to a the status code that resulted in a device to be in a problem state.
        // FormatID = 
        DEVPKEY_Device_ProblemStatus,

        // Device properties
        // These DEVPKEYs are set for the corresponding types of root-enumerated devices.
        // FormatID = 
        DEVPKEY_Device_Reported,
        DEVPKEY_Device_Legacy,

        // Device Container Id
        // FormatID = 
        DEVPKEY_Device_ContainerId,
        DEVPKEY_Device_InLocalMachineContainer,

        // Device property
        // This DEVPKEY correspond to a device's model.
        // FormatID = 
        DEVPKEY_Device_Model,

        // Device Experience related Keys
        // FormatID = 
        DEVPKEY_Device_ModelId,
        DEVPKEY_Device_FriendlyNameAttributes,
        DEVPKEY_Device_ManufacturerAttributes,
        DEVPKEY_Device_PresenceNotForDevice,
        DEVPKEY_Device_SignalStrength,
        DEVPKEY_Device_IsAssociateableByUserAction,
        DEVPKEY_Device_ShowInUninstallUI,

        // Hardware Support App (HSA) properties
        // FormatID = 
        DEVPKEY_Device_CompanionApps,
        DEVPKEY_Device_PrimaryCompanionApp,

        // Other Device properties
        // FormatID = 
        DEVPKEY_Device_Numa_Proximity_Domain,
        DEVPKEY_Device_DHP_Rebalance_Policy,
        DEVPKEY_Device_Numa_Node,
        DEVPKEY_Device_BusReportedDeviceDesc,
        DEVPKEY_Device_IsPresent,
        DEVPKEY_Device_HasProblem,
        DEVPKEY_Device_ConfigurationId,
        DEVPKEY_Device_ReportedDeviceIdsHash,
        DEVPKEY_Device_PhysicalDeviceLocation,
        DEVPKEY_Device_BiosDeviceName,
        DEVPKEY_Device_DriverProblemDesc,
        DEVPKEY_Device_DebuggerSafe,
        DEVPKEY_Device_PostInstallInProgress,
        DEVPKEY_Device_Stack,
        DEVPKEY_Device_ExtendedConfigurationIds,
        DEVPKEY_Device_IsRebootRequired,
        DEVPKEY_Device_FirmwareDate,
        DEVPKEY_Device_FirmwareVersion,
        DEVPKEY_Device_FirmwareRevision,
        DEVPKEY_Device_DependencyProviders,
        DEVPKEY_Device_DependencyDependents,
        DEVPKEY_Device_SoftRestartSupported,
        DEVPKEY_Device_ExtendedAddress,
        DEVPKEY_Device_AssignedToGuest,
        DEVPKEY_Device_CreatorProcessId,
        DEVPKEY_Device_FirmwareVendor,
        DEVPKEY_Device_SessionId,

        // Device activity timestamp properties
        // FormatID = 
        DEVPKEY_Device_InstallDate,
        DEVPKEY_Device_FirstInstallDate,
        DEVPKEY_Device_LastArrivalDate,
        DEVPKEY_Device_LastRemovalDate,

        // Device driver properties
        // FormatID = 
        DEVPKEY_Device_DriverDate,
        DEVPKEY_Device_DriverVersion,
        DEVPKEY_Device_DriverDesc,
        DEVPKEY_Device_DriverInfPath,
        DEVPKEY_Device_DriverInfSection,
        DEVPKEY_Device_DriverInfSectionExt,
        DEVPKEY_Device_MatchingDeviceId,
        DEVPKEY_Device_DriverProvider,
        DEVPKEY_Device_DriverPropPageProvider,
        DEVPKEY_Device_DriverCoInstallers,
        DEVPKEY_Device_ResourcePickerTags,
        DEVPKEY_Device_ResourcePickerExceptions,
        DEVPKEY_Device_DriverRank,
        DEVPKEY_Device_DriverLogoLevel,

        // Device properties
        // These DEVPKEYs may be set by the driver package installed for a device.
        // FormatID = 
        DEVPKEY_Device_NoConnectSound,
        DEVPKEY_Device_GenericDriverInstalled,
        DEVPKEY_Device_AdditionalSoftwareRequested,

        // Device safe-removal properties
        // FormatID = 
        DEVPKEY_Device_SafeRemovalRequired,
        DEVPKEY_Device_SafeRemovalRequiredOverride,

        // Device properties
        // These DEVPKEYs may be set by the driver package installed for a device.
        // FormatID = 
        DEVPKEY_DrvPkg_Model,
        DEVPKEY_DrvPkg_VendorWebSite,
        DEVPKEY_DrvPkg_DetailedDescription,
        DEVPKEY_DrvPkg_DocumentationLink,
        DEVPKEY_DrvPkg_Icon,
        DEVPKEY_DrvPkg_BrandingIcon,

        // Device setup class properties
        // These DEVPKEYs correspond to the SetupAPI SPCRP_XXX setup class properties.
        // FormatID = 
        DEVPKEY_DeviceClass_UpperFilters,
        DEVPKEY_DeviceClass_LowerFilters,
        DEVPKEY_DeviceClass_Security,
        DEVPKEY_DeviceClass_SecuritySDS,
        DEVPKEY_DeviceClass_DevType,
        DEVPKEY_DeviceClass_Exclusive,
        DEVPKEY_DeviceClass_Characteristics,

        // Device setup class properties
        // FormatID = 
        DEVPKEY_DeviceClass_Name,
        DEVPKEY_DeviceClass_ClassName,
        DEVPKEY_DeviceClass_Icon,
        DEVPKEY_DeviceClass_ClassInstaller,
        DEVPKEY_DeviceClass_PropPageProvider,
        DEVPKEY_DeviceClass_NoInstallClass,
        DEVPKEY_DeviceClass_NoDisplayClass,
        DEVPKEY_DeviceClass_SilentInstall,
        DEVPKEY_DeviceClass_NoUseClass,
        DEVPKEY_DeviceClass_DefaultService,
        DEVPKEY_DeviceClass_IconPath,

        // Other Device setup class properties
        // FormatID = 
        DEVPKEY_DeviceClass_DHPRebalanceOptOut,
        DEVPKEY_DeviceClass_ClassCoInstallers,

        // Device interface properties
        // FormatID = 
        DEVPKEY_DeviceInterface_FriendlyName,
        DEVPKEY_DeviceInterface_Enabled,
        DEVPKEY_DeviceInterface_ClassGuid,
        DEVPKEY_DeviceInterface_ReferenceString,
        DEVPKEY_DeviceInterface_Restricted,
        DEVPKEY_DeviceInterface_UnrestrictedAppCapabilities,
        DEVPKEY_DeviceInterface_SchematicName,

        // Device interface class properties
        // FormatID = 
        DEVPKEY_DeviceInterfaceClass_DefaultInterface,
        DEVPKEY_DeviceInterfaceClass_Name,

        // Device Container Properties
        // FormatID = 
        DEVPKEY_DeviceContainer_Address,
        DEVPKEY_DeviceContainer_DiscoveryMethod,
        DEVPKEY_DeviceContainer_IsEncrypted,
        DEVPKEY_DeviceContainer_IsAuthenticated,
        DEVPKEY_DeviceContainer_IsConnected,
        DEVPKEY_DeviceContainer_IsPaired,
        DEVPKEY_DeviceContainer_Icon,
        DEVPKEY_DeviceContainer_Version,
        DEVPKEY_DeviceContainer_Last_Seen,
        DEVPKEY_DeviceContainer_Last_Connected,
        DEVPKEY_DeviceContainer_IsShowInDisconnectedState,
        DEVPKEY_DeviceContainer_IsLocalMachine,
        DEVPKEY_DeviceContainer_MetadataPath,
        DEVPKEY_DeviceContainer_IsMetadataSearchInProgress,
        DEVPKEY_DeviceContainer_MetadataChecksum,
        DEVPKEY_DeviceContainer_IsNotInterestingForDisplay,
        DEVPKEY_DeviceContainer_LaunchDeviceStageOnDeviceConnect,
        DEVPKEY_DeviceContainer_LaunchDeviceStageFromExplorer,
        DEVPKEY_DeviceContainer_BaselineExperienceId,
        DEVPKEY_DeviceContainer_IsDeviceUniquelyIdentifiable,
        DEVPKEY_DeviceContainer_AssociationArray,
        DEVPKEY_DeviceContainer_DeviceDescription1,
        DEVPKEY_DeviceContainer_DeviceDescription2,
        DEVPKEY_DeviceContainer_HasProblem,
        DEVPKEY_DeviceContainer_IsSharedDevice,
        DEVPKEY_DeviceContainer_IsNetworkDevice,
        DEVPKEY_DeviceContainer_IsDefaultDevice,
        DEVPKEY_DeviceContainer_MetadataCabinet,
        DEVPKEY_DeviceContainer_RequiresPairingElevation,
        DEVPKEY_DeviceContainer_ExperienceId,
        DEVPKEY_DeviceContainer_Category,
        DEVPKEY_DeviceContainer_Category_Desc_Singular,
        DEVPKEY_DeviceContainer_Category_Desc_Plural,
        DEVPKEY_DeviceContainer_Category_Icon,
        DEVPKEY_DeviceContainer_CategoryGroup_Desc,
        DEVPKEY_DeviceContainer_CategoryGroup_Icon,
        DEVPKEY_DeviceContainer_PrimaryCategory,
        DEVPKEY_DeviceContainer_UnpairUninstall,
        DEVPKEY_DeviceContainer_RequiresUninstallElevation,
        DEVPKEY_DeviceContainer_DeviceFunctionSubRank,
        DEVPKEY_DeviceContainer_AlwaysShowDeviceAsConnected,
        DEVPKEY_DeviceContainer_ConfigFlags,
        DEVPKEY_DeviceContainer_PrivilegedPackageFamilyNames,
        DEVPKEY_DeviceContainer_CustomPrivilegedPackageFamilyNames,
        DEVPKEY_DeviceContainer_IsRebootRequired,
        DEVPKEY_DeviceContainer_FriendlyName,
        DEVPKEY_DeviceContainer_Manufacturer,
        DEVPKEY_DeviceContainer_ModelName,
        DEVPKEY_DeviceContainer_ModelNumber,
        DEVPKEY_DeviceContainer_InstallInProgress,

        // DevQuery properties
        // FormatID = 
        DEVPKEY_DevQuery_ObjectType,
    }

    public static readonly Dictionary<DEVPROPKEY, DEVPKEY_ENUM> DEVPKEY = new Dictionary<DEVPROPKEY, DEVPKEY_ENUM>()
    {
        {new DEVPROPKEY(new Guid("B725F130-47EF-101A-A5F1-02608C9EEBAC"),    10), DEVPKEY_ENUM.DEVPKEY_NAME},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),     2), DEVPKEY_ENUM.DEVPKEY_Device_DeviceDesc},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),     3), DEVPKEY_ENUM.DEVPKEY_Device_HardwareIds},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),     4), DEVPKEY_ENUM.DEVPKEY_Device_CompatibleIds},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),     6), DEVPKEY_ENUM.DEVPKEY_Device_Service},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),     9), DEVPKEY_ENUM.DEVPKEY_Device_Class},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    10), DEVPKEY_ENUM.DEVPKEY_Device_ClassGuid},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    11), DEVPKEY_ENUM.DEVPKEY_Device_Driver},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    12), DEVPKEY_ENUM.DEVPKEY_Device_ConfigFlags},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    13), DEVPKEY_ENUM.DEVPKEY_Device_Manufacturer},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    14), DEVPKEY_ENUM.DEVPKEY_Device_FriendlyName},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    15), DEVPKEY_ENUM.DEVPKEY_Device_LocationInfo},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    16), DEVPKEY_ENUM.DEVPKEY_Device_PDOName},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    17), DEVPKEY_ENUM.DEVPKEY_Device_Capabilities},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    18), DEVPKEY_ENUM.DEVPKEY_Device_UINumber},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    19), DEVPKEY_ENUM.DEVPKEY_Device_UpperFilters},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    20), DEVPKEY_ENUM.DEVPKEY_Device_LowerFilters},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    21), DEVPKEY_ENUM.DEVPKEY_Device_BusTypeGuid},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    22), DEVPKEY_ENUM.DEVPKEY_Device_LegacyBusType},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    23), DEVPKEY_ENUM.DEVPKEY_Device_BusNumber},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    24), DEVPKEY_ENUM.DEVPKEY_Device_EnumeratorName},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    25), DEVPKEY_ENUM.DEVPKEY_Device_Security},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    26), DEVPKEY_ENUM.DEVPKEY_Device_SecuritySDS},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    27), DEVPKEY_ENUM.DEVPKEY_Device_DevType},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    28), DEVPKEY_ENUM.DEVPKEY_Device_Exclusive},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    29), DEVPKEY_ENUM.DEVPKEY_Device_Characteristics},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    30), DEVPKEY_ENUM.DEVPKEY_Device_Address},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    31), DEVPKEY_ENUM.DEVPKEY_Device_UINumberDescFormat},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    32), DEVPKEY_ENUM.DEVPKEY_Device_PowerData},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    33), DEVPKEY_ENUM.DEVPKEY_Device_RemovalPolicy},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    34), DEVPKEY_ENUM.DEVPKEY_Device_RemovalPolicyDefault},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    35), DEVPKEY_ENUM.DEVPKEY_Device_RemovalPolicyOverride},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    36), DEVPKEY_ENUM.DEVPKEY_Device_InstallState},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    37), DEVPKEY_ENUM.DEVPKEY_Device_LocationPaths},
        {new DEVPROPKEY(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),    38), DEVPKEY_ENUM.DEVPKEY_Device_BaseContainerId},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),   256), DEVPKEY_ENUM.DEVPKEY_Device_InstanceId},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     2), DEVPKEY_ENUM.DEVPKEY_Device_DevNodeStatus},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     3), DEVPKEY_ENUM.DEVPKEY_Device_ProblemCode},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     4), DEVPKEY_ENUM.DEVPKEY_Device_EjectionRelations},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     5), DEVPKEY_ENUM.DEVPKEY_Device_RemovalRelations},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     6), DEVPKEY_ENUM.DEVPKEY_Device_PowerRelations},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     7), DEVPKEY_ENUM.DEVPKEY_Device_BusRelations},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     8), DEVPKEY_ENUM.DEVPKEY_Device_Parent},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),     9), DEVPKEY_ENUM.DEVPKEY_Device_Children},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),    10), DEVPKEY_ENUM.DEVPKEY_Device_Siblings},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),    11), DEVPKEY_ENUM.DEVPKEY_Device_TransportRelations},
        {new DEVPROPKEY(new Guid("4340A6C5-93FA-4706-972C-7B648008A5A7"),    12), DEVPKEY_ENUM.DEVPKEY_Device_ProblemStatus},
        {new DEVPROPKEY(new Guid("80497100-8C73-48B9-AAD9-CE387E19C56E"),     2), DEVPKEY_ENUM.DEVPKEY_Device_Reported},
        {new DEVPROPKEY(new Guid("80497100-8C73-48B9-AAD9-CE387E19C56E"),     3), DEVPKEY_ENUM.DEVPKEY_Device_Legacy},
        {new DEVPROPKEY(new Guid("8C7ED206-3F8A-4827-B3AB-AE9E1FAEFC6C"),     2), DEVPKEY_ENUM.DEVPKEY_Device_ContainerId},
        {new DEVPROPKEY(new Guid("8C7ED206-3F8A-4827-B3AB-AE9E1FAEFC6C"),     4), DEVPKEY_ENUM.DEVPKEY_Device_InLocalMachineContainer},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    39), DEVPKEY_ENUM.DEVPKEY_Device_Model},
        {new DEVPROPKEY(new Guid("80D81EA6-7473-4B0C-8216-EFC11A2C4C8B"),     2), DEVPKEY_ENUM.DEVPKEY_Device_ModelId},
        {new DEVPROPKEY(new Guid("80D81EA6-7473-4B0C-8216-EFC11A2C4C8B"),     3), DEVPKEY_ENUM.DEVPKEY_Device_FriendlyNameAttributes},
        {new DEVPROPKEY(new Guid("80D81EA6-7473-4B0C-8216-EFC11A2C4C8B"),     4), DEVPKEY_ENUM.DEVPKEY_Device_ManufacturerAttributes},
        {new DEVPROPKEY(new Guid("80D81EA6-7473-4B0C-8216-EFC11A2C4C8B"),     5), DEVPKEY_ENUM.DEVPKEY_Device_PresenceNotForDevice},
        {new DEVPROPKEY(new Guid("80D81EA6-7473-4B0C-8216-EFC11A2C4C8B"),     6), DEVPKEY_ENUM.DEVPKEY_Device_SignalStrength},
        {new DEVPROPKEY(new Guid("80D81EA6-7473-4B0C-8216-EFC11A2C4C8B"),     7), DEVPKEY_ENUM.DEVPKEY_Device_IsAssociateableByUserAction},
        {new DEVPROPKEY(new Guid("80D81EA6-7473-4B0C-8216-EFC11A2C4C8B"),     8), DEVPKEY_ENUM.DEVPKEY_Device_ShowInUninstallUI},
        {new DEVPROPKEY(new Guid("6A742654-D0B2-4420-A523-E068352AC1DF"),     2), DEVPKEY_ENUM.DEVPKEY_Device_CompanionApps},
        {new DEVPROPKEY(new Guid("6A742654-D0B2-4420-A523-E068352AC1DF"),     3), DEVPKEY_ENUM.DEVPKEY_Device_PrimaryCompanionApp},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     1), DEVPKEY_ENUM.DEVPKEY_Device_Numa_Proximity_Domain},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     2), DEVPKEY_ENUM.DEVPKEY_Device_DHP_Rebalance_Policy},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     3), DEVPKEY_ENUM.DEVPKEY_Device_Numa_Node},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     4), DEVPKEY_ENUM.DEVPKEY_Device_BusReportedDeviceDesc},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     5), DEVPKEY_ENUM.DEVPKEY_Device_IsPresent},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     6), DEVPKEY_ENUM.DEVPKEY_Device_HasProblem},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     7), DEVPKEY_ENUM.DEVPKEY_Device_ConfigurationId},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     8), DEVPKEY_ENUM.DEVPKEY_Device_ReportedDeviceIdsHash},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),     9), DEVPKEY_ENUM.DEVPKEY_Device_PhysicalDeviceLocation},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    10), DEVPKEY_ENUM.DEVPKEY_Device_BiosDeviceName},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    11), DEVPKEY_ENUM.DEVPKEY_Device_DriverProblemDesc},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    12), DEVPKEY_ENUM.DEVPKEY_Device_DebuggerSafe},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    13), DEVPKEY_ENUM.DEVPKEY_Device_PostInstallInProgress},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    14), DEVPKEY_ENUM.DEVPKEY_Device_Stack},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    15), DEVPKEY_ENUM.DEVPKEY_Device_ExtendedConfigurationIds},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    16), DEVPKEY_ENUM.DEVPKEY_Device_IsRebootRequired},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    17), DEVPKEY_ENUM.DEVPKEY_Device_FirmwareDate},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    18), DEVPKEY_ENUM.DEVPKEY_Device_FirmwareVersion},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    19), DEVPKEY_ENUM.DEVPKEY_Device_FirmwareRevision},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    20), DEVPKEY_ENUM.DEVPKEY_Device_DependencyProviders},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    21), DEVPKEY_ENUM.DEVPKEY_Device_DependencyDependents},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    22), DEVPKEY_ENUM.DEVPKEY_Device_SoftRestartSupported},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    23), DEVPKEY_ENUM.DEVPKEY_Device_ExtendedAddress},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    24), DEVPKEY_ENUM.DEVPKEY_Device_AssignedToGuest},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    25), DEVPKEY_ENUM.DEVPKEY_Device_CreatorProcessId},
        {new DEVPROPKEY(new Guid("540B947E-8B40-45BC-A8A2-6A0B894CBDA2"),    26), DEVPKEY_ENUM.DEVPKEY_Device_FirmwareVendor},
        {new DEVPROPKEY(new Guid("83DA6326-97A6-4088-9453-A1923F573B29"),     6), DEVPKEY_ENUM.DEVPKEY_Device_SessionId},
        {new DEVPROPKEY(new Guid("83DA6326-97A6-4088-9453-A1923F573B29"),   100), DEVPKEY_ENUM.DEVPKEY_Device_InstallDate},
        {new DEVPROPKEY(new Guid("83DA6326-97A6-4088-9453-A1923F573B29"),   101), DEVPKEY_ENUM.DEVPKEY_Device_FirstInstallDate},
        {new DEVPROPKEY(new Guid("83DA6326-97A6-4088-9453-A1923F573B29"),   102), DEVPKEY_ENUM.DEVPKEY_Device_LastArrivalDate},
        {new DEVPROPKEY(new Guid("83DA6326-97A6-4088-9453-A1923F573B29"),   103), DEVPKEY_ENUM.DEVPKEY_Device_LastRemovalDate},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     2), DEVPKEY_ENUM.DEVPKEY_Device_DriverDate},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     3), DEVPKEY_ENUM.DEVPKEY_Device_DriverVersion},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     4), DEVPKEY_ENUM.DEVPKEY_Device_DriverDesc},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     5), DEVPKEY_ENUM.DEVPKEY_Device_DriverInfPath},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     6), DEVPKEY_ENUM.DEVPKEY_Device_DriverInfSection},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     7), DEVPKEY_ENUM.DEVPKEY_Device_DriverInfSectionExt},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     8), DEVPKEY_ENUM.DEVPKEY_Device_MatchingDeviceId},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),     9), DEVPKEY_ENUM.DEVPKEY_Device_DriverProvider},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    10), DEVPKEY_ENUM.DEVPKEY_Device_DriverPropPageProvider},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    11), DEVPKEY_ENUM.DEVPKEY_Device_DriverCoInstallers},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    12), DEVPKEY_ENUM.DEVPKEY_Device_ResourcePickerTags},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    13), DEVPKEY_ENUM.DEVPKEY_Device_ResourcePickerExceptions},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    14), DEVPKEY_ENUM.DEVPKEY_Device_DriverRank},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    15), DEVPKEY_ENUM.DEVPKEY_Device_DriverLogoLevel},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    17), DEVPKEY_ENUM.DEVPKEY_Device_NoConnectSound},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    18), DEVPKEY_ENUM.DEVPKEY_Device_GenericDriverInstalled},
        {new DEVPROPKEY(new Guid("A8B865DD-2E3D-4094-AD97-E593A70C75D6"),    19), DEVPKEY_ENUM.DEVPKEY_Device_AdditionalSoftwareRequested},
        {new DEVPROPKEY(new Guid("AFD97640-86A3-4210-B67C-289C41AABE55"),     2), DEVPKEY_ENUM.DEVPKEY_Device_SafeRemovalRequired},
        {new DEVPROPKEY(new Guid("AFD97640-86A3-4210-B67C-289C41AABE55"),     3), DEVPKEY_ENUM.DEVPKEY_Device_SafeRemovalRequiredOverride},
        {new DEVPROPKEY(new Guid("CF73BB51-3ABF-44A2-85E0-9A3DC7A12132"),     2), DEVPKEY_ENUM.DEVPKEY_DrvPkg_Model},
        {new DEVPROPKEY(new Guid("CF73BB51-3ABF-44A2-85E0-9A3DC7A12132"),     3), DEVPKEY_ENUM.DEVPKEY_DrvPkg_VendorWebSite},
        {new DEVPROPKEY(new Guid("CF73BB51-3ABF-44A2-85E0-9A3DC7A12132"),     4), DEVPKEY_ENUM.DEVPKEY_DrvPkg_DetailedDescription},
        {new DEVPROPKEY(new Guid("CF73BB51-3ABF-44A2-85E0-9A3DC7A12132"),     5), DEVPKEY_ENUM.DEVPKEY_DrvPkg_DocumentationLink},
        {new DEVPROPKEY(new Guid("CF73BB51-3ABF-44A2-85E0-9A3DC7A12132"),     6), DEVPKEY_ENUM.DEVPKEY_DrvPkg_Icon},
        {new DEVPROPKEY(new Guid("CF73BB51-3ABF-44A2-85E0-9A3DC7A12132"),     7), DEVPKEY_ENUM.DEVPKEY_DrvPkg_BrandingIcon},
        {new DEVPROPKEY(new Guid("4321918B-F69E-470D-A5DE-4D88C75AD24B"),    19), DEVPKEY_ENUM.DEVPKEY_DeviceClass_UpperFilters},
        {new DEVPROPKEY(new Guid("4321918B-F69E-470D-A5DE-4D88C75AD24B"),    20), DEVPKEY_ENUM.DEVPKEY_DeviceClass_LowerFilters},
        {new DEVPROPKEY(new Guid("4321918B-F69E-470D-A5DE-4D88C75AD24B"),    25), DEVPKEY_ENUM.DEVPKEY_DeviceClass_Security},
        {new DEVPROPKEY(new Guid("4321918B-F69E-470D-A5DE-4D88C75AD24B"),    26), DEVPKEY_ENUM.DEVPKEY_DeviceClass_SecuritySDS},
        {new DEVPROPKEY(new Guid("4321918B-F69E-470D-A5DE-4D88C75AD24B"),    27), DEVPKEY_ENUM.DEVPKEY_DeviceClass_DevType},
        {new DEVPROPKEY(new Guid("4321918B-F69E-470D-A5DE-4D88C75AD24B"),    28), DEVPKEY_ENUM.DEVPKEY_DeviceClass_Exclusive},
        {new DEVPROPKEY(new Guid("4321918B-F69E-470D-A5DE-4D88C75AD24B"),    29), DEVPKEY_ENUM.DEVPKEY_DeviceClass_Characteristics},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     2), DEVPKEY_ENUM.DEVPKEY_DeviceClass_Name},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     3), DEVPKEY_ENUM.DEVPKEY_DeviceClass_ClassName},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     4), DEVPKEY_ENUM.DEVPKEY_DeviceClass_Icon},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     5), DEVPKEY_ENUM.DEVPKEY_DeviceClass_ClassInstaller},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     6), DEVPKEY_ENUM.DEVPKEY_DeviceClass_PropPageProvider},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     7), DEVPKEY_ENUM.DEVPKEY_DeviceClass_NoInstallClass},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     8), DEVPKEY_ENUM.DEVPKEY_DeviceClass_NoDisplayClass},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),     9), DEVPKEY_ENUM.DEVPKEY_DeviceClass_SilentInstall},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),    10), DEVPKEY_ENUM.DEVPKEY_DeviceClass_NoUseClass},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),    11), DEVPKEY_ENUM.DEVPKEY_DeviceClass_DefaultService},
        {new DEVPROPKEY(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"),    12), DEVPKEY_ENUM.DEVPKEY_DeviceClass_IconPath},
        {new DEVPROPKEY(new Guid("D14D3EF3-66CF-4BA2-9D38-0DDB37AB4701"),     2), DEVPKEY_ENUM.DEVPKEY_DeviceClass_DHPRebalanceOptOut},
        {new DEVPROPKEY(new Guid("713D1703-A2E2-49F5-9214-56472EF3DA5C"),     2), DEVPKEY_ENUM.DEVPKEY_DeviceClass_ClassCoInstallers},
        {new DEVPROPKEY(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),     2), DEVPKEY_ENUM.DEVPKEY_DeviceInterface_FriendlyName},
        {new DEVPROPKEY(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),     3), DEVPKEY_ENUM.DEVPKEY_DeviceInterface_Enabled},
        {new DEVPROPKEY(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),     4), DEVPKEY_ENUM.DEVPKEY_DeviceInterface_ClassGuid},
        {new DEVPROPKEY(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),     5), DEVPKEY_ENUM.DEVPKEY_DeviceInterface_ReferenceString},
        {new DEVPROPKEY(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),     6), DEVPKEY_ENUM.DEVPKEY_DeviceInterface_Restricted},
        {new DEVPROPKEY(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),     8), DEVPKEY_ENUM.DEVPKEY_DeviceInterface_UnrestrictedAppCapabilities},
        {new DEVPROPKEY(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),     9), DEVPKEY_ENUM.DEVPKEY_DeviceInterface_SchematicName},
        {new DEVPROPKEY(new Guid("14C83A99-0B3F-44B7-BE4C-A178D3990564"),     2), DEVPKEY_ENUM.DEVPKEY_DeviceInterfaceClass_DefaultInterface},
        {new DEVPROPKEY(new Guid("14C83A99-0B3F-44B7-BE4C-A178D3990564"),     3), DEVPKEY_ENUM.DEVPKEY_DeviceInterfaceClass_Name},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    51), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Address},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    52), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_DiscoveryMethod},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    53), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsEncrypted},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    54), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsAuthenticated},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    55), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsConnected},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    56), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsPaired},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    57), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Icon},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    65), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Version},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    66), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Last_Seen},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    67), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Last_Connected},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    68), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsShowInDisconnectedState},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    70), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsLocalMachine},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    71), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_MetadataPath},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    72), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsMetadataSearchInProgress},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    73), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_MetadataChecksum},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    74), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsNotInterestingForDisplay},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    76), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_LaunchDeviceStageOnDeviceConnect},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    77), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_LaunchDeviceStageFromExplorer},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    78), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_BaselineExperienceId},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    79), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsDeviceUniquelyIdentifiable},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    80), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_AssociationArray},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    81), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_DeviceDescription1},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    82), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_DeviceDescription2},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    83), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_HasProblem},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    84), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsSharedDevice},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    85), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsNetworkDevice},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    86), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsDefaultDevice},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    87), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_MetadataCabinet},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    88), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_RequiresPairingElevation},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    89), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_ExperienceId},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    90), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Category},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    91), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Category_Desc_Singular},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    92), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Category_Desc_Plural},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    93), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Category_Icon},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    94), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_CategoryGroup_Desc},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    95), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_CategoryGroup_Icon},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    97), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_PrimaryCategory},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    98), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_UnpairUninstall},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),    99), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_RequiresUninstallElevation},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),   100), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_DeviceFunctionSubRank},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),   101), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_AlwaysShowDeviceAsConnected},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),   105), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_ConfigFlags},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),   106), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_PrivilegedPackageFamilyNames},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),   107), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_CustomPrivilegedPackageFamilyNames},
        {new DEVPROPKEY(new Guid("78C34FC8-104A-4ACA-9EA4-524D52996E57"),   108), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_IsRebootRequired},
        {new DEVPROPKEY(new Guid("656A3BB3-ECC0-43FD-8477-4AE0404A96CD"), 12288), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_FriendlyName},
        {new DEVPROPKEY(new Guid("656A3BB3-ECC0-43FD-8477-4AE0404A96CD"),  8192), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_Manufacturer},
        {new DEVPROPKEY(new Guid("656A3BB3-ECC0-43FD-8477-4AE0404A96CD"),  8194), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_ModelName},
        {new DEVPROPKEY(new Guid("656A3BB3-ECC0-43FD-8477-4AE0404A96CD"),  8195), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_ModelNumber},
        {new DEVPROPKEY(new Guid("83DA6326-97A6-4088-9453-A1923F573B29"),     9), DEVPKEY_ENUM.DEVPKEY_DeviceContainer_InstallInProgress},
        {new DEVPROPKEY(new Guid("13673F42-A3D6-49F6-B4DA-AE46E0C5237C"),     2), DEVPKEY_ENUM.DEVPKEY_DevQuery_ObjectType},
    };
}


