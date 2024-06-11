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

public static partial class MountMgrData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/mountmgr/

    public enum MOUNTMGR_IOCTL_Enum
    {
        // Upon receiving this IOCTL a client driver must provide the (nonpersistent) device (or target) name for the volume.
        // The mount manager uses the device name returned by the client as the target of a symbolic link. An example of a device name would be "\Device\HarddiskVolume1".
        // Support for this IOCTL by the mount manager clients is mandatory.
        IOCTL_MOUNTDEV_QUERY_DEVICE_NAME,
        // IOCTL_MOUNTMGR_AUTO_DL_ASSIGNMENTS informs the mount manager that it should assign drive letters to volumes automatically as they are introduced in the system.
        IOCTL_MOUNTMGR_AUTO_DL_ASSIGNMENTS,
        // 
        IOCTL_MOUNTMGR_BOOT_DL_ASSIGNMENT,
        // 
        IOCTL_MOUNTMGR_CANCEL_VOLUME_DELETE,
        // Clients send this IOCTL to the mount manager to be informed whenever there is a change in the mount manager's persistent symbolic link name database.
        // The mount manager maintains a counter called EpicNumber that records how many changes have occurred in its persistent name database since the last startup.
        // Clients send a number to the mount manager with every change notification request IRP, and the mount manager responds in the following manner:
        // If the number supplied by the client is not equal to EpicNumber, the mount manager returns STATUS_SUCCESS, indicating that changes have occurred since
        // the client last compared its number with the mount manager's EpicNumber.
        // If the number supplied by the client is equal to EpicNumber, the mount manager interprets this as a request to be informed of the next change to
        // the persistent name database, and it queues the change notification IRP and returns STATUS_PENDING. Whenever a change occurs in the database,
        // the mount manager completes all of the pending change notification IRPs, thereby informing clients of the change.
        // A client that only wants to be informed of the changes to a particular volume is advised to register for Plug and Play target device notification
        // and watch for GUID_IO_VOLUME_NAME_CHANGE.
        IOCTL_MOUNTMGR_CHANGE_NOTIFY,
        // When a volume arrives in the system, it registers for the MOUNTDEV_MOUNTED_DEVICE_GUID interface class and the mount manager receives a Plug and Play notification.
        // When the mount manager receives this notification, it queries the client driver that manages the volume for the volume's unique ID.
        // In some cases, however, particularly with clusters, the client notifies the mount manager of the arrival of its volume,
        // but then does not respond when queried for the volume's unique ID. The mount manager keeps these volumes in a dead mounted device list.
        // Clients can use the IOCTL_MOUNTMGR_CHECK_UNPROCESSED_VOLUMES IOCTL to request that the mount manager rescan its dead mounted device list
        // and make another attempt to query the clients on the list for the unique IDs of their respective volumes.
        // This IOCTL is used primarily for cluster support.
        IOCTL_MOUNTMGR_CHECK_UNPROCESSED_VOLUMES,
        // The mount manager clients can use this IOCTL to request that the mount manager create a persistent symbolic link name for the indicated volume.
        IOCTL_MOUNTMGR_CREATE_POINT,
        // This IOCTL is identical in input and output to IOCTL_MOUNTMGR_QUERY_POINTS.
        // The difference is that IOCTL_MOUNTMGR_DELETE_POINTS has the side effect of deleting the symbolic links and the mount manager database entries for the triples returned.
        // If the input to this IOCTL is ("\DosDevices\X:", NULL, NULL) where X is the current drive letter for the volume indicated in
        // the input triple, the mount manager adds a special entry to its database indicating that the client does not require a drive letter.
        // On subsequent reboots, the mount manager will not assign a default drive letter to the volume.
        IOCTL_MOUNTMGR_DELETE_POINTS,
        // This IOCTL is identical in input and output to IOCTL_MOUNTMGR_QUERY_POINTS.
        // The difference is that IOCTL_MOUNTMGR_DELETE_POINTS_DBONLY has the side effect of deleting the mount manager database entries for the triples returned.
        // However, the mount manager does not delete the symbolic links corresponding to the database entries.
        // If the input to this IOCTL is ("\DosDevices\X:", NULL, NULL), where X is the current drive letter for the volume indicated in the input triple,
        // the mount manager adds a special entry to its database indicating that the client does not require a drive letter.On subsequent reboots,
        // the mount manager will not assign a default drive letter to the volume.
        IOCTL_MOUNTMGR_DELETE_POINTS_DBONLY,
        // This IOCTL directs the mount manager to keep a symbolic link active after the Plug and Play manager has given notification
        // that its corresponding volume has gone offline. When the volume goes back online, the mount manager reassigns the symbolic link to the volume.
        // No other volume is allowed to claim the symbolic link while its original owner is offline.
        // Clusters use this IOCTL to ensure that a node can continue to access a volume with the same drive letter, even if the volume is not continually present in the system.
        IOCTL_MOUNTMGR_KEEP_LINKS_WHEN_OFFLINE,
        // This IOCTL checks to see if the given volume has a drive letter.
        // If it already has a drive letter, or if the volume has a special mount manager database entry indicating that it does not require a drive letter,
        // this routine returns the current drive letter (if there is one) and does nothing.
        // If the given volume does not have a drive letter and does not have a special mount manager database entry indicating that it does not require a drive letter,
        // the next available drive letter is assigned to the volume. If the volume's nonpersistent device name begins with "\Device\Floppy",
        // the mount manager will check for available drive letters starting with the letter "A." If the volume name begins with "\Device\CdRom"
        // the mount manager will check for available drive letters starting with drive letter "D." In all other cases, the mount manager starts with drive letter "C."
        IOCTL_MOUNTMGR_NEXT_DRIVE_LETTER,
        // 
        IOCTL_MOUNTMGR_PREPARE_VOLUME_DELETE,
        // 
        IOCTL_MOUNTMGR_QUERY_AUTO_MOUNT,
        // 
        IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH,
        // 
        IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATHS,
        // This IOCTL returns triples that consist of a persistent symbolic link name for the volume (that is, a mount point), a unique ID for the volume,
        // and a nonpersistent device name (such as "\Device\HarddiskVolume1") for the volume. The input to this IOCTL is a MOUNTMGR_MOUNT_POINT structure
        // that contains a single triple.
        // If the input triple contains a unique ID or a non-persistent device name, the request retrieves all associated mount points (symbolic links),
        // including the volume GUID pathname and the drive letters.However, if the input triple has a symbolic link, but does not specify either the unique ID or
        // the device name, the request only returns a single triple that contains the symbolic link that was provided in the input, together with
        // the unique ID and the device name.The caller must submit another IOCTL with either the unique ID or the device name to retrieve the remaining mount points.
        // If the input triple is empty, the mount manager returns the entire mounted device list.
        // The mount manager returns triples that match as much info as is provided by the caller.
        // If the caller submits the unique ID, the mount manager returns all triples with that unique ID.
        // If the caller inputs the volume pathname or a drive letter as the symbolic link name, the mount manager returns only the triple for the symbolic link.
        // There is one entry per symbolic link.If the caller inputs a device pathname, the mount manager returns only the triples for that device pathname.
        // If the caller inputs a unique ID and a symbolic link, again, the mount manager returns only one entry for that symbolic link.
        // A caller would do this to get the device pathname.If the caller inputs no device pathname, unique ID or symbolic link, the mount manager returns all entries/triples.
        IOCTL_MOUNTMGR_QUERY_POINTS,
        // 
        IOCTL_MOUNTMGR_SCRUB_REGISTRY,
        // 
        IOCTL_MOUNTMGR_SET_AUTO_MOUNT,
        // 
        IOCTL_MOUNTMGR_SILO_ARRIVAL,
        // 
        IOCTL_MOUNTMGR_TRACELOG_CACHE,
        // This IOCTL allows a client to simulate a Plug and Play device interface arrival notification with the given volume name.
        // If a client does not register a device interface of type MOUNTDEV_MOUNTED_DEVICE_GUID, the mount manager is not alerted of its arrival.
        // However, the client can alert the mount manager of its volume's arrival directly by means of this IOCTL.
        // This IOCTL allows clients to obtain drive letters for newly created volumes during text mode setup when the Plug and Play device installer is not running.
        // Clients that have registered a device interface of type MOUNTDEV_MOUNTED_DEVICE_GUID in the normal way should not use this IOCTL.
        IOCTL_MOUNTMGR_VOLUME_ARRIVAL_NOTIFICATION,
        // This IOCTL alerts the mount manager that a volume mount point has been created, so that the mount manager can replicate the database entry for the given mount point.
        // Its primary function is to allow volume mount points to persist even when the volumes are moved from one system to another.
        // The Microsoft Win32 routine SetVolumeMountPoint sends this IOCTL to the mount manager, to inform the mount manager
        // that a newly-created directory junction is pointing to a volume name. The mount manager responds by storing the volume name contained in
        // the directory junction along with its unique ID on the volume hosting the directory junction.
        IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_CREATED,
        // The mount manager clients use this IOCTL to alert the mount manager that a volume mount point has been deleted so
        // that the mount manager can replicate the database entry for the given mount point.
        // The Microsoft Win32 routine DeleteVolumeMountPoint sends this IOCTL to the mount manager,
        // to inform the mount manager that a directory junction is no longer pointing to a volume name.
        // The mount manager responds by deleting the volume name formerly contained in the directory junction along with its unique ID from the volume hosting
        // the directory junction.
        IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_DELETED,


        IOCTL_MOUNTDEV_QUERY_SUGGESTED_LINK_NAME,

    }

    public static readonly Dictionary<MOUNTMGR_IOCTL_Enum, uint> MOUNTMGR_IOCTL = new Dictionary<MOUNTMGR_IOCTL_Enum, uint>()
    {
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_CREATE_POINT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 0, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_DELETE_POINTS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 1, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_QUERY_POINTS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 2, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_DELETE_POINTS_DBONLY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 3, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_NEXT_DRIVE_LETTER,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 4, METHOD_CODES.METHOD_BUFFERED,  FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_AUTO_DL_ASSIGNMENTS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 5, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_CREATED,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 6, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_DELETED,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 7, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_CHANGE_NOTIFY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 8, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_KEEP_LINKS_WHEN_OFFLINE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 9, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_CHECK_UNPROCESSED_VOLUMES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 10, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_VOLUME_ARRIVAL_NOTIFICATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 11, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTDEV_QUERY_DEVICE_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTDEVCONTROLTYPE, 2, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 12, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATHS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 13, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_SCRUB_REGISTRY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 14, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_QUERY_AUTO_MOUNT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 15, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_SET_AUTO_MOUNT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 16, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_BOOT_DL_ASSIGNMENT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 17, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_TRACELOG_CACHE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 18, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_PREPARE_VOLUME_DELETE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 19, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_CANCEL_VOLUME_DELETE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 20, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTMGR_SILO_ARRIVAL,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTMGRCONTROLTYPE, 21, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },

        {
            MOUNTMGR_IOCTL_Enum.IOCTL_MOUNTDEV_QUERY_SUGGESTED_LINK_NAME,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.MOUNTDEVCONTROLTYPE, 3, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
    };
}
