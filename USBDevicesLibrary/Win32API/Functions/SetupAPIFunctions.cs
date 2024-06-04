using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.SetupAPIData;

// https://learn.microsoft.com/en-us/windows/win32/api/setupapi/

namespace USBDevicesLibrary.Win32API;

public static partial class SetupAPIFunctions
{
    private const string _DLLName = "setupapi.dll";
    private const bool _LastErrorStatus = true;
    private const CharSet _CharSet= CharSet.Unicode;

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern IntPtr
        SetupDiGetClassDevs
        (
        [In, Optional] Guid ClassGuid,
        [In, Optional] string Enumerator,
        [In, Optional] IntPtr hwndParent,
        [In] uint Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        SetupDiEnumDeviceInfo
        (
        [In] IntPtr hDevInfo,
        [In] uint memberIndex,
        [Out] out SP_DEVINFO_DATA deviceInfoData
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern int 
        CM_Get_Device_ID
        (
        [In] uint dnDevInst,
        [Out] IntPtr DeviceID,
        [In] int BufferLen,
        [In] uint ulFlags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        SetupDiEnumDeviceInterfaces
        (
        [In] IntPtr hDevInfo,
        [In, Optional] SP_DEVINFO_DATA deviceInfoData,
        [In] Guid interfaceClassGuid,
        [In] uint memberIndex,
        [Out] out SpDeviceInterfaceData deviceInterfaceData
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        SetupDiGetDeviceInterfaceDetail
        (
        [In] IntPtr hDevInfo,
        [In] SpDeviceInterfaceData deviceInterfaceData,
        ref SpDeviceInterfaceDetailData deviceInterfaceDetailData,
        [In] uint deviceInterfaceDetailDataSize,
        [Out] out uint requiredSize,
        [Out] out SP_DEVINFO_DATA deviceInfoData
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        SetupDiGetDeviceRegistryProperty
        (
        [In] IntPtr hDevInfo,
        [In] ref SP_DEVINFO_DATA devInfoData,
        [In] uint Property,
        [Out, Optional] out uint PropertyRegDataType,
        [Out] byte[] PropertyBuffer,
        [In] uint PropertyBufferSize,
        [Out, Optional] out uint RequiredSize 
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        SetupDiDestroyDeviceInfoList
        (
        [In] IntPtr hDevInfo
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiGetClassDescriptionW
        (
        [In] Guid ClassGuid,
        [Out] out string ClassDescription,
        [In] ref uint ClassDescriptionSize,
        [Out, Optional] out uint RequiredSize
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        CM_Get_DevNode_Property
        (
        [In] IntPtr hDevInfo,
        ref SP_DEVINFO_DATA devInfoData,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        [In] uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        [In] uint Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiGetDevicePropertyKeys
        (
        [In] IntPtr hDevInfo,
        ref SP_DEVINFO_DATA devInfoData,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        [In] uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        [In] uint Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiGetDevicePropertyW
        (
        [In] IntPtr hDevInfo,
        [In] SP_DEVINFO_DATA DeviceInfoData,
        [In] DEVPROPKEY PropertyKey,
        [Out] out DevPropType PropertyType,
        [Out, Optional] byte[] PropertyBuffer,
        [In] uint PropertyBufferSize,
        [Out, Optional] out uint RequiredSize,
        [In] uint Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiGetDeviceInterfacePropertyKeys
        (
        [In] IntPtr hDevInfo,
        [In] SpDeviceInterfaceData deviceInterfaceData,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        [In] uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        [In] uint Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiGetDeviceInterfacePropertyW
        (
        [In] IntPtr hDevInfo,
        [In] SpDeviceInterfaceData DeviceInfoData,
        [In] DEVPROPKEY PropertyKey,
        [Out] out DevPropType PropertyType,
        [Out, Optional] byte[] PropertyBuffer,
        [In] uint PropertyBufferSize,
        [Out, Optional] out uint RequiredSize,
        [In] uint Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiGetClassPropertyKeys
        (
        [In] Guid ClassGuid,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        [In] uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        [In] DICLASSPROP Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiGetClassPropertyKeysExW
        (
        [In] Guid ClassGuid,
        [Out] DEVPROPKEY[] PropertyKeyArray,
        [In] uint PropertyKeyCount,
        ref uint RequiredPropertyKeyCount,
        [In] DICLASSPROP Flags,
        [In, Optional] string MachineName,
        IntPtr Reserved
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool SetupDiGetClassPropertyW
        (
        [In] Guid ClassGuid,
        [In] DEVPROPKEY PropertyKey,
        [Out] out DevPropType PropertyType,
        [Out, Optional] byte[] PropertyBuffer,
        [In] uint PropertyBufferSize,
        [Out, Optional] out uint RequiredSize,
        [In] DICLASSPROP Flags
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        SetupDiEnumDriverInfoW
        (
        [In] IntPtr hDevInfo,
        [In] SP_DEVINFO_DATA DeviceInfoData,
        [In, Optional] SPDIT DriverType,
        [In] uint MemberIndex,
        [Out] out PSP_DRVINFO_DATA_W DriverInfoData
        );
}

public class SafeMemoryBuffer : SafeHandleMinusOneIsInvalid
{
    public SafeMemoryBuffer() : base(true) { }
    public SafeMemoryBuffer(int cb) : base(true)
    {
        base.SetHandle(Marshal.AllocHGlobal(cb));
    }
    public SafeMemoryBuffer(IntPtr ptr) : base(true)
    {
        base.SetHandle(ptr);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected override bool ReleaseHandle()
    {
        if (handle != IntPtr.Zero)
            Marshal.FreeHGlobal(handle);
        return true;
    }
}

