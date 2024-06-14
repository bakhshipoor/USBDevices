using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.CfgMgrData;
using static USBDevicesLibrary.Win32API.SetupAPIData;

// https://learn.microsoft.com/en-us/windows/win32/api/setupapi/

namespace USBDevicesLibrary.Win32API;

public static partial class SetupAPIFunctions
{
    private const string _DLLName = "setupapi.dll";
    private const bool _LastErrorStatus = true;
    private const CharSet _CharSet= CharSet.Unicode;

    [LibraryImport(_DLLName, SetLastError = _LastErrorStatus, StringMarshalling = StringMarshalling.Utf16)]
    public static partial IntPtr
        SetupDiGetClassDevsW
        (
        Guid ClassGuid,
        string Enumerator,
        IntPtr hwndParent,
        uint Flags
        );

    [LibraryImport( _DLLName, SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool 
        SetupDiEnumDeviceInfo
        (
        IntPtr hDevInfo,
        uint memberIndex,
        out SP_DEVINFO_DATA deviceInfoData
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiGetDeviceInstanceIdW", SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetDeviceInstanceIdW
        (
        IntPtr DeviceInfoSet,
        SP_DEVINFO_DATA DeviceInfoData,
        IntPtr DeviceInstanceId,
        uint DeviceInstanceIdSize,
        out uint RequiredSize
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiEnumDeviceInterfaces", SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool 
        SetupDiEnumDeviceInterfaces
        (
        IntPtr hDevInfo,
        SP_DEVINFO_DATA deviceInfoData,
        Guid interfaceClassGuid,
        uint memberIndex,
        out SpDeviceInterfaceData deviceInterfaceData
        );

    [LibraryImportAttribute(_DLLName, EntryPoint = "SetupDiGetDeviceInterfaceDetailW", SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool 
        SetupDiGetDeviceInterfaceDetail
        (
        IntPtr hDevInfo,
        SpDeviceInterfaceData deviceInterfaceData,
#pragma warning disable SYSLIB1051 // Specified type is not supported by source-generated P/Invokes
        ref SpDeviceInterfaceDetailData deviceInterfaceDetailData,
#pragma warning restore SYSLIB1051 // Specified type is not supported by source-generated P/Invokes
        uint deviceInterfaceDetailDataSize,
        out uint requiredSize,
        out SP_DEVINFO_DATA deviceInfoData
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiGetDeviceRegistryPropertyW", SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool 
        SetupDiGetDeviceRegistryProperty
        (
        IntPtr hDevInfo,
        ref SP_DEVINFO_DATA devInfoData,
        uint Property,
        out uint PropertyRegDataType,
        [Out] byte[] PropertyBuffer,
        uint PropertyBufferSize,
        out uint RequiredSize 
        );

    [LibraryImport(_DLLName, SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool 
        SetupDiDestroyDeviceInfoList
        (
        IntPtr hDevInfo
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiGetClassDescriptionW", SetLastError = _LastErrorStatus, StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetClassDescriptionW
        (
        Guid ClassGuid,
        out string ClassDescription,
        ref uint ClassDescriptionSize,
        out uint RequiredSize
        );

    [LibraryImport(_DLLName, SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetDevicePropertyKeys
        (
        IntPtr hDevInfo,
        ref SP_DEVINFO_DATA devInfoData,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        uint Flags
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiGetDevicePropertyW", SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetDevicePropertyW
        (
        IntPtr hDevInfo,
        SP_DEVINFO_DATA DeviceInfoData,
        DEVPROPKEY PropertyKey,
        out DevPropType PropertyType,
        [Out] byte[] PropertyBuffer,
        uint PropertyBufferSize,
        out uint RequiredSize,
        uint Flags
        );

    [LibraryImport(_DLLName, SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetDeviceInterfacePropertyKeys
        (
        IntPtr hDevInfo,
        SpDeviceInterfaceData deviceInterfaceData,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        uint Flags
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiGetDeviceInterfacePropertyW", SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetDeviceInterfacePropertyW
        (
        IntPtr hDevInfo,
        SpDeviceInterfaceData DeviceInfoData,
        DEVPROPKEY PropertyKey,
        out DevPropType PropertyType,
        [Out] byte[] PropertyBuffer,
        uint PropertyBufferSize,
        out uint RequiredSize,
        uint Flags
        );

    [LibraryImport(_DLLName, SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetClassPropertyKeys
        (
        Guid ClassGuid,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        DICLASSPROP Flags
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiGetClassPropertyKeysExW", SetLastError = _LastErrorStatus, StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiGetClassPropertyKeysExW
        (
        Guid ClassGuid,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        DICLASSPROP Flags,
        string MachineName,
        IntPtr Reserved
        );

    [LibraryImport(_DLLName, EntryPoint = "SetupDiGetClassPropertyW", SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool 
        SetupDiGetClassPropertyW
        (
        Guid ClassGuid,
        DEVPROPKEY PropertyKey,
        out DevPropType PropertyType,
        [Out] byte[] PropertyBuffer,
        uint PropertyBufferSize,
        out uint RequiredSize,
        DICLASSPROP Flags
        );

    [LibraryImport(_DLLName, SetLastError = _LastErrorStatus)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool
        SetupDiEnumDriverInfoW
        (
        IntPtr hDevInfo,
        SP_DEVINFO_DATA DeviceInfoData,
        SPDIT DriverType,
        uint MemberIndex,
#pragma warning disable SYSLIB1051 // Specified type is not supported by source-generated P/Invokes
        out SP_DRVINFO_DATA_W DriverInfoData
#pragma warning restore SYSLIB1051 // Specified type is not supported by source-generated P/Invokes
        );

    [DllImport(_DLLName, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern CONFIGRET
        CM_Query_And_Remove_SubTreeW
        (
        uint dnDevInst,
        out PNP_VETO_TYPE pVetoType,
        StringBuilder pszVetoName,
        uint ulNameLength,
        Query_And_Remove_SubTree_FLAGS ulFlags
        );
}