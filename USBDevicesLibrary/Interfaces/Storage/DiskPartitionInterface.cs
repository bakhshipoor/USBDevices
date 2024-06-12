using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Win32API.NTDDDiskData;

namespace USBDevicesLibrary.Interfaces.Storage;

public class DiskPartitionInterface : InterfaceBaseClass
{
    public DiskPartitionInterface()
    {
        //IdentifyingDescriptions = [];
        //ErrorMethodology = string.Empty;
        //Purpose = string.Empty;

        Name = string.Empty;
        GPT_Name = string.Empty;
    }

    //public ulong BlockSize { get; set; }
    //public string ErrorMethodology { get; set; }
    //public ulong NumberOfBlocks { get; set; }
    //public string Purpose { get; set; }
    //public ushort Access { get; set; }
    //public bool PrimaryPartition { get; set; }
    //public ushort AdditionalAvailability { get; set; }
    //public List<string> IdentifyingDescriptions { get; set; }
    //public ulong MaxQuiesceTime { get; set; }
    //public ulong OtherIdentifyingInfo { get; set; }
    //public ulong PowerOnHours { get; set; }
    //public ulong TotalPowerOnHours { get; set; }
    //public bool BootPartition { get; set; }
 

    // Drive Layout Information
    public string Name { get; set; }
    
    public uint DiskNumber { get; set; }
    public uint PartitionNumber { get; set; }
    public PARTITION_STYLE PartitionStyle { get; set; }
    public ulong Size { get; set; }
    public ulong StartingOffset { get; set; }
    public bool RewritePartition { get; set; }
    public bool IsServicePartition { get; set; }

    // MBR
    public PARTITION_TYPE MBR_Type { get; set; }
    public bool Bootable { get; set; }
    public uint HiddenSectors { get; set; }

    // MBR & GPT
    public Guid PartitionID {  get; set; }

    // GPT
    public Guid GPT_Type { get; set; }
    public ulong GPT_Attributes { get; set; }
    public string GPT_Name { get; set; }


    public override List<PropertiesToList> PropertiesToList()
    {
        List<PropertiesToList> bResponse = [];
        bResponse.Add(new PropertiesToList() { Name = "Name: ", Value = Name });
        bResponse.Add(new PropertiesToList() { Name = "Disk Number: ", Value = DiskNumber });
        bResponse.Add(new PropertiesToList() { Name = "Partition Number: ", Value = PartitionNumber });
        bResponse.Add(new PropertiesToList() { Name = "Partition Style: ", Value = PartitionStyle });
        bResponse.Add(new PropertiesToList() { Name = "Size: ", Value = Size });
        bResponse.Add(new PropertiesToList() { Name = "Starting Offset: ", Value = StartingOffset });
        bResponse.Add(new PropertiesToList() { Name = "Rewrite Partitionr: ", Value = RewritePartition });
        bResponse.Add(new PropertiesToList() { Name = "IsService Partition: ", Value = IsServicePartition });
        bResponse.Add(new PropertiesToList() { Name = "MBR Type: ", Value = MBR_Type });
        bResponse.Add(new PropertiesToList() { Name = "MBR Bootable: ", Value = Bootable });
        bResponse.Add(new PropertiesToList() { Name = "MBR HiddenSectors: ", Value = HiddenSectors });
        bResponse.Add(new PropertiesToList() { Name = "Partition ID: ", Value = PartitionID });
        bResponse.Add(new PropertiesToList() { Name = "GPT Type: ", Value = GPT_Type });
        bResponse.Add(new PropertiesToList() { Name = "GPT Attributes: ", Value = GPT_Attributes });
        bResponse.Add(new PropertiesToList() { Name = "GPT Name: ", Value = GPT_Name });

        return bResponse;
    }
}
