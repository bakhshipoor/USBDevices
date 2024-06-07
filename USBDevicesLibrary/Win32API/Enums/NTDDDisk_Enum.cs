using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.WinIOCtlData;

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDDiskData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntdddisk/

    public enum PARTITION_STYLE : uint
    {
        PARTITION_STYLE_MBR,
        PARTITION_STYLE_GPT,
        PARTITION_STYLE_RAW
    }

    public enum DETECTION_TYPE : uint
    {
        DetectNone,
        DetectInt13,
        DetectExInt13
    }

    public enum MEDIA_TYPE : uint
    {
        Unknown,                // Format is unknown
        F5_1Pt2_512,            // 5.25", 1.2MB,  512 bytes/sector
        F3_1Pt44_512,           // 3.5",  1.44MB, 512 bytes/sector
        F3_2Pt88_512,           // 3.5",  2.88MB, 512 bytes/sector
        F3_20Pt8_512,           // 3.5",  20.8MB, 512 bytes/sector
        F3_720_512,             // 3.5",  720KB,  512 bytes/sector
        F5_360_512,             // 5.25", 360KB,  512 bytes/sector
        F5_320_512,             // 5.25", 320KB,  512 bytes/sector
        F5_320_1024,            // 5.25", 320KB,  1024 bytes/sector
        F5_180_512,             // 5.25", 180KB,  512 bytes/sector
        F5_160_512,             // 5.25", 160KB,  512 bytes/sector
        RemovableMedia,         // Removable media other than floppy
        FixedMedia,             // Fixed hard disk media
        F3_120M_512,            // 3.5", 120M Floppy
        F3_640_512,             // 3.5" ,  640KB,  512 bytes/sector
        F5_640_512,             // 5.25",  640KB,  512 bytes/sector
        F5_720_512,             // 5.25",  720KB,  512 bytes/sector
        F3_1Pt2_512,            // 3.5" ,  1.2Mb,  512 bytes/sector
        F3_1Pt23_1024,          // 3.5" ,  1.23Mb, 1024 bytes/sector
        F5_1Pt23_1024,          // 5.25",  1.23MB, 1024 bytes/sector
        F3_128Mb_512,           // 3.5" MO 128Mb   512 bytes/sector
        F3_230Mb_512,           // 3.5" MO 230Mb   512 bytes/sector
        F8_256_128,             // 8",     256KB,  128 bytes/sector
        F3_200Mb_512,           // 3.5",   200M Floppy (HiFD)
        F3_240M_512,            // 3.5",   240Mb Floppy (HiFD)
        F3_32M_512              // 3.5",   32Mb Floppy
    }

    public enum DISK_IOCTL_Enum
    {
        // 
        IOCTL_DISK_ARE_VOLUMES_READY,
        // In Microsoft Windows 2000 and later operating systems, this IOCTL is replaced by IOCTL_STORAGE_CHECK_VERIFY.
        // The only difference between the two IOCTLs is the base value.
        IOCTL_DISK_CHECK_VERIFY,
        // Retrieves the controller number and disk number for an IDE disk.
        IOCTL_DISK_CONTROLLER_NUMBER,
        // This IOCTL_DISK_COPY_DATA IOCTL is used to copy data from one area of the disk to another.
        IOCTL_DISK_COPY_DATA,
        // Creates an empty partition for the device object. It can operate on either an EFI disk or an MBR disk.
        // The parameters necessary to create an empty disk depend on the type of partition table that will be put onto the disk. For more information, see CREATE_DISK.
        // Disk drivers enumerate partitions as though they were child devices. Thus, upon creating the new partition, the disk class driver notifies
        // the PnP manager by means of a call to IoInvalidateDeviceRelations that the disk device has a new child device(partition).
        // Initializes the specified disk and disk partition table using the information in the CREATE_DISK structure.
        IOCTL_DISK_CREATE_DISK,
        // Removes partition information from the disk. If the partition style of the disk is Master Boot Record (MBR),
        // sector 0 of the disk is wiped clean except for the bootstrap code. All signatures, such as the AA55 boot signature and the NTFT disk signature,
        // will be removed. If the partition style of the disk is GUID Partition Table (GPT), the primary partition table header in sector 1 and the backup partition table in
        // the last sector of the disk are wiped clean. This operation can be used to generate so-called "superfloppies" that contain a file system starting at
        // the first sector of the disk rather than in a partition on the disk.
        IOCTL_DISK_DELETE_DRIVE_LAYOUT,
        // 
        IOCTL_DISK_EJECT_MEDIA,
        // In Microsoft Windows 2000 and later operating systems, this IOCTL is replaced by IOCTL_STORAGE_FIND_NEW_DEVICES.
        // The only difference between the two IOCTLs is the base value.
        IOCTL_DISK_FIND_NEW_DEVICES,
        // 
        IOCTL_DISK_FORMAT_DRIVE,
        // Formats the specified set of contiguous tracks on the disk.
        IOCTL_DISK_FORMAT_TRACKS,
        // Is similar to IOCTL_DISK_FORMAT_TRACKS, except that it allows the caller to specify several more parameters.
        // The additional extended parameters are the format gap length, the number of sectors per track, and an array whose element size is equal to
        // the number of sectors per track. This array represents the track layout.
        IOCTL_DISK_FORMAT_TRACKS_EX,
        // Returns disk cache configuration data.
        IOCTL_DISK_GET_CACHE_INFORMATION,
        // 
        IOCTL_DISK_GET_CACHE_SETTING,
        // 
        IOCTL_DISK_GET_CLUSTER_INFO,
        // Retrieves the attributes of the specified disk device.
        IOCTL_DISK_GET_DISK_ATTRIBUTES,
        // Returns information about the physical disk's geometry (media type, number of cylinders, tracks per cylinder, sectors per track, and bytes per sector).
        IOCTL_DISK_GET_DRIVE_GEOMETRY,
        // Returns information about the physical disk's geometry (media type, number of cylinders, tracks per cylinder, sectors per track, and bytes per sector).
        // The difference between IOCTL_DISK_GET_DRIVE_GEOMETRY_EX and the older IOCTL_DISK_GET_DRIVE_GEOMETRY request is
        // that IOCTL_DISK_GET_DRIVE_GEOMETRY_EX can retrieve information from both Master Boot Record (MBR) and GUID Partition Table(GPT)-type partitioned media,
        // whereas IOCTL_DISK_GET_DRIVE_GEOMETRY can only read MBR-style media.
        IOCTL_DISK_GET_DRIVE_GEOMETRY_EX,
        // Returns information about the number of partitions, disk signature, and features of each partition on a disk. (Floppy drivers need not handle this request.)
        IOCTL_DISK_GET_DRIVE_LAYOUT,
        // Returns information about the number of partitions, disk signature, and features of each partition on a disk. (Floppy drivers need not handle this request.)
        IOCTL_DISK_GET_DRIVE_LAYOUT_EX,
        // Returns the length, in bytes, of the disk, partition, or volume associated with the device object that is the target of the request.
        IOCTL_DISK_GET_LENGTH_INFO,
        // In Microsoft Windows 2000 and later operating systems, this IOCTL is replaced by IOCTL_STORAGE_GET_MEDIA_TYPES.
        // The only difference between the two IOCTLs is the base value.
        IOCTL_DISK_GET_MEDIA_TYPES,
        // 
        IOCTL_DISK_GET_PARTITION_ATTRIBUTES,
        // Returns information about the type, size, and nature of a disk partition. (Floppy drivers need not handle this request.)
        IOCTL_DISK_GET_PARTITION_INFO,
        // Returns information about the type, size, and nature of a disk partition. (Floppy drivers need not handle this request.)
        IOCTL_DISK_GET_PARTITION_INFO_EX,
        // 
        IOCTL_DISK_GET_PERFORMANCE_INFO,
        // 
        IOCTL_DISK_GET_SAN_SETTINGS,
        // 
        IOCTL_DISK_GET_SNAPSHOT_INFO,
        // 
        IOCTL_DISK_GET_WRITE_CACHE_STATE,
        // Increases the size of an existing partition. It is used in conjunction with IOCTL_DISK_UPDATE_DRIVE_SIZE to extend a disk,
        // so that it will contain a new free space area, and then to extend an existing partition on the disk into the newly attached free space.
        // It takes a DISK_GROW_PARTITION structure as the only parameter. For this operation to work, the space after the specified partition must be free.
        // A partition cannot be extended over another existing partition.
        IOCTL_DISK_GROW_PARTITION,
        // 
        IOCTL_DISK_HISTOGRAM_DATA,
        // 
        IOCTL_DISK_HISTOGRAM_RESET,
        // 
        IOCTL_DISK_HISTOGRAM_STRUCTURE,
        // Allows a driver to clear the verify bit on a disk device object, if the mode of the caller is kernel mode.
        IOCTL_DISK_INTERNAL_CLEAR_VERIFY,
        // 
        IOCTL_DISK_INTERNAL_SET_NOTIFY,
        // Allows a driver to set the verify bit on a disk device object if the mode of the caller is kernel mode.
        IOCTL_DISK_INTERNAL_SET_VERIFY,
        // Allows a driver or application to determine if a disk is clustered.
        IOCTL_DISK_IS_CLUSTERED,
        // Determines whether a disk is writable.
        IOCTL_DISK_IS_WRITABLE,
        // 
        IOCTL_DISK_LOAD_MEDIA,
        // 
        IOCTL_DISK_LOGGING,
        // 
        IOCTL_DISK_MEDIA_REMOVAL,
        // Increments a reference counter that enables the collection of disk performance statistics, such as the numbers of bytes read and written since
        // the driver last processed this request, for a corresponding disk monitoring application. In Microsoft Windows 2000 this IOCTL is handled by
        // the filter driver diskperf. In Windows XP and later operating systems, the partition manager handles this request for disks and ftdisk.sys and dmio.sys handle
        // this request for volumes.
        IOCTL_DISK_PERFORMANCE,
        // Disables the counters that were enabled by previous calls to IOCTL_DISK_PERFORMANCE.
        // This request is available in Windows XP and later operating systems. Caller must be running at IRQL = PASSIVE_LEVEL.
        IOCTL_DISK_PERFORMANCE_OFF,
        // Maps defective blocks to new location on disk. This request instructs the device to reassign the bad block address to a good block from its spare-block pool.
        IOCTL_DISK_REASSIGN_BLOCKS,
        // Maps defective blocks to a new location on disk. This request instructs the device to reassign the bad block address to a good block from its spare-block pool.
        // A IOCTL_DISK_REASSIGN_BLOCKS_EX request allows for a larger number of blocks for assignment than the IOCTL_DISK_REASSIGN_BLOCKS request.
        IOCTL_DISK_REASSIGN_BLOCKS_EX,
        // 
        IOCTL_DISK_RELEASE,
        // 
        IOCTL_DISK_REQUEST_DATA,
        // 
        IOCTL_DISK_REQUEST_STRUCTURE,
        // 
        IOCTL_DISK_RESERVE,
        // Clears all volume shadow copy service (VSS) hardware-based snapshot information from the disk.
        // A snapshot is also known as a shadow copy. This request is available in Windows Vista and later versions of the Windows operating systems.
        // The caller must be running at IRQL = PASSIVE_LEVEL.
        IOCTL_DISK_RESET_SNAPSHOT_INFO,
        // 
        IOCTL_DISK_SENSE_DEVICE,
        // Sets disk cache configuration data.
        IOCTL_DISK_SET_CACHE_INFORMATION,
        // 
        IOCTL_DISK_SET_CACHE_SETTING,
        // 
        IOCTL_DISK_SET_CLUSTER_INFO,
        // 
        IOCTL_DISK_SET_DISK_ATTRIBUTES,
        // Repartitions a disk as specified. (Floppy drivers need not handle this request.)
        IOCTL_DISK_SET_DRIVE_LAYOUT,
        // Repartitions a disk as specified. (Floppy drivers need not handle this request.)
        IOCTL_DISK_SET_DRIVE_LAYOUT_EX,
        // 
        IOCTL_DISK_SET_PARTITION_ATTRIBUTES,
        // Changes the partition type of the specified disk partition. (Floppy drivers need not handle this request.)
        IOCTL_DISK_SET_PARTITION_INFO,
        // Changes the partition type of the specified disk partition. (Floppy drivers need not handle this request.)
        IOCTL_DISK_SET_PARTITION_INFO_EX,
        // 
        IOCTL_DISK_SET_SAN_SETTINGS,
        // 
        IOCTL_DISK_SET_SNAPSHOT_INFO,
        // 
        IOCTL_DISK_SIMBAD,
        // Updates device extension with drive size information for current media.
        IOCTL_DISK_UPDATE_DRIVE_SIZE,
        // 
        IOCTL_DISK_UPDATE_PROPERTIES,
        // Performs verification for a specified extent on a disk.
        IOCTL_DISK_VERIFY,
        // 
        IOCTL_DISK_VOLUMES_ARE_READY,
        // 
        OBSOLETE_DISK_GET_WRITE_CACHE_STATE,
        // 
        SMART_GET_VERSION,
        // 
        SMART_RCV_DRIVE_DATA,
        // 
        SMART_RCV_DRIVE_DATA_EX,
        // 
        SMART_SEND_DRIVE_COMMAND,
    }

    public static readonly Dictionary<DISK_IOCTL_Enum, uint> DISK_IOCTL = new Dictionary<DISK_IOCTL_Enum, uint>()
    {
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_DRIVE_GEOMETRY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0000, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_PARTITION_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0001, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_PARTITION_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0002, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_DRIVE_LAYOUT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0003, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_DRIVE_LAYOUT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0004, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_VERIFY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0005, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_FORMAT_TRACKS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0006, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_REASSIGN_BLOCKS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0007, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_PERFORMANCE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0008, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_IS_WRITABLE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0009, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_LOGGING,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X000A, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_FORMAT_TRACKS_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X000B, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_HISTOGRAM_STRUCTURE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X000C, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_HISTOGRAM_DATA,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X000D, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_HISTOGRAM_RESET,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X000E, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_REQUEST_STRUCTURE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X000F, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_REQUEST_DATA,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0010, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_CONTROLLER_NUMBER,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0011, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_PARTITION_INFO_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0012, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_PARTITION_INFO_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0013, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_DRIVE_LAYOUT_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0014, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_DRIVE_LAYOUT_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0015, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_CREATE_DISK,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0016, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_LENGTH_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0017, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_PERFORMANCE_OFF,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0018, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_COPY_DATA,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0019, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.SMART_GET_VERSION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0020, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.SMART_SEND_DRIVE_COMMAND,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0021, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.SMART_RCV_DRIVE_DATA,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0022, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.SMART_RCV_DRIVE_DATA_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0023, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_DRIVE_GEOMETRY_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0028, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_REASSIGN_BLOCKS_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0029, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_UPDATE_DRIVE_SIZE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0032, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GROW_PARTITION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0034, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_CACHE_INFORMATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0035, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_CACHE_INFORMATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0036, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_WRITE_CACHE_STATE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0037, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.OBSOLETE_DISK_GET_WRITE_CACHE_STATE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0037, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_CACHE_SETTING,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0038, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_CACHE_SETTING,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0039, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_PARTITION_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X003A, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_PARTITION_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X003B, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_DISK_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X003C, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_DISK_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X003D, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_IS_CLUSTERED,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X003E, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_DELETE_DRIVE_LAYOUT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0040, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_UPDATE_PROPERTIES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0050, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_SAN_SETTINGS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0080, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_SAN_SETTINGS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0081, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_SNAPSHOT_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0082, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_SNAPSHOT_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0083, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_RESET_SNAPSHOT_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0084, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_CLUSTER_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0085, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SET_CLUSTER_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0086, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_ARE_VOLUMES_READY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0087, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_VOLUMES_ARE_READY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0088, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_PERFORMANCE_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0089, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_FORMAT_DRIVE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X00F3, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SENSE_DEVICE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0X00F8, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_INTERNAL_SET_VERIFY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0100, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_INTERNAL_CLEAR_VERIFY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0101, METHOD_CODES.METHOD_NEITHER, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_INTERNAL_SET_NOTIFY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0102, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_CHECK_VERIFY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0200, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_MEDIA_REMOVAL,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0201, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_EJECT_MEDIA,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0202, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_LOAD_MEDIA,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0203, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_RESERVE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0204, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_RELEASE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0205, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_FIND_NEW_DEVICES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0206, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_GET_MEDIA_TYPES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0300, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            DISK_IOCTL_Enum.IOCTL_DISK_SIMBAD,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_DISK_BASE, 0x0400, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
    };
}
