using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.Kernel32Data;

namespace USBDevicesLibrary.Interfaces.Storage;

public class VolumeInformation
{
    public VolumeInformation()
    {
        VolumeName = string.Empty;
        FileSystem = string.Empty;
        FileSystemFlags = [];
    }

    public uint VolumeSerialNumber { get; set; }
    public string VolumeName { get; set; }
    public string FileSystem { get; set; }
    public uint MaximumComponentLength { get; set; }
    public List<FILE_SYSTEM_FLAGS> FileSystemFlags { get; set; }
}
