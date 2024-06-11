using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Effects;
using System.Windows.Media;
using static USBDevicesLibrary.Win32API.WinIOCtlData;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows;
using System.Xml.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.CompilerServices;

namespace USBDevicesLibrary.Win32API;

public static partial class MountMgrData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/mountmgr/

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct MOUNTMGR_DRIVE_LETTER_TARGET
    {
        public ushort DeviceNameLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst =255)]
        public string DeviceName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
    public struct MOUNTMGR_DRIVE_LETTER_INFORMATION
    {
        public byte DriveLetterWasAssigned;
        public char CurrentDriveLetter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
    public struct MOUNTDEV_NAME
    {
        public ushort NameLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Name;
    }

    // Input structure for IOCTL_MOUNTMGR_KEEP_LINKS_WHEN_OFFLINE,
    // IOCTL_MOUNTMGR_VOLUME_ARRIVAL_NOTIFICATION,
    // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH, and
    // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATHS.
    // IOCTL_MOUNTMGR_PREPARE_VOLUME_DELETE
    // IOCTL_MOUNTMGR_CANCEL_VOLUME_DELETE
    [StructLayout(LayoutKind.Sequential, Size =512, CharSet = CharSet.Unicode)]
    public struct MOUNTMGR_TARGET_NAME
    {
        public ushort DeviceNameLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string DeviceName;
    }

    // Output structure for IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH and
    // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATHS.
    [StructLayout(LayoutKind.Sequential, Size =204, CharSet = CharSet.Unicode)]
    public struct MOUNTMGR_VOLUME_PATHS
    {
        public uint MultiSzLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string MultiSz;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
    public struct MOUNTDEV_SUGGESTED_LINK_NAME
    {
        public byte UseOnlyIfThereAreNoOtherLinks;
        public ushort NameLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 250)]
        public string Name;
    }

    public struct MOUNTMGR_MOUNT_POINT
    {
        public uint SymbolicLinkNameOffset;
        public ushort SymbolicLinkNameLength;
        public ushort Reserved1;
        public uint UniqueIdOffset;
        public ushort UniqueIdLength;
        public ushort Reserved2;
        public uint DeviceNameOffset;
        public ushort DeviceNameLength;
        public ushort Reserved3;
    }

}
