using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Devices;

namespace USBDevicesLibrary.Interfaces;

public class DiskDriveInterface : InterfaceBaseClass
{
    public DiskDriveInterface()
    {
        DevicePath = string.Empty;
        BaseDeviceProperties = new();
        BaseClassProperties = new();

        Capabilities = [];
        CapabilityDescriptions = [];

        CompressionMethod = string.Empty;
        ErrorMethodology = string.Empty;
        FirmwareRevision = string.Empty;
        InterfaceType = string.Empty;
        Manufacturer = string.Empty;
        MediaType = string.Empty;
        Model = string.Empty;
        SerialNumber = string.Empty;
    }

    public DiskDriveInterface(Device device) : this()
    {
        DevicePath = device.DevicePath;
        BaseDeviceProperties = device.DeviceProperties;
        BaseClassProperties = device.ClassProperties;
    }

    public string DevicePath { get; set; }
    public DeviceProperties BaseDeviceProperties { get; set; }
    public DeviceClassProperties BaseClassProperties { get; set; }

    public List<ushort> Capabilities { get; set; }
    public List<string> CapabilityDescriptions { get; set; }
    public string CompressionMethod { get; set; }
    public ulong DefaultBlockSize { get; set; }
    public string ErrorMethodology { get; set; }
    public ulong MaxBlockSize { get; set; }
    public ulong MaxMediaSize { get; set; }
    public ulong MinBlockSize { get; set; }
    public bool NeedsCleaning { get; set; }
    public uint NumberOfMediaSupported { get; set; }

    public uint BytesPerSector { get; set; }
    public string FirmwareRevision { get; set; }
    public uint Index { get; set; }
    public string InterfaceType { get; set; }
    public string Manufacturer { get; set; }
    public bool MediaLoaded { get; set; }
    public string MediaType { get; set; }
    public string Model { get; set; }
    public uint Partitions { get; set; }
    public uint SCSIBus { get; set; }
    public ushort SCSILogicalUnit { get; set; }
    public ushort SCSIPort { get; set; }
    public ushort SCSITargetId { get; set; }
    public uint SectorsPerTrack { get; set; }
    public string SerialNumber { get; set; }
    public uint Signature { get; set; }
    public ulong Size { get; set; }
    public ulong TotalCylinders { get; set; }
    public uint TotalHeads { get; set; }
    public ulong TotalSectors { get; set; }
    public ulong TotalTracks { get; set; }
    public uint TracksPerCylinder { get; set; }
}
