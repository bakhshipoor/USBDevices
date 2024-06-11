using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Interfaces.Storage;

public class DiskLogicalInterface : InterfaceBaseClass
{
    public DiskLogicalInterface()
    {

    }

    
    public string? Name { get; set; }
  
    public ulong? BlockSize { get; set; }
    public string? ErrorMethodology { get; set; }
    public ulong? NumberOfBlocks { get; set; }
    public string? Purpose { get; set; }

    public ushort? Access { get; set; }
    public ulong? FreeSpace { get; set; }
    public ulong? Size { get; set; }

    public bool? Compressed { get; set; }
    public uint? DriveType { get; set; }
    public string? FileSystem { get; set; }
    public uint? MaximumComponentLength { get; set; }
    public uint? MediaType { get; set; }
    public string? ProviderName { get; set; }
    public bool? QuotasDisabled { get; set; }
    public bool? QuotasIncomplete { get; set; }
    public bool? QuotasRebuilding { get; set; }
    public bool? SupportsDiskQuotas { get; set; }
    public bool? SupportsFileBasedCompression { get; set; }
    public bool? VolumeDirty { get; set; }
    public string? VolumeName { get; set; }
    public string? VolumeSerialNumber { get; set; }

    // 1
    public uint DiskNumber {  get; set; }
    public ulong StatingOffset {  get; set; }
    public ulong VolumeLength {  get; set; }

    //2
    public string DrivePathGuid { get; set; }
    public string DrivePathDos { get; set; }
}
