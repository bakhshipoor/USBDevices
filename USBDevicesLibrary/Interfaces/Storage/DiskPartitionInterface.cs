using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Interfaces.Storage;

public class DiskPartitionInterface : InterfaceBaseClass
{
    public DiskPartitionInterface()
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
    public bool? Bootable { get; set; }
    public bool? PrimaryPartition { get; set; }

    public ushort? AdditionalAvailability { get; set; }
    public List<string> IdentifyingDescriptions { get; set; }
    public ulong? MaxQuiesceTime { get; set; }
    public ulong? OtherIdentifyingInfo { get; set; }
    public ulong? PowerOnHours { get; set; }
    public ulong? TotalPowerOnHours { get; set; }
    public bool? BootPartition { get; set; }
    public uint? DiskIndex { get; set; }
    public uint? HiddenSectors { get; set; }
    public uint? Index { get; set; }
    public bool? RewritePartition { get; set; }
    public ulong? Size { get; set; }
    public ulong? StartingOffset { get; set; }
    public string? Type { get; set; }
}
