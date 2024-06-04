using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Win32API;

public static class ClassesGUID
{
    //Defines GUIDs for device classes used in Plug & Play.
    public enum GUID_DEVCLASS
    {
        GUID_DEVCLASS_1394,
        GUID_DEVCLASS_1394DEBUG,
        GUID_DEVCLASS_61883,
        GUID_DEVCLASS_ADAPTER,
        GUID_DEVCLASS_APMSUPPORT,
        GUID_DEVCLASS_AVC,
        GUID_DEVCLASS_BATTERY,
        GUID_DEVCLASS_BIOMETRIC,
        GUID_DEVCLASS_BLUETOOTH,
        GUID_DEVCLASS_CAMERA,
        GUID_DEVCLASS_CDROM,
        GUID_DEVCLASS_COMPUTEACCELERATOR,
        GUID_DEVCLASS_COMPUTER,
        GUID_DEVCLASS_DECODER,
        GUID_DEVCLASS_DISKDRIVE,
        GUID_DEVCLASS_DISPLAY,
        GUID_DEVCLASS_DOT4,
        GUID_DEVCLASS_DOT4PRINT,
        GUID_DEVCLASS_EHSTORAGESILO,
        GUID_DEVCLASS_ENUM1394,
        GUID_DEVCLASS_EXTENSION,
        GUID_DEVCLASS_FDC,
        GUID_DEVCLASS_FIRMWARE,
        GUID_DEVCLASS_FLOPPYDISK,
        GUID_DEVCLASS_GENERIC,
        GUID_DEVCLASS_GPS,
        GUID_DEVCLASS_HDC,
        GUID_DEVCLASS_HIDCLASS,
        GUID_DEVCLASS_HOLOGRAPHIC,
        GUID_DEVCLASS_IMAGE,
        GUID_DEVCLASS_INFINIBAND,
        GUID_DEVCLASS_INFRARED,
        GUID_DEVCLASS_KEYBOARD,
        GUID_DEVCLASS_LEGACYDRIVER,
        GUID_DEVCLASS_MEDIA,
        GUID_DEVCLASS_MEDIUM_CHANGER,
        GUID_DEVCLASS_MEMORY,
        GUID_DEVCLASS_MODEM,
        GUID_DEVCLASS_MONITOR,
        GUID_DEVCLASS_MOUSE,
        GUID_DEVCLASS_MTD,
        GUID_DEVCLASS_MULTIFUNCTION,
        GUID_DEVCLASS_MULTIPORTSERIAL,
        GUID_DEVCLASS_NET,
        GUID_DEVCLASS_NETCLIENT,
        GUID_DEVCLASS_NETDRIVER,
        GUID_DEVCLASS_NETSERVICE,
        GUID_DEVCLASS_NETTRANS,
        GUID_DEVCLASS_NETUIO,
        GUID_DEVCLASS_NODRIVER,
        GUID_DEVCLASS_PCMCIA,
        GUID_DEVCLASS_PNPPRINTERS,
        GUID_DEVCLASS_PORTS,
        GUID_DEVCLASS_PRIMITIVE,
        GUID_DEVCLASS_PRINTER,
        GUID_DEVCLASS_PRINTERUPGRADE,
        GUID_DEVCLASS_PRINTQUEUE,
        GUID_DEVCLASS_PROCESSOR,
        GUID_DEVCLASS_SBP2,
        GUID_DEVCLASS_SCMDISK,
        GUID_DEVCLASS_SCMVOLUME,
        GUID_DEVCLASS_SCSIADAPTER,
        GUID_DEVCLASS_SECURITYACCELERATOR,
        GUID_DEVCLASS_SENSOR,
        GUID_DEVCLASS_SIDESHOW,
        GUID_DEVCLASS_SMARTCARDREADER,
        GUID_DEVCLASS_SMRDISK,
        GUID_DEVCLASS_SMRVOLUME,
        GUID_DEVCLASS_SOFTWARECOMPONENT,
        GUID_DEVCLASS_SOUND,
        GUID_DEVCLASS_SYSTEM,
        GUID_DEVCLASS_TAPEDRIVE,
        GUID_DEVCLASS_UNKNOWN,
        GUID_DEVCLASS_UCM,
        GUID_DEVCLASS_USB,
        GUID_DEVCLASS_VOLUME,
        GUID_DEVCLASS_VOLUMESNAPSHOT,
        GUID_DEVCLASS_WCEUSBS,
        GUID_DEVCLASS_WPD,
        // Define filesystem filter classes used for classification and load ordering.
        // Classes are listed below in order from "highest" (i.e., farthest from the
        // filesystem) to "lowest" (i.e., closest to the filesystem).
        GUID_DEVCLASS_FSFILTER_TOP,
        GUID_DEVCLASS_FSFILTER_ACTIVITYMONITOR,
        GUID_DEVCLASS_FSFILTER_UNDELETE,
        GUID_DEVCLASS_FSFILTER_ANTIVIRUS,
        GUID_DEVCLASS_FSFILTER_REPLICATION,
        GUID_DEVCLASS_FSFILTER_CONTINUOUSBACKUP,
        GUID_DEVCLASS_FSFILTER_CONTENTSCREENER,
        GUID_DEVCLASS_FSFILTER_QUOTAMANAGEMENT,
        GUID_DEVCLASS_FSFILTER_SYSTEMRECOVERY,
        GUID_DEVCLASS_FSFILTER_CFSMETADATASERVER,
        GUID_DEVCLASS_FSFILTER_HSM,
        GUID_DEVCLASS_FSFILTER_COMPRESSION,
        GUID_DEVCLASS_FSFILTER_ENCRYPTION,
        GUID_DEVCLASS_FSFILTER_VIRTUALIZATION,
        GUID_DEVCLASS_FSFILTER_PHYSICALQUOTAMANAGEMENT,
        GUID_DEVCLASS_FSFILTER_OPENFILEBACKUP,
        GUID_DEVCLASS_FSFILTER_SECURITYENHANCER,
        GUID_DEVCLASS_FSFILTER_COPYPROTECTION,
        GUID_DEVCLASS_FSFILTER_BOTTOM,
        GUID_DEVCLASS_FSFILTER_SYSTEM,
        GUID_DEVCLASS_FSFILTER_INFRASTRUCTURE,
        // DiskClassGuid is an obsolete identifier for the device interface
        // class for hard disk storage devices. Starting with Microsoft Windows 2000
        GUID_DEVINTERFACE_DISK,
        GUID_DEVINTERFACE_CDROM,
        GUID_DEVINTERFACE_PARTITION,
        GUID_DEVINTERFACE_TAPE,
        GUID_DEVINTERFACE_WRITEONCEDISK,
        GUID_DEVINTERFACE_VOLUME,
        GUID_DEVINTERFACE_MEDIUMCHANGER,
        GUID_DEVINTERFACE_FLOPPY,
        GUID_DEVINTERFACE_CDCHANGER,
        GUID_DEVINTERFACE_STORAGEPORT,
        GUID_DEVINTERFACE_VMLUN,
        GUID_DEVINTERFACE_SES,
        GUID_DEVINTERFACE_ZNSDISK,
        WDI_STORAGE_PREDICT_FAILURE_DPS_GUID,
        // Interfaces to discover devices that are
        // not reported  through conventional APIs
        GUID_DEVINTERFACE_SERVICE_VOLUME,
        GUID_DEVINTERFACE_HIDDEN_VOLUME,
        // Interface to register for RPMB commands
        GUID_DEVINTERFACE_UNIFIED_ACCESS_RPMB,
        // GUID, identifying crash dump section, containing device information or driver information.
        // These GUIDs are used both to identify secondary dump section in the crashdump
        // and in IOCTL to identify why section is requested by the user mode application
        GUID_DEVICEDUMP_STORAGE_DEVICE,
        GUID_DEVICEDUMP_DRIVER_STORAGE_PORT,
        // The GUID_AVC_CLASS device interface class is defined for audio video control (AV/C) devices that are supported by the AVStream architecture.
        GUID_AVC_CLASS,
        // The GUID_BTHPORT_DEVICE_INTERFACE device interface class is defined for Bluetooth radios.
        GUID_BTHPORT_DEVICE_INTERFACE,
        // GUID_CLASS_COMPORT is an obsolete identifier for the device interface class for devices that support a 16550 UART-compatible hardware interface.
        // Use the GUID_DEVINTERFACE_COMPORT class identifier for new instances of this class.
        GUID_DEVINTERFACE_COMPORT,
        GUID_DEVINTERFACE_SERENUM_BUS_ENUMERATOR,
        // GUID_CLASS_INPUT is an obsolete identifier for the device interface class for HID collections.
        // Starting with Microsoft Windows 2000, use the GUID_DEVINTERFACE_HID class identifier for new instances of this class.
        GUID_DEVINTERFACE_HID,
        GUID_HID_INTERFACE_NOTIFY,
        GUID_HID_INTERFACE_HIDPARSE,
        // The GUID_DEVICE_APPLICATIONLAUNCH_BUTTON device interface class is defined for Advanced Configuration and Power Interface (ACPI) application start buttons.
        GUID_DEVICE_APPLICATIONLAUNCH_BUTTON,
        // The GUID_DEVICE_BATTERY device interface class is defined for battery devices.
        GUID_DEVICE_BATTERY,
        // The GUID_DEVICE_LID device interface class is defined for Advanced Configuration and Power Interface (ACPI) lid devices.
        GUID_DEVICE_LID,
        // The GUID_DEVICE_MEMORY device interface class is defined for Advanced Configuration and Power Interface (ACPI) memory devices.
        GUID_DEVICE_MEMORY,
        // The GUID_DEVICE_MESSAGE_INDICATOR device interface class is defined for Advanced Configuration and Power Interface (ACPI) message indicator devices.
        GUID_DEVICE_MESSAGE_INDICATOR,
        // The GUID_DEVICE_PROCESSOR device interface class is defined for Advanced Configuration and Power Interface (ACPI) processor devices.
        GUID_DEVICE_PROCESSOR,
        // The GUID_DEVICE_SYS_BUTTON device interface class is defined for Advanced Configuration and Power Interface (ACPI) system power button devices.
        GUID_DEVICE_SYS_BUTTON,
        // The GUID_DEVICE_THERMAL_ZONE device interface class is defined for Advanced Configuration and Power Interface (ACPI) thermal zone devices.
        GUID_DEVICE_THERMAL_ZONE,
        // The GUID_DEVINTERFACE_BRIGHTNESS device interface class is defined for display adapter drivers that operate in the context of the
        // Windows Vista Display Driver Model and support brightness control of monitor child devices.
        GUID_DEVINTERFACE_BRIGHTNESS,
        // The GUID_DEVINTERFACE_DISPLAY_ADAPTER device interface class is defined for display views that are supported by display adapters.
        GUID_DEVINTERFACE_DISPLAY_ADAPTER,
        // The GUID_DEVINTERFACE_I2C device interface class is defined for display adapter drivers that operate in the context of the
        // Windows Vista Display Driver Model and perform I2C transactions with monitor child devices.
        GUID_DEVINTERFACE_I2C,
        // The GUID_DEVINTERFACE_IMAGE device interface class is defined for WIA devices and Still Image (STI) devices, including digital cameras and scanners.
        GUID_DEVINTERFACE_IMAGE,
        // The GUID_DEVINTERFACE_KEYBOARD device interface class is defined for keyboard devices.
        GUID_DEVINTERFACE_KEYBOARD,
        // The GUID_DEVINTERFACE_MODEM device interface class is defined for modem devices.
        GUID_DEVINTERFACE_MODEM,
        // The GUID_DEVINTERFACE_MONITOR device interface class is defined for monitor devices.
        GUID_DEVINTERFACE_MONITOR,
        // The GUID_DEVINTERFACE_MOUSE device interface class is defined for mouse devices.
        GUID_DEVINTERFACE_MOUSE,
        // The GUID_DEVINTERFACE_NET device interface class is defined for network devices.
        GUID_DEVINTERFACE_NET,
        // The GUID_DEVINTERFACE_OPM device interface class is defined for display adapter drivers that operate in the context of the
        // Windows Vista Display Driver Model and support output protection management (OPM) for monitor child devices.
        GUID_DEVINTERFACE_OPM,
        // The GUID_DEVINTERFACE_PARALLEL device interface class is defined for parallel ports that support an IEEE 1284-compatible hardware interface.
        GUID_DEVINTERFACE_PARALLEL,
        // The GUID_DEVINTERFACE_PARCLASS device interface class is defined for devices that are attached to a parallel port.
        GUID_DEVINTERFACE_PARCLASS,
        // The GUID_DEVINTERFACE_SIDESHOW device interface class is defined for Windows SideShow devices.
        GUID_DEVINTERFACE_SIDESHOW,
        // The GUID_DEVINTERFACE_USB_DEVICE device interface class is defined for USB devices that are attached to a USB hub.
        GUID_DEVINTERFACE_USB_DEVICE,
        // The GUID_DEVINTERFACE_USB_HOST_CONTROLLER device interface class is defined for USB host controller devices.
        GUID_DEVINTERFACE_USB_HOST_CONTROLLER,
        // The GUID_DEVINTERFACE_USB_HUB device interface class is defined for USB hub devices.
        GUID_DEVINTERFACE_USB_HUB,
        GUID_DEVINTERFACE_USB_BILLBOARD,
        GUID_USB_WMI_STD_DATA,
        GUID_USB_WMI_STD_NOTIFICATION,
        GUID_USB_WMI_DEVICE_PERF_INFO,
        GUID_USB_WMI_NODE_INFO,
        GUID_USB_WMI_TRACING,
        GUID_USB_TRANSFER_TRACING,
        GUID_USB_PERFORMANCE_TRACING,
        GUID_USB_WMI_SURPRISE_REMOVAL_NOTIFICATION,
        GUID_DEVINTERFACE_WINUSB,
        ANDROID_USB_CLASS_ID,
        // The GUID_DEVINTERFACE_VIDEO_OUTPUT_ARRIVAL device interface class is defined for child devices of display devices.
        GUID_DEVINTERFACE_VIDEO_OUTPUT_ARRIVAL,
        // The GUID_DEVINTERFACE_WPD device interface class is defined for Windows Portable Devices (WPD).
        GUID_DEVINTERFACE_WPD,
        GUID_DEVINTERFACE_WPD_AUDIO,
        // The GUID_DEVINTERFACE_WPD_PRIVATE device interface class is defined for specialized Windows Portable Devices (WPD).
        GUID_DEVINTERFACE_WPD_PRIVATE,
        // The GUID_DISPLAY_DEVICE_ARRIVAL device interface class is defined for display adapters.
        GUID_DISPLAY_DEVICE_ARRIVAL,
        // The GUID_IO_VOLUME_DEVICE_INTERFACE device interface class is defined for volume devices.
        GUID_IO_VOLUME_DEVICE_INTERFACE,
        // The GUID_VIRTUAL_AVC_CLASS device interface class is defined for virtual audio video control (AV/C) devices that are supported by the AVStream architecture.
        GUID_VIRTUAL_AVC_CLASS,
        // The KSCATEGORY_ACOUSTIC_ECHO_CANCEL device interface class is defined for the kernel streaming (KS) functional category that performs acoustic echo cancellation.
        KSCATEGORY_ACOUSTIC_ECHO_CANCEL,
        // The KSCATEGORY_AUDIO device interface class is defined for the kernel streaming (KS) functional category for an audio device.
        KSCATEGORY_AUDIO,
        // The KSCATEGORY_AUDIO_DEVICE device interface class represents a kernel streaming (KS) functional category that is reserved for exclusive use by the system-supplied WDM audio components.
        KSCATEGORY_AUDIO_DEVICE,
        // The KSCATEGORY_AUDIO_GFX device interface class is defined for the kernel streaming (KS) functional category that supports a global effects (GFX) filter.
        KSCATEGORY_AUDIO_GFX,
        // The KSCATEGORY_AUDIO_SPLITTER device interface class is designed for a kernel streaming (KS) functional category that is reserved for exclusive use by the system-supplied WDM audio components.
        KSCATEGORY_AUDIO_SPLITTER,
        // The KSCATEGORY_BDA_IP_SINK device interface class is defined for the kernel streaming (KS) functional category for a sink filter in the broadcast driver architecture (BDA).
        KSCATEGORY_BDA_IP_SINK,
        // The KSCATEGORY_BDA_NETWORK_EPG device interface class is defined for the kernel streaming (KS) functional category for an electronic program guide (EPG) in the broadcast driver architecture (BDA).
        KSCATEGORY_BDA_NETWORK_EPG,
        // The KSCATEGORY_BDA_NETWORK_PROVIDER device interface class is defined for the kernel streaming (KS) functional category for a network provider in the broadcast driver architecture (BDA).
        KSCATEGORY_BDA_NETWORK_PROVIDER,
        // The KSCATEGORY_BDA_NETWORK_TUNER device interface class is defined for the kernel streaming (KS) functional category for a network tuner in the broadcast driver architecture (BDA).
        KSCATEGORY_BDA_NETWORK_TUNER,
        // The KSCATEGORY_BDA_RECEIVER_COMPONENT device interface class is defined for the kernel streaming (KS) functional category for a receiver in the broadcast driver architecture (BDA).
        KSCATEGORY_BDA_RECEIVER_COMPONENT,
        // The KSCATEGORY_BDA_TRANSPORT_INFORMATION device interface class is defined for the kernel streaming (KS) functional category for a transport information filter (TIF) in the broadcast driver architecture (BDA).
        KSCATEGORY_BDA_TRANSPORT_INFORMATION,
        // The KSCATEGORY_BRIDGE device interface class is defined for the kernel streaming (KS) functional category that supports a software interface between the KS subsystem and another software component.
        KSCATEGORY_BRIDGE,
        // The KSCATEGORY_CAPTURE device interface class is defined for the kernel streaming (KS) functional category that captures wave or MIDI data streams.
        KSCATEGORY_CAPTURE,
        // The KSCATEGORY_CLOCK device interface class is defined for the kernel streaming (KS) functional category for a clock device.
        KSCATEGORY_CLOCK,
        // The KSCATEGORY_COMMUNICATIONSTRANSFORM device interface class is defined for the kernel streaming (KS) functional category for a communication transform device.
        KSCATEGORY_COMMUNICATIONSTRANSFORM,
        // The KSCATEGORY_CROSSBAR device interface class is defined for the kernel streaming (KS) functional category for a crossbar device that routes video and audio streams.
        KSCATEGORY_CROSSBAR,
        // The KSCATEGORY_DATACOMPRESSOR device interface class is defined for the kernel streaming (KS) functional category that compresses a data stream.
        KSCATEGORY_DATACOMPRESSOR,
        // The KSCATEGORY_DATADECOMPRESSOR device interface class is defined for the kernel streaming (KS) functional category that decompresses a data stream.
        KSCATEGORY_DATADECOMPRESSOR,
        // The KSCATEGORY_DATATRANSFORM device interface class is defined for the kernel streaming (KS) functional category that transforms audio data streams.
        KSCATEGORY_DATATRANSFORM,
        // The KSCATEGORY_DRM_DESCRAMBLE device interface class is defined for the kernel streaming (KS) functional category that unscrambles a DRM-protected wave stream.
        KSCATEGORY_DRM_DESCRAMBLE,
        // The KSCATEGORY_ENCODER device interface class is defined for the kernel streaming (KS) functional category that encodes data.
        KSCATEGORY_ENCODER,
        // The KSCATEGORY_ESCALANTE_PLATFORM_DRIVER device interface class is obsolete. Do not use for new devices.
        KSCATEGORY_ESCALANTE_PLATFORM_DRIVER,
        // The KSCATEGORY_FILESYSTEM device interface class is defined for the kernel streaming (KS) functional category that moves a data stream into or out of the file system.
        KSCATEGORY_FILESYSTEM,
        // The KSCATEGORY_INTERFACETRANSFORM device interface class is defined for the kernel streaming (KS) functional category that transforms the interface of a device.
        KSCATEGORY_INTERFACETRANSFORM,
        // The KSCATEGORY_MEDIUMTRANSFORM device interface class is defined for the kernel streaming (KS) functional category that transforms the type of medium that is being used.
        KSCATEGORY_MEDIUMTRANSFORM,
        // The KSCATEGORY_MICROPHONE_ARRAY_PROCESSOR device interface class is defined for the kernel streaming (KS) functional category that processes input from a microphone array.
        KSCATEGORY_MICROPHONE_ARRAY_PROCESSOR,
        // The KSCATEGORY_MIXER device interface class is defined for the kernel streaming (KS) functional category that mixes data streams.
        KSCATEGORY_MIXER,
        // The KSCATEGORY_MULTIPLEXER device interface class is defined for the kernel streaming (KS) functional category for a multiplexer device.
        KSCATEGORY_MULTIPLEXER,
        // The KSCATEGORY_NETWORK device interface class is defined for the kernel streaming (KS) functional category for a network device.
        KSCATEGORY_NETWORK,
        // The KSCATEGORY_NETWORK_CAMERA GUID is defined as follows:
        KSCATEGORY_NETWORK_CAMERA,
        // The KSCATEGORY_PREFERRED_MIDIOUT_DEVICE device interface class is defined for the kernel streaming (KS) functional category for the preferred MIDI output device.
        KSCATEGORY_PREFERRED_MIDIOUT_DEVICE, //= KSCATEGORY_PREFERRED_WAVEOUT_DEVICE,
        // The KSCATEGORY_PREFERRED_WAVEIN_DEVICE device interface class is defined for the kernel streaming (KS) functional category for the preferred wave input device.
        KSCATEGORY_PREFERRED_WAVEIN_DEVICE,
        // The KSCATEGORY_PREFERRED_WAVEIN_DEVICE device interface class is defined for the kernel streaming (KS) functional category for the preferred wave input device.
        KSCATEGORY_PREFERRED_WAVEOUT_DEVICE,
        // The KSCATEGORY_PROXY device interface class represents a kernel streaming (KS) functional category that is reserved for exclusive use by the kernel streaming proxy module.
        KSCATEGORY_PROXY,
        // The KSCATEGORY_QUALITY device interface class is defined for the kernel streaming (KS) functional category for quality management.
        KSCATEGORY_QUALITY,
        // The KSCATEGORY_REALTIME device interface class is defined for the kernel streaming (KS) functional category for an audio device that is connected to a
        // system bus (for example, the PCI bus) and that plays back or captures wave data in real time.
        KSCATEGORY_REALTIME,
        // The KSCATEGORY_RENDER device interface class is defined for the kernel streaming (KS) functional category that renders wave and MIDI data streams.
        KSCATEGORY_RENDER,
        // he KSCATEGORY_SENSOR_CAMERA GUID is defined as follows:
        KSCATEGORY_SENSOR_CAMERA,
        // The KSCATEGORY_SPLITTER device interface class is defined for the kernel streaming (KS) functional category that splits a data stream.
        KSCATEGORY_SPLITTER,
        // The KSCATEGORY_SYNTHESIZER device interface class is defined for the kernel streaming (KS) functional category that converts MIDI data to either wave audio samples or an analog output signal.
        KSCATEGORY_SYNTHESIZER,
        // The KSCATEGORY_SYSAUDIO device interface class represents a kernel streaming (KS) functional category that is reserved for exclusive use by the system-supplied WDM audio components.
        KSCATEGORY_SYSAUDIO,
        // The KSCATEGORY_TEXT device interface class is defined for the kernel streaming (KS) functional category that supports text data.
        KSCATEGORY_TEXT,
        // The KSCATEGORY_TOPOLOGY device interface class is defined for the kernel streaming (KS) functional category for the internal topology of an audio device.
        KSCATEGORY_TOPOLOGY,
        // The KSCATEGORY_TVAUDIO device interface class is defined for the kernel streaming (KS) functional category for a TV audio device.
        KSCATEGORY_TVAUDIO,
        // The KSCATEGORY_TVTUNER device interface class is defined for the kernel streaming (KS) functional category for a TV tuner device.
        KSCATEGORY_TVTUNER,
        // The KSCATEGORY_VBICODEC device interface class is defined for the kernel streaming (KS) functional category for a video blanking interval (VBI) codec device.
        KSCATEGORY_VBICODEC,
        // The KSCATEGORY_VIDEO device interface class is defined for the kernel streaming (KS) functional category for a video device.
        KSCATEGORY_VIDEO,
        // The KSCATEGORY_VIDEO_CAMERA GUID is defined as follows:
        KSCATEGORY_VIDEO_CAMERA,
        // The KSCATEGORY_VIRTUAL device interface class represents a kernel streaming (KS) category that is reserved for exclusive use by the system-supplied WDM audio components.
        KSCATEGORY_VIRTUAL,
        // The KSCATEGORY_VPMUX device interface class is defined for the kernel streaming (KS) functional category that supports video multiplexing.
        KSCATEGORY_VPMUX,
        // The KSCATEGORY_WDMAUD device interface class represents a kernel streaming (KS) functional category that is reserved for exclusive use by the system-supplied WDM audio components.
        KSCATEGORY_WDMAUD,
        // The KSMFT_CATEGORY_AUDIO_DECODER device interface class is defined for the Kernel Streaming (KS) functional category for an audio device.
        KSMFT_CATEGORY_AUDIO_DECODER,
        // The KSMFT_CATEGORY_AUDIO_EFFECT device interface class is defined for the Kernel Streaming (KS) functional category for an audio device.
        KSMFT_CATEGORY_AUDIO_EFFECT,
        // The KSMFT_CATEGORY_AUDIO_ENCODER device interface class is defined for the Kernel Streaming (KS) functional category for an audio device.
        KSMFT_CATEGORY_AUDIO_ENCODER,
        // The KSMFT_CATEGORY_DEMULTIPLEXER device interface class is defined for the Kernel Streaming (KS) functional category for a device that separates (demultiplexes) media streams.
        KSMFT_CATEGORY_DEMULTIPLEXER,
        // The KSMFT_CATEGORY_MULTIPLEXER device interface class is defined for the Kernel Streaming (KS) functional category for a device that combines (multiplexes) media streams.
        KSMFT_CATEGORY_MULTIPLEXER,
        // The KSMFT_CATEGORY_OTHER device interface class is defined for the Kernel Streaming (KS) functional category for a device that does not belong in other KS functional categories.
        KSMFT_CATEGORY_OTHER,
        // The KSMFT_CATEGORY_VIDEO_DECODER device interface class is defined for the Kernel Streaming (KS) functional category for a video device.
        KSMFT_CATEGORY_VIDEO_DECODER,
        // The KSMFT_CATEGORY_VIDEO_EFFECT device interface class is defined for the Kernel Streaming (KS) functional category for a video device.
        KSMFT_CATEGORY_VIDEO_EFFECT,
        // The KSMFT_CATEGORY_VIDEO_ENCODER device interface class is defined for the Kernel Streaming (KS) functional category for a video device.
        KSMFT_CATEGORY_VIDEO_ENCODER,
        // The KSMFT_CATEGORY_VIDEO_PROCESSOR device interface class is defined for the Kernel Streaming (KS) functional category for a video device.
        KSMFT_CATEGORY_VIDEO_PROCESSOR,
        // The MOUNTDEV_MOUNTED_DEVICE_GUID device interface class is defined for volume devices.
        MOUNTDEV_MOUNTED_DEVICE_GUID,
    }

