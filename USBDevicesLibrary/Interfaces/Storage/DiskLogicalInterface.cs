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

    public string? Caption { get; set; }
    public string? Description { get; set; }
    public DateTime? InstallDate { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }

    public ushort? Availability { get; set; }
    public uint? ConfigManagerErrorCode { get; set; }
    public bool? ConfigManagerUserConfig { get; set; }
    public string? CreationClassName { get; set; }
    public string? DeviceID { get; set; }
    public List<ushort> PowerManagementCapabilities { get; set; }
    public bool? ErrorCleared { get; set; }
    public string? ErrorDescription { get; set; }
    public uint? LastErrorCode { get; set; }
    public string? PNPDeviceID { get; set; }
    public bool? PowerManagementSupported { get; set; }
    public ushort? StatusInfo { get; set; }
    public string? SystemCreationClassName { get; set; }
    public string? SystemName { get; set; }

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
}
