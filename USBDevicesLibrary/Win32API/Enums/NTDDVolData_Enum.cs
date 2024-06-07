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

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDVolData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntddvol/
    public enum VOLUME_IOCTL_Enum
    {
        // 
        IOCTL_VOLUME_ALLOCATE_BC_STREAM,
        // 
        IOCTL_VOLUME_FREE_BC_STREAM,
        // 
        IOCTL_VOLUME_GET_BC_PROPERTIES,
        // 
        IOCTL_VOLUME_GET_CSVBLOCKCACHE_CALLBACK,
        // 
        IOCTL_VOLUME_GET_GPT_ATTRIBUTES,
        // Returns the physical location(s) of a volume on one or more disks. Extents are initially stored in the order in which they are created,
        // but remirroring, splitting, or breaking a mirror, or actions taken during disaster recovery, can affect the order of disk extents.
        IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS,
        // Allows a driver or application to determine if a volume is clustered.
        IOCTL_VOLUME_IS_CLUSTERED,
        // 
        IOCTL_VOLUME_IS_CSV,
        // 
        IOCTL_VOLUME_IS_DYNAMIC,
        // 
        IOCTL_VOLUME_IS_IO_CAPABLE,
        // 
        IOCTL_VOLUME_IS_OFFLINE,
        // 
        IOCTL_VOLUME_IS_PARTITION,
        // Returns physical offsets and physical disk numbers for a given volume logical offset.
        // For example, a logical volume offset inside a mirrored volume with two plex corresponds to two physical offsets,
        // one in each of the two disks participating in the mirror. In response to this IOCTL, the volume manager returns two physical offsets
        // and two physical disk numbers for the logical volume offset.
        // The volume manager supports this IOCTL for all types of basic and dynamic volumes.
        IOCTL_VOLUME_LOGICAL_TO_PHYSICAL,
        // The IOCTL_VOLUME_OFFLINE IOCTL puts the volume in an OFFLINE state, which is a state where read and write operations will fail.
        // The requests will not be passed down to the physical disk until a subsequent IOCTL_VOLUME_ONLINE is received.
        // A common use for IOCTL_VOLUME_OFFLINE is a case in which one application or driver wants to prevent a volume from being remounted
        // by a call to open the volume from a second application or driver while the volume is in the process of being removed by the first application or driver.
        // For example, before masking a Logical Unit Number (LUN), the volumes on the LUN should be locked(optional), dismounted, taken offline, and uninstalled.
        // Now the LUN can be masked without causing Plug and Play(PnP) surprise removal events to be logged for the volumes and the disk itself.
        // Without the call to take the volume offline, after the handle that is used to dismount it is closed, the volume can then be remounted
        // by a call to open it from another application or driver, if it occurred before the call to uninstall the volume.A call to open a volume succeeds
        // on an offline volume, but I/O directed toward an offline volume fails.Taking a volume offline has no effect on disk I/O
        // (if the application or driver opened a handle to the disk), but stops volume I/O (if the application or driver opened a handle to the volume).
        IOCTL_VOLUME_OFFLINE,
        // The IOCTL_VOLUME_ONLINE IOCTL puts the volume in an ONLINE state, which is a state where read and write operations will be executed.
        // The requests are passed down to the physical disk until a subsequent IOCTL_VOLUME_OFFLINE is received.
        // A common use for IOCTL_VOLUME_ONLINE is a case in which the mount manager automatically puts a new volume in the ONLINE state when the volume arrives,
        // unless that volume is listed in a registry key that is populated by the cluster service.IOCTL_VOLUME_ONLINE is called for removable drives regardless of
        // the NoAutoMount setting in the following registry key:
        // HKCU\System\CurrentControlSet\Services\Mountmgr\NoAutoMount
        // But for volumes controlled by NoAutoMount, assigning a drive letter will cause IOCTL_VOLUME_ONLINE to be called.
        // For volumes that are controlled by the cluster service, IOCTL_VOLUME_ONLINE is sent by the cluster service when the local node owns the volume.
        // The cluster service uses both IOCTL_VOLUME_ONLINE and IOCTL_VOLUME_ONLINE to allow I/O to a disk volume when the disk volume is owned by the local server.
        // Until the cluster service puts the disk volume in an ONLINE state, no I/O is permitted to the disk volume.
        // This prevents disk volume corruption that could result from multiple cluster nodes writing to the same disk volume simultaneously.
        IOCTL_VOLUME_ONLINE,
        // Returns the logical offset corresponding to a physical disk number and a physical offset.
        // The volume manager supports this IOCTL as described for all types of basic and dynamic volumes.
        IOCTL_VOLUME_PHYSICAL_TO_LOGICAL,
        // 
        IOCTL_VOLUME_POST_ONLINE,
        // 
        IOCTL_VOLUME_PREPARE_FOR_CRITICAL_IO,
        // 
        IOCTL_VOLUME_PREPARE_FOR_SHRINK,
        // 
        IOCTL_VOLUME_QUERY_ALLOCATION_HINT,
        // 
        IOCTL_VOLUME_QUERY_FAILOVER_SET,
        // 
        IOCTL_VOLUME_QUERY_MINIMUM_SHRINK_SIZE,
        // 
        IOCTL_VOLUME_QUERY_VOLUME_NUMBER,
        // Performs a read on a specific plex of a volume. Because all plexes are identical, the volume manager can retrieve data from any of a volume's plexes during
        // a normal read operation. The volume manager spreads reads among a volume's plexes, to balance the I/O load on the physical media and to maximize read performance.
        // If, however, an application or kernel-mode component must read data from a particular plex instead of letting the volume manager pick one, it can use this IOCTL to do so.
        IOCTL_VOLUME_READ_PLEX,
        // 
        IOCTL_VOLUME_SET_GPT_ATTRIBUTES,
        // 
        IOCTL_VOLUME_SUPPORTS_ONLINE_OFFLINE,
        // 
        IOCTL_VOLUME_UPDATE_PROPERTIES,
    }

    public static readonly Dictionary<VOLUME_IOCTL_Enum, uint> VOLUME_IOCTL = new Dictionary<VOLUME_IOCTL_Enum, uint>()
    {
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 0, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_SUPPORTS_ONLINE_OFFLINE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 1, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_ONLINE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 2, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_OFFLINE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 3, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_IS_OFFLINE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 4, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_IS_IO_CAPABLE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 5, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_QUERY_FAILOVER_SET,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 6, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_QUERY_VOLUME_NUMBER,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 7, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_LOGICAL_TO_PHYSICAL,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 8, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_PHYSICAL_TO_LOGICAL,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 9, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_IS_PARTITION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 10, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_READ_PLEX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 11, METHOD_CODES.METHOD_OUT_DIRECT, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_IS_CLUSTERED,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 12, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_SET_GPT_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 13, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_GET_GPT_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 14, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_GET_BC_PROPERTIES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 15, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_ALLOCATE_BC_STREAM,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 16, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_FREE_BC_STREAM,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 17, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_IS_DYNAMIC,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 18, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_PREPARE_FOR_CRITICAL_IO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 19, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_QUERY_ALLOCATION_HINT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 20, METHOD_CODES.METHOD_OUT_DIRECT, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
        VOLUME_IOCTL_Enum.IOCTL_VOLUME_UPDATE_PROPERTIES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 21, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_QUERY_MINIMUM_SHRINK_SIZE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 22, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_PREPARE_FOR_SHRINK,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 23, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_IS_CSV,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 24, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_POST_ONLINE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 25, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            VOLUME_IOCTL_Enum.IOCTL_VOLUME_GET_CSVBLOCKCACHE_CALLBACK,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_VOLUME_BASE, 26, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
    };
}
