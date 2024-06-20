using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

// https://learn.microsoft.com/en-us/windows/win32/api/winusb/

namespace USBDevicesLibrary.Win32API;

public static unsafe partial class WinUSBFunctions
{
    private const string _DLLName = "winusb.dll";
    private const bool _LastErrorStatus = true;
    private const CharSet _CharSet = CharSet.Unicode;

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool
        WinUsb_ControlTransfer
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] WinUSBData.WINUSB_SETUP_PACKET SetupPacket,
        [Out] byte[] Buffer,
        [In] uint BufferLength,
        [Out, Optional] out uint LengthTransferred,
        [In, Optional] IntPtr Overlapped
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, EntryPoint = "WinUsb_GetAssociatedInterface", CharSet = _CharSet)]
    private static extern bool
        WinUsb_GetAssociatedInterface
        (
        [In] IntPtr InterfaceHandle,
        [In] byte AssociatedInterfaceIndex,
        [Out] out IntPtr AssociatedInterfaceHandle
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        WinUsb_GetDescriptor
        (
        [In] IntPtr InterfaceHandle,
        [In] byte DescriptorType,
        [In] byte Index,
        [In] ushort LanguageID,
        [Out] out USBSpec.USB_DEVICE_DESCRIPTOR deviceDesc,
        [In] uint BufferLength,
        [Out] out uint LengthTransfered
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_GetDescriptor
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte DescriptorType,
        [In] byte Index,
        [In] ushort LanguageID,
        [Out] out USBSpec.USB_CONFIGURATION_DESCRIPTOR deviceDesc,
        [In] uint BufferLength,
        [Out] out uint LengthTransfered
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_GetDescriptor
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte DescriptorType,
        [In] byte Index,
        [In] ushort LanguageID,
        [Out] out USBSpec.USB_STRING_DESCRIPTOR deviceDesc,
        [In] uint BufferLength,
        [Out] out uint LengthTransfered
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        WinUsb_Free
        (
        [In] IntPtr InterfaceHandle
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_Initialize
        (
        [In] SafeFileHandle DeviceHandle,
        out IntPtr InterfaceHandle
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_QueryDeviceInformation
        (
        [In] IntPtr InterfaceHandle,
        [In] uint InformationType,
        [In, Out] ref uint BufferLength,
        [Out] out byte Buffer
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        WinUsb_QueryInterfaceSettings
        (
        [In] IntPtr InterfaceHandle,
        [In] byte AlternateInterfaceNumber,
        [Out] out USBSpec.USB_INTERFACE_DESCRIPTOR UsbAltInterfaceDescriptor
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_QueryPipe
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte AlternateInterfaceNumber,
        [In] byte PipeIndex,
        [Out] out WinUSBIO.WINUSB_PIPE_INFORMATION PipeInformation
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_ReadPipe
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte PipeID,
        [Out] byte[] Buffer,
        [In] uint BufferLength,
        [Out, Optional] out uint LengthTransferred,
        [In, Optional] IntPtr Overlapped
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_SetPipePolicy
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte PipeID,
        [In] uint PolicyType,
        [In] uint ValueLength,
        [In] in byte Value
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_SetPipePolicy
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte PipeID,
        [In] uint PolicyType,
        [In] uint ValueLength,
        [In] in uint Value
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_GetPipePolicy
        (
        [In] SafeFileHandle InterfaceHandle, 
        [In] byte PipeID, 
        [In] uint PolicyType,
        [In, Out] ref uint ValueLength,
        [Out] out byte Value
        );
    
    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_GetPipePolicy
        (
        [In] SafeFileHandle InterfaceHandle, 
        [In] byte PipeID, 
        [In] uint PolicyType,
        [In, Out] ref uint ValueLength, 
        [Out] out uint Value
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_WritePipe
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte PipeID,
        [In] byte[] Buffer,
        [In] uint BufferLength,
        [Out, Optional] out uint LengthTransferred,
        [In, Optional] IntPtr Overlapped
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_ResetPipe
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte PipeID
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_FlushPipe
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte PipeID
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    private static extern bool 
        WinUsb_AbortPipe
        (
        [In] SafeFileHandle InterfaceHandle,
        [In] byte PipeID
        );
}
