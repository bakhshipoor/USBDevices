using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Interfaces;

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

    public UInt16? Availability { get; set; }
    public UInt32? ConfigManagerErrorCode { get; set; }
    public bool? ConfigManagerUserConfig { get; set; }
    public string? CreationClassName { get; set; }
    public string? DeviceID { get; set; }
    public List<UInt16> PowerManagementCapabilities { get; set; }
    public bool? ErrorCleared { get; set; }
    public string? ErrorDescription { get; set; }
    public UInt32? LastErrorCode { get; set; }
    public string? PNPDeviceID { get; set; }
    public bool? PowerManagementSupported { get; set; }
    public UInt16? StatusInfo { get; set; }
    public string? SystemCreationClassName { get; set; }
    public string? SystemName { get; set; }

    public UInt64? BlockSize { get; set; }
    public string? ErrorMethodology { get; set; }
    public UInt64? NumberOfBlocks { get; set; }
    public string? Purpose { get; set; }

    public UInt16? Access { get; set; }
    public bool? Bootable { get; set; }
    public bool? PrimaryPartition { get; set; }

    public UInt16? AdditionalAvailability { get; set; }
    public List<string> IdentifyingDescriptions { get; set; }
    public UInt64? MaxQuiesceTime { get; set; }
    public UInt64? OtherIdentifyingInfo { get; set; }
    public UInt64? PowerOnHours { get; set; }
    public UInt64? TotalPowerOnHours { get; set; }
    public bool? BootPartition { get; set; }
    public UInt32? DiskIndex { get; set; }
    public UInt32? HiddenSectors { get; set; }
    public UInt32? Index { get; set; }
    public bool? RewritePartition { get; set; }
    public UInt64? Size { get; set; }
    public UInt64? StartingOffset { get; set; }
    public string? Type { get; set; }
}
