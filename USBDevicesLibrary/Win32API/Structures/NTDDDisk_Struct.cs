using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static USBDevicesLibrary.Win32API.WinIOCtlData;

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDDiskData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntdddisk/

    [StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_PARTITION_INFO_UNION
    {
        [FieldOffset(0)]
        public uint MBR_Signature;
        [FieldOffset(4)]
        public uint MBR_CheckSum;
        [FieldOffset(0)]
        public Guid GPT_DiskId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_PARTITION_INFO
    {
        public uint SizeOfPartitionInfo;
        public PARTITION_STYLE PartitionStyle;
        public DISK_PARTITION_INFO_UNION PartitionLayoutInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_INT13_INFO
    {
        public ushort DriveSelect;
        public uint MaxCylinders;
        public ushort SectorsPerTrack;
        public uint MaxHeads;
        public uint NumberDrives;
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
        public uint ExSectorSize;
        public uint ExReserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DISK_DETECTION_INFO
    {
        public uint SizeOfDetectInfo;
        public DETECTION_TYPE DetectionType;
        public DISK_INT13_INFO DiskIny13Info;
        public DISK_EX_INT13_INFO DiskExInt13Info;
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
        public DISK_GEOMETRY Geometry;
        public ulong DiskSize;
        public DISK_PARTITION_INFO PatitionInfo;
        public DISK_DETECTION_INFO DetectionInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DRIVE_LAYOUT_INFORMATION_MBR
    {
        public uint Signature;
        public uint CheckSum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
    public struct PARTITION_INFORMATION
    {
        ulong StartingOffset;
        ulong PartitionLength;
        uint HiddenSectors;
        uint PartitionNumber;
        byte PartitionType;
        byte BootIndicator;
        byte RecognizedPartition;
        byte RewritePartition;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct PARTITION_INFORMATION_MBR
    {
        public byte PartitionType;
        public byte BootIndicator;
        public byte RecognizedPartition;
        public uint HiddenSectors;
        public Guid PartitionId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct PARTITION_INFORMATION_GPT
    {
        public Guid PartitionType;                 // Partition type. See table 16-3.
        public Guid PartitionId;                   // Unique GUID for this partition.
        public ulong Attributes;                 // See table 16-4.
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Name;                    // Partition Name in Unicode.
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DRIVE_LAYOUT_INFORMATION_GPT
    {
        public Guid DiskId;
        public ulong StartingUsableOffset;
        public ulong UsableLength;
        public uint MaxPartitionCount;
    }

    [StructLayout(LayoutKind.Explicit, Size =40)]
    public struct DRIVE_INFORMATION_UNION
    {
        [FieldOffset(0)]
        public uint MBR_Signature;
        [FieldOffset(4)]
        public uint MBR_CheckSum;
        // GPT
        [FieldOffset(0)]
        public Guid GPT_DiskId;
        [FieldOffset(16)]
        public ulong GPT_StartingUsableOffset;
        [FieldOffset(24)]
        public ulong GPT_UsableLength;
        [FieldOffset(32)]
        public ulong GPT_MaxPartitionCount;
    }

    [StructLayout(LayoutKind.Explicit, Size =112)]
    public struct PARTITION_INFORMATION_UNION
    {
        [FieldOffset(0)]
        public PARTITION_TYPE MBR_PartitionType;
        [FieldOffset(2)]
        public byte MBR_BootIndicator;
        [FieldOffset(3)]
        public byte MBR_RecognizedPartition;
        [FieldOffset(4)]
        public uint MBR_HiddenSectors;
        [FieldOffset(8)]
        public Guid MBR_PartitionId;
        
        [FieldOffset(0)]
        public Guid GPT_PartitionType;
        [FieldOffset(16)]
        public Guid GPT_PartitionId;
        [FieldOffset(32)]
        public ulong GPT_Attributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        [FieldOffset(40)]
        public string GPT_Name;
    }


    [StructLayout(LayoutKind.Sequential, Pack =8, Size =144, CharSet = CharSet.Unicode)]
    public struct PARTITION_INFORMATION_EX
    {
        public PARTITION_STYLE PartitionStyle;
        public ulong StartingOffset;
        public ulong PartitionLength;
        public uint PartitionNumber;
        public byte RewritePartition;
        public byte IsServicePartition;
        public PARTITION_INFORMATION_UNION PartitionInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
    public struct DRIVE_LAYOUT_INFORMATION
    {
        public uint PartitionCount;
        public uint Signature;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public PARTITION_INFORMATION[] PartitionEntry;
    }

    [StructLayout(LayoutKind.Sequential, Size =36912, CharSet = CharSet.Unicode)]
    public struct DRIVE_LAYOUT_INFORMATION_EX
    {
        public PARTITION_STYLE PartitionStyle;
        public uint PartitionCount;
        public DRIVE_INFORMATION_UNION DriveInfo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public PARTITION_INFORMATION_EX[] PartitionEntry;
    }
}
