using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Interfaces.Storage;

public class VolumeFreeSpace
{
    public VolumeFreeSpace()
    {
        
    }

    public ulong FreeSpace { get; set; }
    public ulong Capacity { get; set; }
    public uint SectorsPerCluster { get; set; }
    public uint BytesPerSector { get; set; }
    public uint NumberOfFreeClusters { get; set; }
    public uint TotalNumberOfClusters { get; set; }
}