    public static readonly Dictionary<GUID_DEVCLASS, Guid> ClassGuid = new Dictionary<GUID_DEVCLASS, Guid>()
    {
        {GUID_DEVCLASS.GUID_DEVCLASS_1394,                                  new Guid("6BDD1FC1-810F-11D0-BEC7-08002BE2092F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_1394DEBUG,                             new Guid("66F250D6-7801-4A64-B139-EEA80A450B24")},
        {GUID_DEVCLASS.GUID_DEVCLASS_61883,                                 new Guid("7EBEFBC0-3200-11D2-B4C2-00A0C9697D07")},
        {GUID_DEVCLASS.GUID_DEVCLASS_ADAPTER,                               new Guid("4D36E964-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_APMSUPPORT,                            new Guid("D45B1C18-C8FA-11D1-9F77-0000F805F530")},
        {GUID_DEVCLASS.GUID_DEVCLASS_AVC,                                   new Guid("C06FF265-AE09-48F0-812C-16753D7CBA83")},
        {GUID_DEVCLASS.GUID_DEVCLASS_BATTERY,                               new Guid("72631E54-78A4-11D0-BCF7-00AA00B7B32A")},
        {GUID_DEVCLASS.GUID_DEVCLASS_BIOMETRIC,                             new Guid("53D29EF7-377C-4D14-864B-EB3A85769359")},
        {GUID_DEVCLASS.GUID_DEVCLASS_BLUETOOTH,                             new Guid("E0CBF06C-CD8B-4647-BB8A-263B43F0F974")},
        {GUID_DEVCLASS.GUID_DEVCLASS_CAMERA,                                new Guid("CA3E7AB9-B4C3-4AE6-8251-579EF933890F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_CDROM,                                 new Guid("4D36E965-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_COMPUTEACCELERATOR,                    new Guid("F01A9D53-3FF6-48D2-9F97-C8A7004BE10C")},
        {GUID_DEVCLASS.GUID_DEVCLASS_COMPUTER,                              new Guid("4D36E966-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_DECODER,                               new Guid("6BDD1FC2-810F-11D0-BEC7-08002BE2092F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_DISKDRIVE,                             new Guid("4D36E967-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_DISPLAY,                               new Guid("4D36E968-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_DOT4,                                  new Guid("48721B56-6795-11D2-B1A8-0080C72E74A2")},
        {GUID_DEVCLASS.GUID_DEVCLASS_DOT4PRINT,                             new Guid("49CE6AC8-6F86-11D2-B1E5-0080C72E74A2")},
        {GUID_DEVCLASS.GUID_DEVCLASS_EHSTORAGESILO,                         new Guid("9DA2B80F-F89F-4A49-A5C2-511B085B9E8A")},
        {GUID_DEVCLASS.GUID_DEVCLASS_ENUM1394,                              new Guid("C459DF55-DB08-11D1-B009-00A0C9081FF6")},
        {GUID_DEVCLASS.GUID_DEVCLASS_EXTENSION,                             new Guid("E2F84CE7-8EFA-411C-AA69-97454CA4CB57")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FDC,                                   new Guid("4D36E969-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FIRMWARE,                              new Guid("F2E7DD72-6468-4E36-B6F1-6488F42C1B52")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FLOPPYDISK,                            new Guid("4D36E980-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_GENERIC,                               new Guid("FF494DF1-C4ED-4FAC-9B3F-3786F6E91E7E")},
        {GUID_DEVCLASS.GUID_DEVCLASS_GPS,                                   new Guid("6BDD1FC3-810F-11D0-BEC7-08002BE2092F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_HDC,                                   new Guid("4D36E96A-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_HIDCLASS,                              new Guid("745A17A0-74D3-11D0-B6FE-00A0C90F57DA")},
        {GUID_DEVCLASS.GUID_DEVCLASS_HOLOGRAPHIC,                           new Guid("D612553D-06B1-49CA-8938-E39EF80EB16F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_IMAGE,                                 new Guid("6BDD1FC6-810F-11D0-BEC7-08002BE2092F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_INFINIBAND,                            new Guid("30EF7132-D858-4A0C-AC24-B9028A5CCA3F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_INFRARED,                              new Guid("6BDD1FC5-810F-11D0-BEC7-08002BE2092F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_KEYBOARD,                              new Guid("4D36E96B-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_LEGACYDRIVER,                          new Guid("8ECC055D-047F-11D1-A537-0000F8753ED1")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MEDIA,                                 new Guid("4D36E96C-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MEDIUM_CHANGER,                        new Guid("CE5939AE-EBDE-11D0-B181-0000F8753EC4")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MEMORY,                                new Guid("5099944A-F6B9-4057-A056-8C550228544C")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MODEM,                                 new Guid("4D36E96D-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MONITOR,                               new Guid("4D36E96E-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MOUSE,                                 new Guid("4D36E96F-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MTD,                                   new Guid("4D36E970-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MULTIFUNCTION,                         new Guid("4D36E971-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_MULTIPORTSERIAL,                       new Guid("50906CB8-BA12-11D1-BF5D-0000F805F530")},
        {GUID_DEVCLASS.GUID_DEVCLASS_NET,                                   new Guid("4D36E972-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_NETCLIENT,                             new Guid("4D36E973-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_NETDRIVER,                             new Guid("87EF9AD1-8F70-49EE-B215-AB1FCADCBE3C")},
        {GUID_DEVCLASS.GUID_DEVCLASS_NETSERVICE,                            new Guid("4D36E974-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_NETTRANS,                              new Guid("4D36E975-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_NETUIO,                                new Guid("78912BC1-CB8E-4B28-A329-F322EBADBE0F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_NODRIVER,                              new Guid("4D36E976-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PCMCIA,                                new Guid("4D36E977-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PNPPRINTERS,                           new Guid("4658EE7E-F050-11D1-B6BD-00C04FA372A7")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PORTS,                                 new Guid("4D36E978-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PRIMITIVE,                             new Guid("242681D1-EED3-41D2-A1EF-1468FC843106")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PRINTER,                               new Guid("4D36E979-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PRINTERUPGRADE,                        new Guid("4D36E97A-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PRINTQUEUE,                            new Guid("1ED2BBF9-11F0-4084-B21F-AD83A8E6DCDC")},
        {GUID_DEVCLASS.GUID_DEVCLASS_PROCESSOR,                             new Guid("50127DC3-0F36-415E-A6CC-4CB3BE910B65")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SBP2,                                  new Guid("D48179BE-EC20-11D1-B6B8-00C04FA372A7")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SCMDISK,                               new Guid("53966CB1-4D46-4166-BF23-C522403CD495")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SCMVOLUME,                             new Guid("53CCB149-E543-4C84-B6E0-BCE4F6B7E806")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SCSIADAPTER,                           new Guid("4D36E97B-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SECURITYACCELERATOR,                   new Guid("268C95A1-EDFE-11D3-95C3-0010DC4050A5")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SENSOR,                                new Guid("5175D334-C371-4806-B3BA-71FD53C9258D")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SIDESHOW,                              new Guid("997B5D8D-C442-4F2E-BAF3-9C8E671E9E21")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SMARTCARDREADER,                       new Guid("50DD5230-BA8A-11D1-BF5D-0000F805F530")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SMRDISK,                               new Guid("53487C23-680F-4585-ACC3-1F10D6777E82")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SMRVOLUME,                             new Guid("53B3CF03-8F5A-4788-91B6-D19ED9FCCCBF")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SOFTWARECOMPONENT,                     new Guid("5C4C3332-344D-483C-8739-259E934C9CC8")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SOUND,                                 new Guid("4D36E97C-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_SYSTEM,                                new Guid("4D36E97D-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_TAPEDRIVE,                             new Guid("6D807884-7D21-11CF-801C-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_UNKNOWN,                               new Guid("4D36E97E-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVCLASS_UCM,                                   new Guid("E6F1AA1C-7F3B-4473-B2E8-C97D8AC71D53")},
        {GUID_DEVCLASS.GUID_DEVCLASS_USB,                                   new Guid("36FC9E60-C465-11CF-8056-444553540000")},
        {GUID_DEVCLASS.GUID_DEVCLASS_VOLUME,                                new Guid("71A27CDD-812A-11D0-BEC7-08002BE2092F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_VOLUMESNAPSHOT,                        new Guid("533C5B84-EC70-11D2-9505-00C04F79DEAF")},
        {GUID_DEVCLASS.GUID_DEVCLASS_WCEUSBS,                               new Guid("25DBCE51-6C8F-4A72-8A6D-B54C2B4FC835")},
        {GUID_DEVCLASS.GUID_DEVCLASS_WPD,                                   new Guid("EEC5AD98-8080-425F-922A-DABF3DE3F69A")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_TOP,                          new Guid("B369BAF4-5568-4E82-A87E-A93EB16BCA87")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_ACTIVITYMONITOR,              new Guid("B86DFF51-A31E-4BAC-B3CF-E8CFE75C9FC2")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_UNDELETE,                     new Guid("FE8F1572-C67A-48C0-BBAC-0B5C6D66CAFB")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_ANTIVIRUS,                    new Guid("B1D1A169-C54F-4379-81DB-BEE7D88D7454")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_REPLICATION,                  new Guid("48D3EBC4-4CF8-48FF-B869-9C68AD42EB9F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_CONTINUOUSBACKUP,             new Guid("71AA14F8-6FAD-4622-AD77-92BB9D7E6947")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_CONTENTSCREENER,              new Guid("3E3F0674-C83C-4558-BB26-9820E1EBA5C5")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_QUOTAMANAGEMENT,              new Guid("8503C911-A6C7-4919-8F79-5028F5866B0C")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_SYSTEMRECOVERY,               new Guid("2DB15374-706E-4131-A0C7-D7C78EB0289A")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_CFSMETADATASERVER,            new Guid("CDCF0939-B75B-4630-BF76-80F7BA655884")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_HSM,                          new Guid("D546500A-2AEB-45F6-9482-F4B1799C3177")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_COMPRESSION,                  new Guid("F3586BAF-B5AA-49B5-8D6C-0569284C639F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_ENCRYPTION,                   new Guid("A0A701C0-A511-42FF-AA6C-06DC0395576F")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_VIRTUALIZATION,               new Guid("F75A86C0-10D8-4C3A-B233-ED60E4CDFAAC")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_PHYSICALQUOTAMANAGEMENT,      new Guid("6A0A8E78-BBA6-4FC4-A709-1E33CD09D67E")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_OPENFILEBACKUP,               new Guid("F8ECAFA6-66D1-41A5-899B-66585D7216B7")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_SECURITYENHANCER,             new Guid("D02BC3DA-0C8E-4945-9BD5-F1883C226C8C")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_COPYPROTECTION,               new Guid("89786FF1-9C12-402F-9C9E-17753C7F4375")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_BOTTOM,                       new Guid("37765EA0-5958-4FC9-B04B-2FDFEF97E59E")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_SYSTEM,                       new Guid("5D1B9AAA-01E2-46AF-849F-272B3F324C46")},
        {GUID_DEVCLASS.GUID_DEVCLASS_FSFILTER_INFRASTRUCTURE,               new Guid("E55FA6F9-128C-4D04-ABAB-630C74B1453A")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_DISK,                              new Guid("53F56307-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_CDROM,                             new Guid("53F56308-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_PARTITION,                         new Guid("53F5630A-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_TAPE,                              new Guid("53F5630B-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_WRITEONCEDISK,                     new Guid("53F5630C-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_VOLUME,                            new Guid("53F5630D-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_MEDIUMCHANGER,                     new Guid("53F56310-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_FLOPPY,                            new Guid("53F56311-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_CDCHANGER,                         new Guid("53F56312-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_STORAGEPORT,                       new Guid("2ACCFE60-C130-11D2-B082-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_VMLUN,                             new Guid("6F416619-9F29-42A5-B20B-37E219CA02B0")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_SES,                               new Guid("1790C9EC-47D5-4DF3-B5AF-9ADF3CF23E48")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_ZNSDISK,                           new Guid("B87941C5-FFDB-43C7-B6B1-20B632F0B109")},
        {GUID_DEVCLASS.WDI_STORAGE_PREDICT_FAILURE_DPS_GUID,                new Guid("E9F2D03A-747C-41C2-BB9A-02C62B6D5FCB")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_SERVICE_VOLUME,                    new Guid("6EAD3D82-25EC-46BC-B7FD-C1F0DF8F5037")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_HIDDEN_VOLUME,                     new Guid("7F108A28-9833-4B3B-B780-2C6B5FA5C062")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_UNIFIED_ACCESS_RPMB,               new Guid("27447C21-BCC3-4D07-A05B-A3395BB4EEE7")},
        {GUID_DEVCLASS.GUID_DEVICEDUMP_STORAGE_DEVICE,                      new Guid("D8E2592F-1AAB-4D56-A746-1F7585DF40F4")},
        {GUID_DEVCLASS.GUID_DEVICEDUMP_DRIVER_STORAGE_PORT,                 new Guid("DA82441D-7142-4BC1-B844-0807C5A4B67F")},
        {GUID_DEVCLASS.GUID_AVC_CLASS,                                      new Guid("095780C3-48A1-4570-BD95-46707F78C2DC")},
        {GUID_DEVCLASS.GUID_BTHPORT_DEVICE_INTERFACE,                       new Guid("0850302A-B344-4FDA-9BE9-90576B8D46F0")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_COMPORT,                           new Guid("86E0D1E0-8089-11D0-9CE4-08003E301F73")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_SERENUM_BUS_ENUMERATOR,            new Guid("4D36E978-E325-11CE-BFC1-08002BE10318")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_HID,                               new Guid("4D1E55B2-F16F-11CF-88CB-001111000030")},
        {GUID_DEVCLASS.GUID_HID_INTERFACE_NOTIFY,                           new Guid("2C4E2E88-25E6-4C33-882F-3D82E6073681")},
        {GUID_DEVCLASS.GUID_HID_INTERFACE_HIDPARSE,                         new Guid("F5C315A5-69AC-4BC2-9279-D0B64576F44B")},
        {GUID_DEVCLASS.GUID_DEVICE_APPLICATIONLAUNCH_BUTTON,                new Guid("629758EE-986E-4D9E-8E47-DE27F8AB054D")},
        {GUID_DEVCLASS.GUID_DEVICE_BATTERY,                                 new Guid("72631E54-78A4-11D0-BCF7-00AA00B7B32A")},
        {GUID_DEVCLASS.GUID_DEVICE_LID,                                     new Guid("4AFA3D52-74A7-11D0-BE5E-00A0C9062857")},
        {GUID_DEVCLASS.GUID_DEVICE_MEMORY,                                  new Guid("3FD0F03D-92E0-45FB-B75C-5ED8FFB01021")},
        {GUID_DEVCLASS.GUID_DEVICE_MESSAGE_INDICATOR,                       new Guid("CD48A365-FA94-4CE2-A232-A1B764E5D8B4")},
        {GUID_DEVCLASS.GUID_DEVICE_PROCESSOR,                               new Guid("97FADB10-4E33-40AE-359C-8BEF029DBDD0")},
        {GUID_DEVCLASS.GUID_DEVICE_SYS_BUTTON,                              new Guid("4AFA3D53-74A7-11D0-BE5E-00A0C9062857")},
        {GUID_DEVCLASS.GUID_DEVICE_THERMAL_ZONE,                            new Guid("4AFA3D51-74A7-11D0-BE5E-00A0C9062857")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_BRIGHTNESS,                        new Guid("FDE5BBA4-B3F9-46FB-BDAA-0728CE3100B4")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_DISPLAY_ADAPTER,                   new Guid("5B45201D-F2F2-4F3B-85BB-30FF1F953599")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_I2C,                               new Guid("2564AA4F-DDDB-4495-B497-6AD4A84163D7")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_IMAGE,                             new Guid("6BDD1FC6-810F-11D0-BEC7-08002BE2092F")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_KEYBOARD,                          new Guid("884B96C3-56EF-11D1-BC8C-00A0C91405DD")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_MODEM,                             new Guid("2C7089AA-2E0E-11D1-B114-00C04FC2AAE4")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_MONITOR,                           new Guid("E6F07B5F-EE97-4A90-B076-33F57BF4EAA7")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_MOUSE,                             new Guid("378DE44C-56EF-11D1-BC8C-00A0C91405DD")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_NET,                               new Guid("CAC88484-7515-4C03-82E6-71A87ABAC361")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_OPM,                               new Guid("BF4672DE-6B4E-4BE4-A325-68A91EA49C09")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_PARALLEL,                          new Guid("97F76EF0-F883-11D0-AF1F-0000F800845C")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_PARCLASS,                          new Guid("811FC6A5-F728-11D0-A537-0000F8753ED1")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_SIDESHOW,                          new Guid("152E5811-FEB9-4B00-90F4-D32947AE1681")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_USB_DEVICE,                        new Guid("A5DCBF10-6530-11D2-901F-00C04FB951ED")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_USB_HOST_CONTROLLER,               new Guid("3ABF6F2D-71C4-462A-8A92-1E6861E6AF27")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_USB_HUB,                           new Guid("F18A0E88-C30C-11D0-8815-00A0C906BED8")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_USB_BILLBOARD,                     new Guid("5E9ADAEF-F879-473F-B807-4E5EA77D1B1C")},
        {GUID_DEVCLASS.GUID_USB_WMI_STD_DATA,                               new Guid("4E623B20-CB14-11D1-B331-00A0C959BBD2")},
        {GUID_DEVCLASS.GUID_USB_WMI_STD_NOTIFICATION,                       new Guid("4E623B20-CB14-11D1-B331-00A0C959BBD2")},
        {GUID_DEVCLASS.GUID_USB_WMI_DEVICE_PERF_INFO,                       new Guid("66C1AA3C-499F-49A0-A9A5-61E2359F6407")},
        {GUID_DEVCLASS.GUID_USB_WMI_NODE_INFO,                              new Guid("9C179357-DC7A-4F41-B66B-323B9DDCB5B1")},
        {GUID_DEVCLASS.GUID_USB_WMI_TRACING,                                new Guid("3A61881B-B4E6-4BF9-AE0F-3CD8F394E52F")},
        {GUID_DEVCLASS.GUID_USB_TRANSFER_TRACING,                           new Guid("681EB8AA-403D-452C-9F8A-F0616FAC9540")},
        {GUID_DEVCLASS.GUID_USB_PERFORMANCE_TRACING,                        new Guid("D5DE77A6-6AE9-425C-B1E2-F5615FD348A9")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_WINUSB,                            new Guid("DEE824EF-729B-4A0E-9C14-B7117D33A817")},
        {GUID_DEVCLASS.ANDROID_USB_CLASS_ID,                                new Guid("F72FE0D4-CBCB-407D-8814-9ED673D0DD6B")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_WPD_AUDIO,                         new Guid("F33FDC04-D1AC-4E8E-9A30-19BBD4B108AE")},
        {GUID_DEVCLASS.GUID_USB_WMI_SURPRISE_REMOVAL_NOTIFICATION,          new Guid("9BBBF831-A2F2-43B4-96D1-86944B5914B3")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_VIDEO_OUTPUT_ARRIVAL,              new Guid("1AD9E4F0-F88D-4360-BAB9-4C2D55E564CD")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_WPD,                               new Guid("6AC27878-A6FA-4155-BA85-F98F491D4F33")},
        {GUID_DEVCLASS.GUID_DEVINTERFACE_WPD_PRIVATE,                       new Guid("BA0C718F-4DED-49B7-BDD3-FABE28661211")},
        {GUID_DEVCLASS.GUID_DISPLAY_DEVICE_ARRIVAL,                         new Guid("1CA05180-A699-450A-9A0C-DE4FBE3DDD89")},
        {GUID_DEVCLASS.GUID_IO_VOLUME_DEVICE_INTERFACE,                     new Guid("53F5630D-B6BF-11D0-94F2-00A0C91EFB8B")},
        {GUID_DEVCLASS.GUID_VIRTUAL_AVC_CLASS,                              new Guid("616EF4D0-23CE-446D-A568-C31EB01913D0")},
        {GUID_DEVCLASS.KSCATEGORY_ACOUSTIC_ECHO_CANCEL,                     new Guid("BF963D80-C559-11D0-8A2B-00A0C9255AC1")},
        {GUID_DEVCLASS.KSCATEGORY_AUDIO,                                    new Guid("6994AD04-93EF-11D0-A3CC-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_AUDIO_DEVICE,                             new Guid("FBF6F530-07B9-11D2-A71E-0000F8004788")},
        {GUID_DEVCLASS.KSCATEGORY_AUDIO_GFX,                                new Guid("9BAF9572-340C-11D3-ABDC-00A0C90AB16F")},
        {GUID_DEVCLASS.KSCATEGORY_AUDIO_SPLITTER,                           new Guid("9EA331FA-B91B-45F8-9285-BD2BC77AFCDE")},
        {GUID_DEVCLASS.KSCATEGORY_BDA_IP_SINK,                              new Guid("71985F4A-1CA1-11d3-9CC8-00C04F7971E0")},
        {GUID_DEVCLASS.KSCATEGORY_BDA_NETWORK_EPG,                          new Guid("71985F49-1CA1-11D3-9CC8-00C04F7971E0")},
        {GUID_DEVCLASS.KSCATEGORY_BDA_NETWORK_PROVIDER,                     new Guid("71985F4B-1CA1-11D3-9CC8-00C04F7971E0")},
        {GUID_DEVCLASS.KSCATEGORY_BDA_NETWORK_TUNER,                        new Guid("71985F48-1CA1-11D3-9CC8-00C04F7971E0")},
        {GUID_DEVCLASS.KSCATEGORY_BDA_RECEIVER_COMPONENT,                   new Guid("FD0A5AF4-B41D-11D2-9C95-00C04F7971E0")},
        {GUID_DEVCLASS.KSCATEGORY_BDA_TRANSPORT_INFORMATION,                new Guid("A2E3074F-6C3D-11D3-B653-00C04F79498E")},
        {GUID_DEVCLASS.KSCATEGORY_BRIDGE,                                   new Guid("085AFF00-62CE-11CF-A5D6-28DB04C10000")},
        {GUID_DEVCLASS.KSCATEGORY_CAPTURE,                                  new Guid("65E8773D-8F56-11D0-A3B9-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_CLOCK,                                    new Guid("53172480-4791-11D0-A5D6-28DB04C10000")},
        {GUID_DEVCLASS.KSCATEGORY_COMMUNICATIONSTRANSFORM,                  new Guid("CF1DDA2C-9743-11D0-A3EE-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_CROSSBAR,                                 new Guid("A799A801-A46D-11D0-A18C-00A02401DCD4")},
        {GUID_DEVCLASS.KSCATEGORY_DATACOMPRESSOR,                           new Guid("1E84C900-7E70-11D0-A5D6-28DB04C10000")},
        {GUID_DEVCLASS.KSCATEGORY_DATADECOMPRESSOR,                         new Guid("2721AE20-7E70-11D0-A5D6-28DB04C10000")},
        {GUID_DEVCLASS.KSCATEGORY_DATATRANSFORM,                            new Guid("2EB07EA0-7E70-11D0-A5D6-28DB04C10000")},
        {GUID_DEVCLASS.KSCATEGORY_DRM_DESCRAMBLE,                           new Guid("FFBB6E3F-CCFE-4D84-90D9-421418B03A8E")},
        {GUID_DEVCLASS.KSCATEGORY_ENCODER,                                  new Guid("19689BF6-C384-48FD-AD51-90E58C79F70B")},
        {GUID_DEVCLASS.KSCATEGORY_ESCALANTE_PLATFORM_DRIVER,                new Guid("74F3AEA8-9768-11D1-8E07-00A0C95EC22E")},
        {GUID_DEVCLASS.KSCATEGORY_FILESYSTEM,                               new Guid("760FED5E-9357-11D0-A3CC-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_INTERFACETRANSFORM,                       new Guid("CF1DDA2D-9743-11D0-A3EE-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_MEDIUMTRANSFORM,                          new Guid("CF1DDA2E-9743-11D0-A3EE-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_MICROPHONE_ARRAY_PROCESSOR,               new Guid("830A44F2-A32D-476B-BE97-42845673B35A")},
        {GUID_DEVCLASS.KSCATEGORY_MIXER,                                    new Guid("AD809C00-7B88-11D0-A5D6-28DB04C10000")},
        {GUID_DEVCLASS.KSCATEGORY_MULTIPLEXER,                              new Guid("7A5DE1D3-01A1-452C-B481-4FA2B96271E8")},
        {GUID_DEVCLASS.KSCATEGORY_NETWORK,                                  new Guid("67C9CC3C-69C4-11D2-8759-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_NETWORK_CAMERA,                           new Guid("B8238652-B500-41EB-B4F3-4234F7F5AE99")},
        {GUID_DEVCLASS.KSCATEGORY_PREFERRED_MIDIOUT_DEVICE,                 new Guid("D6C50674-72C1-11D2-9755-0000F8004788")},
        {GUID_DEVCLASS.KSCATEGORY_PREFERRED_WAVEIN_DEVICE,                  new Guid("D6C50671-72C1-11D2-9755-0000F8004788")},
        {GUID_DEVCLASS.KSCATEGORY_PREFERRED_WAVEOUT_DEVICE,                 new Guid("D6C5066E-72C1-11D2-9755-0000F8004788")},
        {GUID_DEVCLASS.KSCATEGORY_PROXY,                                    new Guid("97EBAACA-95BD-11D0-A3EA-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_QUALITY,                                  new Guid("97EBAACB-95BD-11D0-A3EA-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_REALTIME,                                 new Guid("EB115FFC-10C8-4964-831D-6DCB02E6F23F")},
        {GUID_DEVCLASS.KSCATEGORY_RENDER,                                   new Guid("65E8773E-8F56-11D0-A3B9-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_SENSOR_CAMERA,                            new Guid("24E552D7-6523-47F7-A647-D3465BF1F5CA")},
        {GUID_DEVCLASS.KSCATEGORY_SPLITTER,                                 new Guid("0A4252A0-7E70-11D0-A5D6-28DB04C10000")},
        {GUID_DEVCLASS.KSCATEGORY_SYNTHESIZER,                              new Guid("DFF220F3-F70F-11D0-B917-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_SYSAUDIO,                                 new Guid("A7C7A5B1-5AF3-11D1-9CED-00A024BF0407")},
        {GUID_DEVCLASS.KSCATEGORY_TEXT,                                     new Guid("6994AD06-93EF-11D0-A3CC-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_TOPOLOGY,                                 new Guid("DDA54A40-1E4C-11D1-A050-405705C10000")},
        {GUID_DEVCLASS.KSCATEGORY_TVAUDIO,                                  new Guid("A799A802-A46D-11D0-A18C-00A02401DCD4")},
        {GUID_DEVCLASS.KSCATEGORY_TVTUNER,                                  new Guid("A799A800-A46D-11D0-A18C-00A02401DCD4")},
        {GUID_DEVCLASS.KSCATEGORY_VBICODEC,                                 new Guid("07DAD660-22F1-11D1-A9F4-00C04FBBDE8F")},
        {GUID_DEVCLASS.KSCATEGORY_VIDEO,                                    new Guid("6994AD05-93EF-11D0-A3CC-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_VIDEO_CAMERA,                             new Guid("E5323777-F976-4F5B-9B55-B94699C46E44")},
        {GUID_DEVCLASS.KSCATEGORY_VIRTUAL,                                  new Guid("3503EAC4-1F26-11D1-8AB0-00A0C9223196")},
        {GUID_DEVCLASS.KSCATEGORY_VPMUX,                                    new Guid("A799A803-A46D-11D0-A18C-00A02401DCD4")},
        {GUID_DEVCLASS.KSCATEGORY_WDMAUD,                                   new Guid("3E227E76-690D-11D2-8161-0000F8775BF1")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_AUDIO_DECODER,                        new Guid("9EA73FB4-EF7A-4559-8D5D-719D8F0426C7")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_AUDIO_EFFECT,                         new Guid("11064C48-3648-4ED0-932E-05CE8AC811B7")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_AUDIO_ENCODER,                        new Guid("91C64BD0-F91E-4D8C-9276-DB248279D975")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_DEMULTIPLEXER,                        new Guid("A8700A7A-939B-44C5-99D7-76226B23B3F1")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_MULTIPLEXER,                          new Guid("059C561E-05AE-4B61-B69D-55B61EE54A7B")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_OTHER,                                new Guid("90175D57-B7EA-4901-AEB3-933A8747756F")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_VIDEO_DECODER,                        new Guid("D6C02D4B-6833-45B4-971A-05A4B04BAB91")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_VIDEO_EFFECT,                         new Guid("12E17C21-532C-4A6E-8A1C-40825A736397")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_VIDEO_ENCODER,                        new Guid("F79EAC7D-E545-4387-BDEE-D647D7BDE42A")},
        {GUID_DEVCLASS.KSMFT_CATEGORY_VIDEO_PROCESSOR,                      new Guid("302EA3FC-AA5F-47F9-9F7A-C2188BB16302")},
        {GUID_DEVCLASS.MOUNTDEV_MOUNTED_DEVICE_GUID,                        new Guid("53F5630D-B6BF-11D0-94F2-00A0C91EFB8B")},
    };
}
