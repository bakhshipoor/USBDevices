using Microsoft.Win32.SafeHandles;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

// https://learn.microsoft.com/en-us/windows/win32/api/
// https://www.geoffchappell.com/studies/windows/win32/kernel32/api/index.htm

namespace USBDevicesLibrary.Win32API;

public static unsafe partial class Kernel32Functions
{
    private const string _DLLName = "kernel32.dll";
    private const bool _LastErrorStatus = true;
    private const CharSet _CharSet = CharSet.Unicode;

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern SafeFileHandle 
        CreateFile
        (
        string lpFileName, 
        uint dwDesiredAccess, 
        uint dwShareMode, 
        IntPtr lpSecurityAttributes, 
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes, 
        IntPtr hTemplateFile
        );

    // Used to read bytes from the serial connection. 
    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        ReadFile
        (
        SafeFileHandle hFile, 
        byte[] lpBuffer, 
        int nNumberOfBytesToRead, 
        out int lpNumberOfBytesRead, 
        int lpOverlapped
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        WriteFile
        (
        SafeFileHandle hFile, 
        byte[] lpBuffer, 
        uint nNumberOfBytesToWrite, 
        out uint lpNumberOfBytesWritten, 
        int lpOverlapped
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern IntPtr 
        LoadLibrary
        (
        string dllToLoad
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern IntPtr 
        GetProcAddress
        (
        IntPtr hModule, 
        string procedureName
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool 
        FreeLibrary
        (
        IntPtr hModule
        );

    [DllImport(dllName: _DLLName, SetLastError = _LastErrorStatus, CharSet = _CharSet)]
    public static extern bool
        DeviceIoControl
        (
        [In] SafeFileHandle hDevice,
        [In] uint dwIoControlCode,
        [In] IntPtr lpInBuffer,
        [In] uint nInBufferSize,
        [Out] IntPtr lpOutBuffer,
        [In] uint nOutBufferSize,
        [Out]out uint lpBytesReturned,
        [In, Out, Optional] IntPtr lpOverlapped
        );

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr CreateIoCompletionPort
        (
        [In] SafeFileHandle hFile,
        IntPtr ExistingCompletionPort,
        UIntPtr CompletionKey,
        uint NumberOfConcurrentThreads
        );

    //[DllImport("kernel32.dll", SetLastError = true)]
    //internal static extern bool GetQueuedCompletionStatus
    //    (
    //    [In] IntPtr hCompletionPort,
    //    [Out] out UInt32 lpNumberOfBytesWritten,
    //    [Out] out IntPtr lpCompletionKey,
    //    [Out] out NativeOverlapped* lpOverlapped,
    //    [In] UInt32 dwMilliseconds
    //    );

    [DllImport(dllName: "ntoskrnl.exe", EntryPoint = "IoGetDeviceObjectPointer")]
    public static extern uint IoGetDeviceObjectPointer
        (
        [In] string ObjectName,
        [In] uint DesiredAccess,
        [Out] IntPtr FileObject,
        [Out] IntPtr DeviceObject
        );
}

