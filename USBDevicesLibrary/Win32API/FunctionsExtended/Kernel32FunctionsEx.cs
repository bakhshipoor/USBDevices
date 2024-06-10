using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using static USBDevicesLibrary.Win32API.Kernel32Data;
using static USBDevicesLibrary.Win32API.USBIOCtl;

namespace USBDevicesLibrary.Win32API;

public static partial class Kernel32Functions
{
    public static Win32ResponseDataStruct CreateDeviceHandle(string devicePath, [AllowNull] bool readOnly=false)
    {
        Win32ResponseDataStruct bResponse = new();
        SafeFileHandle deviceHandle;
        if (readOnly)
        {
            deviceHandle = CreateFile(
                devicePath,
                (uint)ACCESSTYPES.STANDARD_RIGHTS_READ,
                (uint)FilesAccessRights.FILE_SHARE_READ | (uint)FilesAccessRights.FILE_SHARE_WRITE,
                IntPtr.Zero,
                (uint)FileConsatnts.OPEN_EXISTING,
                (uint)FilesAccessRights.FILE_ATTRIBUTE_NORMAL | (uint)FileFlags.FILE_FLAG_OVERLAPPED,
                IntPtr.Zero);
        }
        else
        {
            deviceHandle = CreateFile(
                devicePath,
                (uint)ACCESSTYPES.GENERIC_WRITE | (uint)ACCESSTYPES.GENERIC_READ,
                (uint)FilesAccessRights.FILE_SHARE_READ | (uint)FilesAccessRights.FILE_SHARE_WRITE,
                IntPtr.Zero,
                (uint)FileConsatnts.OPEN_EXISTING,
                (uint)FilesAccessRights.FILE_ATTRIBUTE_NORMAL | (uint)FileFlags.FILE_FLAG_OVERLAPPED,
                IntPtr.Zero);
        }
        if (deviceHandle.DangerousGetHandle()!=-1)
        {
            bResponse.Status = true;
            bResponse.Data = deviceHandle;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"CreateFile [{devicePath}]";
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetDeviceIoControl<T>(SafeFileHandle fileHandle, [DisallowNull] T structureInput, uint ctlCode, [AllowNull] object? structureOutput=null)
    {
        Win32ResponseDataStruct bResponse = new();
        bool isSuccess;
        uint structureInputSize;
        IntPtr structureInputPtr;
        uint structureOutputSize;
        IntPtr structureOutputPtr;
        uint returnedSize;
        if (structureOutput == null)
        {
            structureInputSize = (uint)Marshal.SizeOf(structureInput);
            structureInputPtr = Marshal.AllocHGlobal((int)structureInputSize);
            Marshal.StructureToPtr(structureInput, structureInputPtr, true);
            structureOutputSize = structureInputSize;
            structureOutputPtr = structureInputPtr;
            isSuccess = DeviceIoControl
                (
                fileHandle,
                ctlCode,
                structureInputPtr,
                structureInputSize,
                structureOutputPtr,
                structureOutputSize,
                out returnedSize,
                nint.Zero
                );
        }
        else
        {

            structureInputSize = (uint)Marshal.SizeOf(structureInput);
            structureInputPtr = Marshal.AllocHGlobal((int)structureInputSize);
            Marshal.StructureToPtr(structureInput, structureInputPtr, true);
            structureOutputSize = (uint)Marshal.SizeOf(structureOutput);
            structureOutputPtr = Marshal.AllocHGlobal((int)structureOutputSize);
            Marshal.StructureToPtr(structureOutput, structureOutputPtr, true);
            isSuccess = DeviceIoControl
                (
                fileHandle,
                ctlCode,
                structureInputPtr,
                structureInputSize,
                structureOutputPtr,
                structureOutputSize,
                out returnedSize,
                nint.Zero
                );
        }
        if (isSuccess)
        {
            if (structureOutput == null)
            {
                T? st = Marshal.PtrToStructure<T>(structureOutputPtr);
                if (st != null)
                {
                    bResponse.Status = true;
                    bResponse.Data = st;
                    bResponse.LengthTransferred = returnedSize;
                }
            }
            else
            {
                var st = Marshal.PtrToStructure(structureOutputPtr, structureOutput.GetType());
                if (st != null)
                {
                    bResponse.Status = true;
                    bResponse.Data = st;
                    bResponse.LengthTransferred = returnedSize;
                }
            }
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
            bResponse.ErrorFunctionName = $"DeviceIoControl [{structureInput.GetType().Name}]";
        }
        return bResponse;
    }
}
