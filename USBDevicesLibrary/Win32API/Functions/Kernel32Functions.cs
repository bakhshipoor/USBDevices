using Microsoft.Win32.SafeHandles;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
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

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool GetVolumeInformation(
            string rootPathName,
            StringBuilder volumeNameBuffer,
            int volumeNameSize,
            out uint volumeSerialNumber,
            out uint maximumComponentLength,
            out uint fileSystemFlags,
            StringBuilder fileSystemNameBuffer,
            int nFileSystemNameSize);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetVolumePathName(
                string volumeName,
                StringBuilder volumePathName,
                uint bufferLength);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.U4)]
    public static extern uint GetShortPathName(
                string volumeLongPath,
                StringBuilder volumeShortPath,
                uint bufferLength);


    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.U4)]
    public static extern uint GetFullPathName(
                string volumeFileNam,
                uint BufferLength,
                [Out] StringBuilder volumeShortPath,
                IntPtr volumeFilePart);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr FindFirstVolumeMountPoint([In] string VolumeName, [Out] StringBuilder lpszVolumeName,
           int cchBufferLength);


    [DllImport("kernel32.dll", SetLastError = false, CharSet = CharSet.Ansi)]
    public static extern int GetLogicalDriveStrings(int nBufferLength,[Out] byte[] lpBuffer);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WIN32_DRV_STR
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
        public byte[] cFileName;
        
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr FindFirstVolume([Out] StringBuilder lpszVolumeName, uint cchBufferLength);


    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool GetVolumeNameForVolumeMountPoint(
       [In] string lpszVolumeMountPoint,
      [Out] StringBuilder lpszVolumeName,
      [In] uint cchBufferLength);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool 
        GetDiskFreeSpace
        (
        string lpRootPathName,
        out uint lpSectorsPerCluster,
        out uint lpBytesPerSector,
        out uint lpNumberOfFreeClusters,
        out uint lpTotalNumberOfClusters
        );

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern uint
        GetDriveType
        (
        string lpRootPathName
        );

}

