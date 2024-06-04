using Microsoft.Win32.SafeHandles;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using static USBDevicesLibrary.Win32API.Kernel32Data;
using static USBDevicesLibrary.Win32API.USBIOCtl;

namespace USBDevicesLibrary.Win32API;

public static partial class Kernel32Functions
{
    public static Win32ResponseDataStruct CreateDeviceHandle(string devicePath)
    {
        Win32ResponseDataStruct bResponse = new();
        SafeFileHandle deviceHandle = CreateFile(devicePath, ((uint)ACCESSTYPES.GENERIC_WRITE | (uint)ACCESSTYPES.GENERIC_READ), (uint)FilesAccessRights.FILE_SHARE_READ | (uint)FilesAccessRights.FILE_SHARE_WRITE, IntPtr.Zero, (uint)FileConsatnts.OPEN_EXISTING, (uint)FilesAccessRights.FILE_ATTRIBUTE_NORMAL | (uint)FileFlags.FILE_FLAG_OVERLAPPED, IntPtr.Zero);
        if (deviceHandle.DangerousGetHandle()!=-1)
        {
            bResponse.Status = true;
            bResponse.Data = deviceHandle;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "CreateFile [Device Handle]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDeviceIoControl<T>(SafeFileHandle fileHandle, [DisallowNull] T structure, uint ctlCode)
    {
        Win32ResponseDataStruct bResponse = new();
        bool isSuccess = false;
        uint returnedSize=0;
        uint structureSize = (uint)Marshal.SizeOf(structure);
        IntPtr structurePtr = Marshal.AllocHGlobal((int)structureSize);
        Marshal.StructureToPtr(structure, structurePtr, true);
        isSuccess = DeviceIoControl
            (
            fileHandle,
            ctlCode,
            structurePtr,
            structureSize,
            structurePtr,
            structureSize, 
            out returnedSize, 
            IntPtr.Zero
            );
        if (isSuccess)
        {
            T? st = Marshal.PtrToStructure<T>(structurePtr);
            if (st != null)
                structure = st;
            bResponse.Status = true;
            bResponse.Data = structure;
            bResponse.LengthTransferred = returnedSize;
        }
        else
        {
            bResponse.Status = false;
            bResponse.ErrorCode = Marshal.GetLastWin32Error();
            bResponse.ErrorDescription = WindowsSystemErrorCodes.Win32Error[bResponse.ErrorCode];
            bResponse.ErrorFunctionName = "CreateFile [Device Handle]";
        }
        return bResponse;
    }
}
