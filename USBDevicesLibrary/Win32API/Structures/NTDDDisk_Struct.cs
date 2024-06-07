using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static USBDevicesLibrary.Win32API.WinIOCtlData;

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDDiskData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntdddisk/

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DRIVE_LAYOUT_INFORMATION_MBR
    {
        public uint Signature;
        public uint CheckSum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_PARTITION_INFO_LAYOUT_GPT
    {
        public Guid DiskId;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_PARTITION_INFO_LAYOUT_UNION
    {
        [FieldOffset(0)]
        public DRIVE_LAYOUT_INFORMATION_MBR LayoutMBR;
        [FieldOffset(0)]
        public DISK_PARTITION_INFO_LAYOUT_GPT LayoutGPT;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_PARTITION_INFO
    {
        public uint SizeOfPartitionInfo;
        public PARTITION_STYLE PartitionStyle;
        public DISK_PARTITION_INFO_LAYOUT_UNION PartitionLayoutInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_INT13_INFO
    {
        public ushort DriveSelect;
        public uint MaxCylinders;
        public ushort SectorsPerTrack;
        public ushort MaxHeads;
        public ushort NumberDrives;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_EX_INT13_INFO
    {
        public ushort ExBufferSize;
        public ushort ExFlags;
        public uint ExCylinders;
        public uint ExHeads;
        public uint ExSectorsPerTrack;
        public ulong ExSectorsPerDrive;
        public ushort ExSectorSize;
        public ushort ExReserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_INT13_EX_INT13_INFO
    {
        public DISK_INT13_INFO DiskInt13Info;
        public DISK_EX_INT13_INFO DiskExtInt13Info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_DETECTION_INFO
    {
        public uint SizeOfDetectInfo;
        DETECTION_TYPE DetectionType;
        public DISK_INT13_EX_INT13_INFO DiskInt13ExInt13;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_GEOMETRY
    {
        public ulong Cylinders;
        public MEDIA_TYPE MediaType;
        public uint TracksPerCylinder;
        public uint SectorsPerTrack;
        public uint BytesPerSector;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_GEOMETRY_EX
    {
        DISK_GEOMETRY Geometry;
        ulong DiskSize;
        DISK_PARTITION_INFO PatitionInfo;
        DISK_DETECTION_INFO DetectionInfo;
    }
}
