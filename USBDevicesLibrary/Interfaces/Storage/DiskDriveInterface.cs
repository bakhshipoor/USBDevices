using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Win32API.NTDDDiskData;
using static USBDevicesLibrary.Win32API.NTDDStorData;

namespace USBDevicesLibrary.Interfaces.Storage;

public class DiskDriveInterface : InterfaceBaseClass
{
    public DiskDriveInterface()
    {
        DevicePath = string.Empty;
        BaseDeviceProperties = new();
        BaseClassProperties = new();

        //Capabilities = [];
        //CapabilityDescriptions = [];

        //CompressionMethod = string.Empty;
        //ErrorMethodology = string.Empty;
        FirmwareRevision = string.Empty;
        Name = string.Empty;
        SerialNumber = string.Empty;
        VendorId = string.Empty;
        ProductId = string.Empty;
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

    //public List<ushort> Capabilities { get; set; }
    //public List<string> CapabilityDescriptions { get; set; }
    //public string CompressionMethod { get; set; }
    //public ulong DefaultBlockSize { get; set; }
    //public string ErrorMethodology { get; set; }
    //public ulong MaxBlockSize { get; set; }
    //public ulong MaxMediaSize { get; set; }
    //public ulong MinBlockSize { get; set; }
    //public bool NeedsCleaning { get; set; }
    //public uint NumberOfMediaSupported { get; set; }

    
    public uint Partitions { get; set; }

    // Disk Geo
    public ulong DiskSize { get; set; }
    public uint BytesPerSector { get; set; }
    public uint SectorsPerTrack { get; set; }
    public uint TracksPerCylinder { get; set; }
    public ulong TotalCylinders { get; set; }
    public uint TotalHeads { get; set; }
    public ulong TotalTracks { get; set; }
    public ulong TotalSectors { get; set; }
    public MEDIA_TYPE MediaType { get; set; }
    public uint Signature { get; set; }
    public bool MediaLoaded { get; set; }

    // Disk Number
    public uint DiskNumber { get; set; }
    public string Name { get; set; }

    // Disk Storage Descriptor
    public string FirmwareRevision { get; set; }
    public string SerialNumber { get; set; }
    public string VendorId { get; set; }
    public string ProductId { get; set; }
    public STORAGE_BUS_TYPE BusType { get; set; }
    public bool RemovableMedia { get; set; }

    public override List<PropertiesToList> PropertiesToList()
    {
        List<PropertiesToList> bResponse = [];
        bResponse.Add(new PropertiesToList() { Name = "Dvice Instance ID: ", Value = BaseDeviceProperties.Device_InstanceId });
        bResponse.Add(new PropertiesToList() { Name = "Device Path: ", Value = DevicePath });
        bResponse.Add(new PropertiesToList() { Name = "Name: ", Value = Name });
        bResponse.Add(new PropertiesToList() { Name = "Disk Size: ", Value = DiskSize });
        bResponse.Add(new PropertiesToList() { Name = "Bytes Per Sector: ", Value = BytesPerSector });
        bResponse.Add(new PropertiesToList() { Name = "Sectors Per Track: ", Value = SectorsPerTrack });
        bResponse.Add(new PropertiesToList() { Name = "Tracks Per Cylinder: ", Value = TracksPerCylinder });
        bResponse.Add(new PropertiesToList() { Name = "Total Cylinders: ", Value = TotalCylinders });
        bResponse.Add(new PropertiesToList() { Name = "Total Heads: ", Value = TotalHeads });
        bResponse.Add(new PropertiesToList() { Name = "Total Tracks: ", Value = TotalTracks });
        bResponse.Add(new PropertiesToList() { Name = "Total Sectors: ", Value = TotalSectors });
        bResponse.Add(new PropertiesToList() { Name = "Media Type: ", Value = MediaType });
        bResponse.Add(new PropertiesToList() { Name = "Signature: ", Value = Signature });
        bResponse.Add(new PropertiesToList() { Name = "Media Loaded: ", Value = MediaLoaded });
        bResponse.Add(new PropertiesToList() { Name = "Disk Number: ", Value = DiskNumber });
        bResponse.Add(new PropertiesToList() { Name = "Firmware Revision: ", Value = FirmwareRevision });
        bResponse.Add(new PropertiesToList() { Name = "Serial Number: ", Value = SerialNumber });
        bResponse.Add(new PropertiesToList() { Name = "Vendor ID: ", Value = VendorId });
        bResponse.Add(new PropertiesToList() { Name = "Product ID: ", Value = ProductId });
        bResponse.Add(new PropertiesToList() { Name = "Bus Type: ", Value = BusType });
        bResponse.Add(new PropertiesToList() { Name = "Removable Media: ", Value = RemovableMedia });

        return bResponse;
    }
}
