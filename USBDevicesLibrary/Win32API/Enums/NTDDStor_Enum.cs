using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Data;
using USBDevicesLibrary.Devices;
using static USBDevicesLibrary.Win32API.WinIOCtlData;
using System.Dynamic;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Diagnostics.Metrics;
using System.Windows.Media;
using System.Windows;

namespace USBDevicesLibrary.Win32API;

public static partial class NTDDStorData
{
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntddstor/
    public enum STORAGE_IOCTL_Enum
    {
        // 
        IOCTL_STORAGE_ALLOCATE_BC_STREAM,
        // 
        IOCTL_STORAGE_ATTRIBUTE_MANAGEMENT,
        // Breaks a disk reservation. In a multi-initiator ("clustered") system, a single computer can reserve a disk resource, so that no other computer can access the disk.
        // If the computer does not or cannot free the resource in a timely fashion, a means is needed to remove the reservation of the disk by force.
        // One means of forcing the system to free a reserved disk resource is to reset the bus. In fact, for storage devices whose bus adapters are managed by the SCSI port driver,
        // the IOCTL_STORAGE_BREAK_RESERVATION request is equivalent to IOCTL_STORAGE_RESET_BUS, which simply does a reset of the bus, freeing all of its reserved resources.
        // For storage devices whose bus adapters are managed by the STOR port driver, this I/O control code offers a better technique of breaking a disk reservation.
        // That technique is called a "hierarchical reset." Upon receiving an IOCTL_STORAGE_BREAK_RESERVATION request, the STOR port driver first attempts to remove
        // the reserve on the logical unit by resetting the logical unit itself. If that fails, the STOR port driver attempts to reset the target device that is the parent of the logical unit.
        // Only when resetting the target device fails will the port driver reset the bus.
        // Resetting the bus clears all device reservations and transfer speed settings, which must then be renegotiated.
        // Because this is a time-consuming operation, IOCTL_STORAGE_BREAK_RESERVATION is always to be preferred to a simple bus reset.
        // The caller requires only read access to issue a bus reset.
        // The SrbStatus flag of pending SRBs is set to SRB_STATUS_BUS_RESET.
        IOCTL_STORAGE_BREAK_RESERVATION,
        // 
        IOCTL_STORAGE_CHECK_PRIORITY_HINT_SUPPORT,
        // Determines whether the media has changed on a removable-media device that the caller has opened for read or write access.
        // If read or write access to the device is not necessary, the caller can improve performance by opening the device with FILE_READ_ATTRIBUTES and issuing anIOCTL_STORAGE_CHECK_VERIFY2 request instead.
        // For more information, see Supporting Removable Media. https://learn.microsoft.com/en-us/windows-hardware/drivers/kernel/supporting-removable-media
        IOCTL_STORAGE_CHECK_VERIFY,
        // Determines whether the media has changed on a removable-media device - the caller has opened with FILE_READ_ATTRIBUTES.
        // Because no file system is mounted when a device is opened in this way, this request can be processed much more quickly than an IOCTL_STORAGE_CHECK_VERIFY request.
        IOCTL_STORAGE_CHECK_VERIFY2,
        // A driver can use IOCTL_STORAGE_DEVICE_POWER_CAP to specify a maximum operational power consumption level for a storage device. The OS will do its best to transition the device
        // to a power state that will not exceed the given maximum; however, this depends on what the device supports. The actual maximum may be less than or greater than the desired maximum.
        IOCTL_STORAGE_DEVICE_POWER_CAP,
        // 
        IOCTL_STORAGE_DEVICE_TELEMETRY_NOTIFY,
        // 
        IOCTL_STORAGE_DEVICE_TELEMETRY_QUERY_CAPS,
        // IOCTL_STORAGE_DIAGNOSTIC is used to query diagnostic data from the storage driver stack.
        IOCTL_STORAGE_DIAGNOSTIC,
        // Causes the device to eject the media if the device supports ejection capabilities.
        IOCTL_STORAGE_EJECT_MEDIA,
        // Locks the device to prevent removal of the media. If the driver can prevent the media from being removed while the drive is in use, the driver disables or enables the mechanism that ejects media,
        // thereby locking the drive. A caller must open the device with FILE_READ_ATTRIBUTES to send this request.
        // Unlike IOCTL_STORAGE_MEDIA_REMOVAL, the driver tracks IOCTL_STORAGE_EJECTION_CONTROL requests by caller and ignores unlock requests for which it has not received a lock request from the same caller,
        // thereby preventing other callers from unlocking the drive.
        // A driver for a removable-media device - can support this IOCTL must do the following:
        // 1. Keep a lock count, tagged by caller, in the device object extension.
        // 2. Keep the lock count per physical device.
        // 3. When called with this IOCTL, if the flag to prevent removing the media is set, increment the count; if the flag is clear and the driver has previously received a lock request from the same caller, decrement the count.
        // 4. Prevent removal of the media unless all lock counts are zero.
        // Under normal circumstances, the caller who locked the device using IOCTL_STORAGE_EJECTION_CONTROL, unlocks the device by sending IOCTL_STORAGE_EJECTION_CONTROL again
        // with Irp->AssociatedIrp.SystemBuffer set to a boolean value of FALSE.However, sometimes the caller fails to unlock the device properly.
        // To ensure that media removal locks are released properly, the class driver keeps track of callers who lock the media with IOCTL_STORAGE_EJECTION_CONTROL.
        // If the caller terminates without unlocking the device, the class driver unlocks the device.
        IOCTL_STORAGE_EJECTION_CONTROL,
        // 
        IOCTL_STORAGE_ENABLE_IDLE_POWER,
        // 
        IOCTL_STORAGE_EVENT_NOTIFICATION,
        // 
        IOCTL_STORAGE_FAILURE_PREDICTION_CONFIG,
        // Determines whether another device that the driver supports has been connected to the I/O bus, either since the system was booted or since the driver last processed this request.
        // This IOCTL is obsolete in the Plug and Play environment. Plug and Play class drivers handle this request by calling IoInvalidateDeviceRelations with the device relations type BusRelations.
        // If a new device is found, the class driver's AddDevice routine will be called.
        // Legacy class drivers can continue to handle this IOCTL without modifications.If a new device is found, the driver sets up any necessary system objects and resources to handle I/O requests for its new device.
        // It also initializes the device on receipt of this request dynamically, that is, without requiring the machine to be rebooted.Such a driver is assumed to support devices connected on a dynamically configurable I/O bus.
        IOCTL_STORAGE_FIND_NEW_DEVICES,
        // A driver can use IOCTL_STORAGE_FIRMWARE_ACTIVATE to activate a firmware image on a storage device.
        IOCTL_STORAGE_FIRMWARE_ACTIVATE,
        // A driver can use IOCTL_STORAGE_FIRMWARE_DOWNLOAD to download a firmware image to a storage device, but does not activate it. If the image to be downloaded is larger than the controller’s maximum data transfer size,
        // this IOCTL will have to be called multiple times until the entire image is downloaded.
        IOCTL_STORAGE_FIRMWARE_DOWNLOAD,
        // A driver can use IOCTL_STORAGE_FIRMWARE_GET_INFO to query a storage device for detailed firmware information.
        // A successful call will return information about firmware revisions, activity status, as well as read/write attributes for each slot. The amount of data returned will vary based on storage protocol.
        IOCTL_STORAGE_FIRMWARE_GET_INFO,
        // 
        IOCTL_STORAGE_FREE_BC_STREAM,
        // 
        IOCTL_STORAGE_GET_BC_PROPERTIES,
        // 
        IOCTL_STORAGE_GET_COUNTERS,
        // A driver can use IOCTL_STORAGE_GET_DEVICE_INTERNAL_LOG to get the internal status data from the device.
        IOCTL_STORAGE_GET_DEVICE_INTERNAL_LOG,
        // Returns a STORAGE_DEVICE_NUMBER structure that contains the FILE_DEVICE_XXX type, device number, and, for a partitionable device,
        // the partition number assigned to a device by the driver when the device is started. This request is usually issued by a fault-tolerant disk driver.
        IOCTL_STORAGE_GET_DEVICE_NUMBER,
        // Returns a STORAGE_DEVICE_NUMBER_EX structure that contains the FILE_DEVICE_XXX type, device number, device GUID, and, for a partitionable device, the partition number assigned to a device by
        // the driver when the device is started. This request is usually issued by a fault-tolerant disk driver.
        IOCTL_STORAGE_GET_DEVICE_NUMBER_EX,
        // 
        IOCTL_STORAGE_GET_DEVICE_TELEMETRY,
        // 
        IOCTL_STORAGE_GET_DEVICE_TELEMETRY_RAW,
        // Retrieves the hotplug configuration of the specified device.
        IOCTL_STORAGE_GET_HOTPLUG_INFO,
        // 
        IOCTL_STORAGE_GET_IDLE_POWERUP_REASON,
        // The IOCTL_STORAGE_GET_LB_PROVISIONING_MAP_RESOURCES request is sent to the storage class driver to determine available and used mapping resources on a storage device.
        IOCTL_STORAGE_GET_LB_PROVISIONING_MAP_RESOURCES,
        // Queries the USB generic parent driver for the serial number of a USB device. If a USB device has a CSM-1 content security interface, a USB client driver can query for its serial number using this request.
        // USB client drivers that help implement a digital rights management (DRM) system can use this information to ensure that only legitimate customers have access to digitized intellectual property.
        IOCTL_STORAGE_GET_MEDIA_SERIAL_NUMBER,
        // Returns information about the geometry of floppy drives.
        IOCTL_STORAGE_GET_MEDIA_TYPES,
        // Returns information about the types of media supported by a device. A storage class driver must handle this IOCTL to control devices to be accessed by
        // the removable storage manager (RSM) either as stand-alone devices or as data transfer elements (drives) in a media library or changer device.
        IOCTL_STORAGE_GET_MEDIA_TYPES_EX,
        // The IOCTL_STORAGE_GET_PHYSICAL_ELEMENT_STATUS control code queries for and returns the physical element status from a device.
        IOCTL_STORAGE_GET_PHYSICAL_ELEMENT_STATUS,
        // Causes media to be loaded in a device that the caller has opened for read or write access. If read or write access to the device is not necessary,
        // the caller can improve performance by opening the device with FILE_READ_ATTRIBUTES and issuing an IOCTL_STORAGE_LOAD_MEDIA2 request instead.
        IOCTL_STORAGE_LOAD_MEDIA,
        // Causes media to be loaded in a device that the caller has opened with FILE_READ_ATTRIBUTES. Because no file system is mounted when a device is opened in this way,
        // this request can be processed much more quickly than an IOCTL_STORAGE_LOAD_MEDIA request.
        // Input, output, and I/O status block values for IOCTL_STORAGE_LOAD_MEDIA2 are identical to those for IOCTL_STORAGE_LOAD_MEDIA.
        IOCTL_STORAGE_LOAD_MEDIA2,
        // The IOCTL_STORAGE_MANAGE_BYPASS_IO control code controls BypassIO operations on a given file in all layers of the volume and storage stacks.
        IOCTL_STORAGE_MANAGE_BYPASS_IO,
        // This IOCTL_STORAGE_MANAGE_DATA_SET_ATTRIBUTES request is used to send a data set management request to a storage device.
        IOCTL_STORAGE_MANAGE_DATA_SET_ATTRIBUTES,
        // Temporarily enables or disables delivery of the custom PnP events GUID_IO_MEDIA_ARRIVAL and GUID_IO_MEDIA_REMOVAL on a removable-media device.
        // This, in turn, enables or disables media change detection (AutoPlay) for the device if the caller has opened the device with FILE_READ_ATTRIBUTES access and if the device has AutoPlay enabled in the registry.
        // The caller must not open the device for read or write access or the IOCTL operation will fail. This IOCTL has no effect on the AutoPlay setting in the registry.
        // A driver for such a removable-media device must do the following:
        // 1. Keep a count of disable requests, per physical device, in the device object extension.
        // 1. When called with this IOCTL, if the flag to disable media change detection is set, increment the count; if the flag is clear, decrement the count.
        // 3. Set the media change event for the device when the media state is changed only if the disable request count is zero.
        // When the IRP_MJ_DEVICE_CONTROL IRP that contains this IOCTL is passed to the SCSI class driver, the FileObject member of the current IO_STACK_LOCATION must point to a valid file object.
        // The SCSI class driver requires a file object for cases where a user-mode application that is disabling or enabling AutoPlay terminates unexpectedly.In such cases, the SCSI class driver uses
        // the file object to reenable media change detection.Because the file object is necessary for proper clean-up, the SCSI class driver will cause the IRP to fail with an error message
        // of STATUS_INVALID_PARAMETER if the FileObject member of IO_STACK_LOCATION does not point to a valid file object. If a user-mode application opens the device, then the I/O manager initializes this member,
        // but kernel-mode driver writers should not assume that FileObject will be properly initialized when the IRP is generated by a user-mode application.
        // If, for instance, a user-mode application mistakenly opens the device for either read or write access before sending the IOCTL, then the device control IRP will be routed through the file system,
        // preventing the SCSI class driver and the device driver from directly accessing the device's file object.
        IOCTL_STORAGE_MCN_CONTROL,
        // Locks the device to prevent removal of the media. If the driver can prevent the media from being removed while the drive is in use,
        // it disables or enables the mechanism that ejects media on a device - the caller has opened for read or write access.
        // Unlike IOCTL_STORAGE_EJECTION_CONTROL, for which the driver tracks requests by caller, the driver ignores IOCTL_STORAGE_MEDIA_REMOVAL unlock requests
        // only if its lock count is already zero, thereby allowing any caller to unlock the drive.
        // A driver for such a removable-media device that can support this IOCTL must do the following:
        // 1. Keep a lock count in the device object extension.
        // 2. Keep the lock count per physical device.
        // 3. When called with this IOCTL, if the flag to prevent removing the media is set, increment the count; if the flag is clear, decrement the count.
        // 4. Prevent removal of the media unless all lock counts are zero.
        IOCTL_STORAGE_MEDIA_REMOVAL,
        // The generic storage class driver (classpnp.sys) exposes an I/O control (IOCTL) interface for issuing Persistent Reserve In commands.
        // The behavior of the storage device when a Persistent Reserve In command is received is described in the SCSI Primary Commands - 2 (SPC-2) specification.
        // The IOCTL interface requires the caller to have read access to the physical device for Persistent Reserve In commands.
        // User-mode applications, services, and kernel-mode drivers can use this IOCTL to control persistent reservations.
        // If called from a driver, this IOCTL must be called from a thread running at IRQL < DISPATCH_LEVEL.
        // This IOCTL is defined with FILE_READ_ACCESS, requiring a device handle to have read permissions to issue the Persistent Reserve In command.
        IOCTL_STORAGE_PERSISTENT_RESERVE_IN,
        // The generic storage class driver (classpnp.sys) exposes an I/O control (IOCTL) interface for issuing Persistent Reserve Out commands.
        // The behavior of the storage device when a Persistent Reserve Out command is received is described in the SCSI Primary Commands - 2 (SPC-2) specification.
        // The IOCTL interface requires the caller to have read/write access to the physical device for Persistent Reserve Out commands.
        // User-mode applications, services, and kernel-mode drivers can use this IOCTL to control persistent reservations.
        // If called from a driver, this IOCTL must be called from a thread running at IRQL < DISPATCH_LEVEL.
        // This IOCTL is defined with FILE_READ_ACCESS and FILE_WRITE_ACCESS, requiring a device handle to have both read and write permissions to issue the Persistent Reserve Out command.
        IOCTL_STORAGE_PERSISTENT_RESERVE_OUT,
        // 
        IOCTL_STORAGE_POWER_ACTIVE,
        // 
        IOCTL_STORAGE_POWER_IDLE,
        // Polls for a prediction of device failure. This request works with the IDE disk drives that support self-monitoring analysis and reporting technology (SMART).
        // If the drive is a SCSI drive, the class driver attempts to verify if the SCSI disk supports the equivalent IDE SMART technology by check the inquiry information on the Information Exception Control Page, X3T10/94-190 Rev 4.
        // If the device supports prediction failure, the disk class driver queries the device for failure prediction status and reports the results.
        // If the disk class driver assigns a nonzero value to the PredictFailure member of STORAGE_PREDICT_FAILURE in the output buffer at Irp->AssociatedIrp.SystemBuffer,
        // the disk has bad sectors and is predicting a failure.The storage stack returns 512 bytes of vendor-specific information about the failure prediction in the VendorSpecific member of STORAGE_PREDICT_FAILURE.
        // If the PredictFailure member contains a value of zero, the disk is not predicting a failure.
        // If the device does not support failure prediction, IOCTL_STORAGE_PREDICT_FAILURE fails with a status of STATUS_INVALID_DEVICE_REQUEST, and the data in the output buffer is undefined
        // Other means of checking for disk failure include monitoring the event log and registering to receive a WMI event with WMI_STORAGE_PREDICT_FAILURE_EVENT_GUID.
        IOCTL_STORAGE_PREDICT_FAILURE,
        // A driver can use IOCTL_STORAGE_PROTOCOL_COMMAND to pass vendor-specific commands to a storage device.
        IOCTL_STORAGE_PROTOCOL_COMMAND,
        // A driver can use IOCTL_STORAGE_QUERY_PROPERTY to return properties of a storage device or adapter.
        // The request indicates the kind of information to retrieve, such as inquiry data for a device or capabilities and limitations of an adapter.
        // IOCTL_STORAGE_QUERY_PROPERTY can also be used to determine whether the port driver supports a particular property or which fields in the property descriptor can be modified with a subsequent change-property request.
        IOCTL_STORAGE_QUERY_PROPERTY,
        // The IOCTL_STORAGE_READ_CAPACITY request returns the read capacity information for the target storage device.
        IOCTL_STORAGE_READ_CAPACITY,
        // A driver can issue a IOCTL_STORAGE_REINITIALIZE_MEDIA control code to offload the erasure process to the storage device.
        IOCTL_STORAGE_REINITIALIZE_MEDIA,
        // Releases a device previously reserved for the exclusive use of the caller on a bus that supports multiple initiators and the concept of reserving a device, such as a SCSI bus.
        IOCTL_STORAGE_RELEASE,
        // 
        IOCTL_STORAGE_REMOVE_ELEMENT_AND_TRUNCATE,
        // Claims a device for the exclusive use of the caller on a bus that supports multiple initiators and the concept of reserving a device, such as a SCSI bus.
        IOCTL_STORAGE_RESERVE,
        // Resets an I/O bus and, indirectly, each device on the bus. Resetting the bus clears all device reservations and transfer speed settings, which must then be renegotiated,
        // making it a time-consuming operation that should be used very rarely. The caller requires only read access to issue a bus reset.
        // The SrbStatus flag of pending SRBs is set to SRB_STATUS_BUS_RESET.
        IOCTL_STORAGE_RESET_BUS,
        // If possible, resets a non-SCSI storage device without affecting other devices on the bus. Device reset for SCSI devices is not supported.
        // The caller requires only read access to issue a device reset and, to comply, the device must be capable of responding to I/O requests.
        // If the device reset succeeds, pending I/O requests are canceled.
        IOCTL_STORAGE_RESET_DEVICE,
        // 
        IOCTL_STORAGE_RPMB_COMMAND,
        // Sets the hotplug configuration of the specified device. This request takes a STORAGE_HOTPLUG_INFO structure as input.
        // The DeviceHotplug member of the STORAGE_HOTPLUG_INFO structure determines what action is taken.
        // If the value of that member is nonzero, the value for the device's removal policy in the registry is set to ExpectSurpriseRemoval and all levels of caching are disabled.
        // If the value of DeviceHotplug is zero, the removal policy is set to ExpectOrderlyRemoval, and caching might be selectively enabled.
        IOCTL_STORAGE_SET_HOTPLUG_INFO,
        // A driver can use IOCTL_STORAGE_SET_PROPERTY to set the properties of a storage device or adapter.
        // Note :
        // Due to colliding IOCTL definitions in Windows 10 version 2004, existing software that uses IOCTL_STORAGE_SET_PROPERTY will need to be recompiled on Windows Server 2022.
        IOCTL_STORAGE_SET_PROPERTY,
        // A driver can use IOCTL_STORAGE_SET_TEMPERATURE_THRESHOLD to set the temperature threshold of a storage device (when supported by the hardware).
        // Use IOCTL_STORAGE_QUERY_PROPERTY to determine if the device supports changing the over and under temperature thresholds.
        IOCTL_STORAGE_SET_TEMPERATURE_THRESHOLD,
        // 
        IOCTL_STORAGE_START_DATA_INTEGRITY_CHECK,
        // 
        IOCTL_STORAGE_STOP_DATA_INTEGRITY_CHECK,
        // 
        OBSOLETE_IOCTL_STORAGE_RESET_BUS,
        // 
        OBSOLETE_IOCTL_STORAGE_RESET_DEVICE,
    }
public static readonly Dictionary<STORAGE_IOCTL_Enum, uint> STORAGE_IOCTL = new Dictionary<STORAGE_IOCTL_Enum, uint>()
{
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_CHECK_VERIFY,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0200, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_CHECK_VERIFY2,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0200, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_MEDIA_REMOVAL,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0201, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_EJECT_MEDIA,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0202, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_LOAD_MEDIA,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0203, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_LOAD_MEDIA2,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0203, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_RESERVE,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0204, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_RELEASE,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0205, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_FIND_NEW_DEVICES,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0206, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_MANAGE_BYPASS_IO,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0230, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_EJECTION_CONTROL,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0250, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_MCN_CONTROL,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0251, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_MEDIA_TYPES,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0300, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_MEDIA_TYPES_EX,
        WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0301, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
    },
    {
        STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_MEDIA_SERIAL_NUMBER,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0304, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_HOTPLUG_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0305, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_SET_HOTPLUG_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0306, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_RESET_BUS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0400, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.OBSOLETE_IOCTL_STORAGE_RESET_BUS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0400, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_RESET_DEVICE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0401, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.OBSOLETE_IOCTL_STORAGE_RESET_DEVICE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0401, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_BREAK_RESERVATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0405, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_PERSISTENT_RESERVE_IN,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0406, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_PERSISTENT_RESERVE_OUT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0407, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_DEVICE_NUMBER,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0420, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_DEVICE_NUMBER_EX,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0421, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_PREDICT_FAILURE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0440, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_FAILURE_PREDICTION_CONFIG,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0441, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_COUNTERS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0442, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_READ_CAPACITY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0450, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_DEVICE_TELEMETRY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0470, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_DEVICE_TELEMETRY_NOTIFY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0471, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_DEVICE_TELEMETRY_QUERY_CAPS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0472, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_DEVICE_TELEMETRY_RAW,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0473, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_SET_TEMPERATURE_THRESHOLD,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0480, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_PROTOCOL_COMMAND,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x04F0, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_SET_PROPERTY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x04FF, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_QUERY_PROPERTY,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0500, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_MANAGE_DATA_SET_ATTRIBUTES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0501, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_LB_PROVISIONING_MAP_RESOURCES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0502, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_REINITIALIZE_MEDIA,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0590, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_BC_PROPERTIES,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0600, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_ALLOCATE_BC_STREAM,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0601, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_FREE_BC_STREAM,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0602, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_CHECK_PRIORITY_HINT_SUPPORT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0620, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_START_DATA_INTEGRITY_CHECK,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0621, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_STOP_DATA_INTEGRITY_CHECK,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0622, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_FIRMWARE_GET_INFO,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0700, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_FIRMWARE_DOWNLOAD,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0701, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_FIRMWARE_ACTIVATE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0702, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_ENABLE_IDLE_POWER,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0720, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_IDLE_POWERUP_REASON,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0721, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_POWER_ACTIVE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0722, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_POWER_IDLE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0723, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_EVENT_NOTIFICATION,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0724, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_DEVICE_POWER_CAP,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0725, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_RPMB_COMMAND,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0726, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_ATTRIBUTE_MANAGEMENT,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0727, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_READ_ACCESS | FILE_ACCESS.FILE_WRITE_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_DIAGNOSTIC,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0728, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_PHYSICAL_ELEMENT_STATUS,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0729, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_REMOVE_ELEMENT_AND_TRUNCATE,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0730, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
        {
            STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_DEVICE_INTERNAL_LOG,
            WinIOCtlFunctions.CTL_CODE( DEVICE_TYPES.IOCTL_STORAGE_BASE, 0x0731, METHOD_CODES.METHOD_BUFFERED, FILE_ACCESS.FILE_ANY_ACCESS)
        },
    };
}
