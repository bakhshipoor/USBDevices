using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Win32API.Kernel32Data;
using static USBDevicesLibrary.Win32API.NTDDDiskData;

namespace USBDevicesLibrary.Interfaces.Storage;

public class DiskLogicalInterface : InterfaceBaseClass
{
    public DiskLogicalInterface()
    {
        DevicePath = string.Empty;
        BaseDeviceProperties = new();
        BaseClassProperties = new();

        VolumePath_Guid = string.Empty;
        VolumePath_Dos = string.Empty;
        Name = string.Empty;
        VolumeName = string.Empty;
        FileSystem = string.Empty;

        FileSystemFlags = [];
    }

    public DiskLogicalInterface(Device device) : this()
    {
        DevicePath = device.DevicePath;
        BaseDeviceProperties = device.DeviceProperties;
        BaseClassProperties = device.ClassProperties;
    }

    public string DevicePath { get; set; }
    public DeviceProperties BaseDeviceProperties { get; set; }
    public DeviceClassProperties BaseClassProperties { get; set; }

    // 1
    public uint DiskNumber {  get; set; }
    public ulong StartingOffset {  get; set; }
    public ulong Size {  get; set; }

    //2
    public string VolumePath_Guid { get; set; }
    public string VolumePath_Dos { get; set; }
    public string Name { get; set; }

    // Get From Volume Information
    public uint VolumeSerialNumber { get; set; }
    public string VolumeName { get; set; }
    public string FileSystem { get; set; }
    public uint MaximumComponentLength { get; set; }
    public List<FILE_SYSTEM_FLAGS> FileSystemFlags {  get; set; }

    // Get Volume Free Space
    public ulong FreeSpace { get; set; }
    public ulong Capacity { get; set; }
    public uint SectorsPerCluster { get; set; }
    public uint BytesPerSector { get; set; }
    public uint NumberOfFreeClusters { get; set; }
    public uint TotalNumberOfClusters { get; set; }

    // Get Drive Type
    public DRIVE_TYPE DriveType { get; set; }

    public override List<PropertiesToList> PropertiesToList()
    {
        List<PropertiesToList> bResponse = [];
        bResponse.Add(new PropertiesToList() { Name = "Name: ", Value = Name });
        bResponse.Add(new PropertiesToList() { Name = "Dvice Instance ID: ", Value = BaseDeviceProperties.Device_InstanceId });
        bResponse.Add(new PropertiesToList() { Name = "Dvice Path: ", Value = DevicePath });
        bResponse.Add(new PropertiesToList() { Name = "Volume Guid Path: ", Value = VolumePath_Guid });
        bResponse.Add(new PropertiesToList() { Name = "Volume Dos Path: ", Value = VolumePath_Dos });
        bResponse.Add(new PropertiesToList() { Name = "Volume Serial Number: ", Value = VolumeSerialNumber });
        bResponse.Add(new PropertiesToList() { Name = "Volume Name: ", Value = VolumeName });
        bResponse.Add(new PropertiesToList() { Name = "File System: ", Value = FileSystem });
        bResponse.Add(new PropertiesToList() { Name = "Maximum Component Length: ", Value = MaximumComponentLength });
        bResponse.Add(new PropertiesToList() { Name = "File System Flags: ", Value = string.Join("\r\n", FileSystemFlags) });
        bResponse.Add(new PropertiesToList() { Name = "Capacity: ", Value = Capacity });
        bResponse.Add(new PropertiesToList() { Name = "Free Space: ", Value = FreeSpace });
        bResponse.Add(new PropertiesToList() { Name = "Bytes Per Sector: ", Value = BytesPerSector });
        bResponse.Add(new PropertiesToList() { Name = "Sectors Per Cluster: ", Value = SectorsPerCluster });
        bResponse.Add(new PropertiesToList() { Name = "Total Number Of Clusters: ", Value = TotalNumberOfClusters });
        bResponse.Add(new PropertiesToList() { Name = "Number Of Free Clusters: ", Value = NumberOfFreeClusters });
        bResponse.Add(new PropertiesToList() { Name = "DriveType: ", Value = DriveType });
        return bResponse;
    }
}
