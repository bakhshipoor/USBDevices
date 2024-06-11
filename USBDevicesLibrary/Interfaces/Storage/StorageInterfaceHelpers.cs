using Microsoft.Win32.SafeHandles;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.Win32API;
using static USBDevicesLibrary.Win32API.ClassesGUID;
using static USBDevicesLibrary.Win32API.MountMgrData;
using static USBDevicesLibrary.Win32API.NTDDDiskData;
using static USBDevicesLibrary.Win32API.NTDDStorData;
using static USBDevicesLibrary.Win32API.NTDDVolData;
using static USBDevicesLibrary.Win32API.SetupAPIData;

namespace USBDevicesLibrary.Interfaces.Storage;

public static class StorageInterfaceHelpers
{
    public static Win32ResponseDataStruct GetDiskDriveGeometryEx(string devicePath)
    {
        Win32ResponseDataStruct bresponse = new();
        Win32ResponseDataStruct diskHandle = Kernel32Functions.CreateDeviceHandle(devicePath, readOnly: true);
        if (diskHandle.Status)
        {
            DISK_GEOMETRY_EX diskGeo = new();
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)diskHandle.Data,
                    diskGeo, DISK_IOCTL[DISK_IOCTL_Enum.IOCTL_DISK_GET_DRIVE_GEOMETRY_EX]);
            if (decviceIOControl.Status)
            {
                bresponse.Status = true;
                bresponse.Data = decviceIOControl.Data;
            }
            ((SafeFileHandle)diskHandle.Data).Close();
        }
        return bresponse;
    }

    public static Win32ResponseDataStruct GetDiskDriveNumber(string devicePath)
    {
        Win32ResponseDataStruct bresponse = new();
        Win32ResponseDataStruct diskHandle = Kernel32Functions.CreateDeviceHandle(devicePath, readOnly: true);
        if (diskHandle.Status)
        {
            STORAGE_DEVICE_NUMBER diskNumber = new();
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)diskHandle.Data,
                    diskNumber, STORAGE_IOCTL[STORAGE_IOCTL_Enum.IOCTL_STORAGE_GET_DEVICE_NUMBER]);
            if (decviceIOControl.Status)
            {
                bresponse.Status = true;
                bresponse.Data = decviceIOControl.Data;
            }
            ((SafeFileHandle)diskHandle.Data).Close();
        }
        return bresponse;
    }

    public static Win32ResponseDataStruct GetStorageDeviceDescriptor(string devicePath)
    {
        Win32ResponseDataStruct bresponse = new();
        Win32ResponseDataStruct diskHandle = Kernel32Functions.CreateDeviceHandle(devicePath, readOnly: true);
        if (diskHandle.Status)
        {
            STORAGE_PROPERTY_QUERY storageQuery = new()
            {
                PropertyId = STORAGE_PROPERTY_ID.StorageDeviceProperty,
                QueryType = STORAGE_QUERY_TYPE.PropertyStandardQuery,
            };
            STORAGE_DEVICE_DESCRIPTOR storageDescriptor = new();
            storageDescriptor.Size = (uint)Marshal.SizeOf(typeof(STORAGE_DEVICE_DESCRIPTOR));

            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)diskHandle.Data,
                    storageQuery, STORAGE_IOCTL[STORAGE_IOCTL_Enum.IOCTL_STORAGE_QUERY_PROPERTY], storageDescriptor);
            if (decviceIOControl.Status)
            {
                StorageDeviceDescriptor storageDeviceDescriptor = new();
                STORAGE_DEVICE_DESCRIPTOR stDesc = (STORAGE_DEVICE_DESCRIPTOR)decviceIOControl.Data;
                storageDeviceDescriptor.DeviceType = stDesc.DeviceType;
                storageDeviceDescriptor.DeviceTypeModifier = stDesc.DeviceTypeModifier;
                storageDeviceDescriptor.RemovableMedia = Convert.ToBoolean(stDesc.RemovableMedia);
                storageDeviceDescriptor.CommandQueueing = Convert.ToBoolean(stDesc.CommandQueueing);
                storageDeviceDescriptor.BusType = stDesc.BusType;

                uint descriptorLenght = 36;
                uint lenghtOfDataRemoved = descriptorLenght + stDesc.RawPropertiesLength;

                byte[] byteDescriptor = new byte[stDesc.Size - lenghtOfDataRemoved];
                Array.Copy(stDesc.RawDeviceProperties, descriptorLenght, byteDescriptor, 0, byteDescriptor.Length);

                if (stDesc.VendorIdOffset > 0)
                {
                    uint indexVID = stDesc.VendorIdOffset - lenghtOfDataRemoved;
                    uint lenghtVID = 0;
                    for (uint i=0;i< byteDescriptor.Length;i++)
                    {
                        byte b = byteDescriptor[i + indexVID];
                        if (b == 0) break;
                        lenghtVID++;
                    }
                    byte[] byteVID = new byte[lenghtVID];
                    Array.Copy(byteDescriptor, indexVID, byteVID, 0, lenghtVID);
                    storageDeviceDescriptor.VendorId = Encoding.ASCII.GetString(byteVID);
                }

                if (stDesc.ProductIdOffset > 0)
                {
                    uint indexPID = stDesc.ProductIdOffset - lenghtOfDataRemoved;
                    uint lenghtPID = 0;
                    for (uint i = 0; i < byteDescriptor.Length; i++)
                    {
                        byte b = byteDescriptor[i + indexPID];
                        if (b == 0) break;
                        lenghtPID++;
                    }
                    byte[] bytePID = new byte[lenghtPID];
                    Array.Copy(byteDescriptor, indexPID, bytePID, 0, lenghtPID);
                    storageDeviceDescriptor.ProductId = Encoding.ASCII.GetString(bytePID);
                }

                if (stDesc.ProductRevisionOffset > 0)
                {
                    uint indexRev = stDesc.ProductRevisionOffset - lenghtOfDataRemoved;
                    uint lenghtRev = 0;
                    for (uint i = 0; i < byteDescriptor.Length; i++)
                    {
                        byte b = byteDescriptor[i + indexRev];
                        if (b == 0) break;
                        lenghtRev++;
                    }
                    byte[] byteRev = new byte[lenghtRev];
                    Array.Copy(byteDescriptor, indexRev, byteRev, 0, lenghtRev);
                    storageDeviceDescriptor.ProductRevision = Encoding.ASCII.GetString(byteRev);
                }

                if (stDesc.SerialNumberOffset > 0)
                {
                    uint indexSerNum = stDesc.SerialNumberOffset - lenghtOfDataRemoved;
                    uint lenghtSerNum = 0;
                    for (uint i = 0; i < byteDescriptor.Length; i++)
                    {
                        byte b = byteDescriptor[i + indexSerNum];
                        if (b == 0) break;
                        lenghtSerNum++;
                    }
                    byte[] byteSerNum = new byte[lenghtSerNum];
                    Array.Copy(byteDescriptor, indexSerNum, byteSerNum, 0, lenghtSerNum);
                    storageDeviceDescriptor.SerialNumber = Encoding.ASCII.GetString(byteSerNum);
                }

                bresponse.Status = true;
                bresponse.Data = storageDeviceDescriptor;
            }
            ((SafeFileHandle)diskHandle.Data).Close();
        }
        return bresponse;
    }

    public static Win32ResponseDataStruct GetDiskDriveLayoutInformationEx(string devicePath)
    {
        Win32ResponseDataStruct bresponse = new();
        Win32ResponseDataStruct diskHandle = Kernel32Functions.CreateDeviceHandle(devicePath, readOnly: true);
        if (diskHandle.Status)
        {
            DRIVE_LAYOUT_INFORMATION_EX driveLayoutInformationEx = new();
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)diskHandle.Data,
                    driveLayoutInformationEx, DISK_IOCTL[DISK_IOCTL_Enum.IOCTL_DISK_GET_DRIVE_LAYOUT_EX]);
            if (decviceIOControl.Status)
            {
                bresponse.Status = true;
                bresponse.Data = decviceIOControl.Data;
            }
            ((SafeFileHandle)diskHandle.Data).Close();
        }
        return bresponse;
    }

    public static ObservableCollection<DiskPartitionInterface> GetDiskDrivePartitions(DRIVE_LAYOUT_INFORMATION_EX driveLayoutInfoEx, uint diskNumber)
    {
        ObservableCollection<DiskPartitionInterface> diskDrivePartitions = [];
        if (driveLayoutInfoEx.PartitionCount>0)
        {
            // Get Volumes
            ObservableCollection<DiskLogicalInterface> logicalDisks = GetDiskDriveVolumes(diskNumber);

            foreach (PARTITION_INFORMATION_EX itemPartitionInfo in driveLayoutInfoEx.PartitionEntry)
            {
                if (itemPartitionInfo.PartitionInfo.MBR_PartitionType == PARTITION_TYPE.PARTITION_ENTRY_UNUSED)
                    break;
                DiskPartitionInterface partition = new();
                partition.DiskNumber = diskNumber;
                partition.PartitionNumber = itemPartitionInfo.PartitionNumber;
                partition.PartitionStyle = itemPartitionInfo.PartitionStyle;
                partition.Size = itemPartitionInfo.PartitionLength;
                partition.StartingOffset = itemPartitionInfo.StartingOffset;
                partition.RewritePartition = Convert.ToBoolean(itemPartitionInfo.RewritePartition);
                partition.IsServicePartition = Convert.ToBoolean(itemPartitionInfo.IsServicePartition);
                if (partition.PartitionStyle ==PARTITION_STYLE.PARTITION_STYLE_MBR)
                {
                    partition.MBR_Type = itemPartitionInfo.PartitionInfo.MBR_PartitionType;
                    partition.Bootable = Convert.ToBoolean(itemPartitionInfo.PartitionInfo.MBR_BootIndicator);
                    partition.HiddenSectors = itemPartitionInfo.PartitionInfo.MBR_HiddenSectors;
                    partition.PartitionID = itemPartitionInfo.PartitionInfo.MBR_PartitionId;
                }
                else if (partition.PartitionStyle == PARTITION_STYLE.PARTITION_STYLE_GPT)
                {
                    partition.GPT_Type = itemPartitionInfo.PartitionInfo.GPT_PartitionType;
                    partition.GPT_Attributes = itemPartitionInfo.PartitionInfo.GPT_Attributes;
                    partition.GPT_Name = itemPartitionInfo.PartitionInfo.GPT_Name;
                    partition.PartitionID = itemPartitionInfo.PartitionInfo.GPT_PartitionId;
                }
                partition.Name = $"Disk #{diskNumber}, Partition #{partition.PartitionNumber}";

                foreach (DiskLogicalInterface itemLogicalDisk in logicalDisks)
                {
                    if ((itemLogicalDisk.StatingOffset == partition.StartingOffset) && (itemLogicalDisk.VolumeLength == partition.Size))
                    {
                        partition.Add(itemLogicalDisk);
                    }
                }
                

                diskDrivePartitions.Add(partition);
            }
        }
        return diskDrivePartitions;
    }

    public static ObservableCollection<DiskLogicalInterface> GetDiskDriveVolumes(uint diskNumber)
    {
        uint flags = (uint)(DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_DEVICEINTERFACE);
        Guid deviceClassGuid = ClassGuid[GUID_DEVCLASS.GUID_DEVINTERFACE_VOLUME];
        ObservableCollection<Device> volumesFromSetupAPI = DeviceHelpers.GetClassDevicesWithProperties(deviceClassGuid, string.Empty, flags);
        ObservableCollection<DiskLogicalInterface> logicalDisks = [];
        foreach (Device itemVolume in volumesFromSetupAPI)
        {
            ulong startingOffset = 0;
            ulong extentLength = 0;
            Win32ResponseDataStruct volumeDiskExtents = GetVolumeDiskExtents(itemVolume.DevicePath);
            if (volumeDiskExtents.Status)
            {
                bool find = false;
                VOLUME_DISK_EXTENTS volumeDiskEx = (VOLUME_DISK_EXTENTS)volumeDiskExtents.Data;
                uint numberOfExtents = volumeDiskExtents.LengthTransferred / 32; // 32 is VOLUME_DISK_EXTENTS size with 1 array
                for (int i=0;i< numberOfExtents;i++)
                {
                    if (volumeDiskEx.Extents[i].DiskNumber== diskNumber)
                    {
                        find = true;
                        startingOffset = volumeDiskEx.Extents[i].StartingOffset;
                        extentLength = volumeDiskEx.Extents[i].ExtentLength;
                        break;
                    }
                }
                if (find)
                {
                    DiskLogicalInterface logicalDisk = new();
                    logicalDisk.DiskNumber = diskNumber;
                    logicalDisk.StatingOffset = startingOffset;
                    logicalDisk.VolumeLength = extentLength;
                    Win32ResponseDataStruct driveGuidPath = GetVolumeGuidPath(itemVolume.DevicePath);
                    if (driveGuidPath.Status)
                    {
                        logicalDisk.DrivePathGuid = (string)driveGuidPath.Data;
                    }
                    string dosPath = GetVolumeDosPath(logicalDisk.DrivePathGuid);
                    logicalDisk.DrivePathDos = dosPath;

                    if (!string.IsNullOrEmpty(logicalDisk.DrivePathDos))
                        logicalDisk.Name = logicalDisk.DrivePathDos;
                    else
                        logicalDisk.Name = logicalDisk.DrivePathGuid;


                    logicalDisks.Add(logicalDisk);
                }
            }
        }
        return logicalDisks;
    }

    public static Win32ResponseDataStruct GetVolumeDiskExtents(string devicePath)
    {
        Win32ResponseDataStruct bresponse = new();
        Win32ResponseDataStruct volumeHandle = Kernel32Functions.CreateDeviceHandle(devicePath, readOnly: true);
        if (volumeHandle.Status)
        {
            VOLUME_DISK_EXTENTS volumeDiskExtents = new();
            Win32ResponseDataStruct decviceIOControl = Kernel32Functions.GetDeviceIoControl((SafeFileHandle)volumeHandle.Data,
                    volumeDiskExtents, VOLUME_IOCTL[VOLUME_IOCTL_Enum.IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS]);
            if (decviceIOControl.Status)
            {
                bresponse.Status = true;
                bresponse.Data = decviceIOControl.Data;
                bresponse.LengthTransferred = decviceIOControl.LengthTransferred;
            }
            ((SafeFileHandle)volumeHandle.Data).Close();
        }
        return bresponse;
    }

    public static Win32ResponseDataStruct GetVolumeGuidPath(string devicePath)
    {
        Win32ResponseDataStruct bResponse = new();
        StringBuilder volumeGuidPath = new(260);
        volumeGuidPath.Length = 260;
        bool isSuccess = Kernel32Functions.GetVolumeNameForVolumeMountPoint(devicePath + "\\", volumeGuidPath, (uint)volumeGuidPath.Length);
        if (isSuccess)
        {
            bResponse.Status = true;
            bResponse.Data = volumeGuidPath.ToString();
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
        }
        return bResponse;
    }

    public static List<string> GetLogicalDriveStrings()
    {
        List<string> bResponse = [];
        byte[] byteDriveLetters = new byte[1024];
        int lenghtTransfered = Kernel32Functions.GetLogicalDriveStrings(byteDriveLetters.Length, byteDriveLetters);
        int win32ErrorCode = Marshal.GetLastWin32Error();
        string win32ErrorMessage = new Win32Exception(win32ErrorCode).Message;
        if (lenghtTransfered != 0)
        {
            byte[] byteFinalDriveLetters = new byte[lenghtTransfered];
            Array.Copy(byteDriveLetters, byteFinalDriveLetters, lenghtTransfered);
            bResponse = Encoding.ASCII.GetString(byteFinalDriveLetters).Remove(lenghtTransfered-1,1).Split('\0').ToList();
        }
        return bResponse;
    }

    public static string GetVolumeDosPath(string volumeGuidPath)
    {
        string bResponse = string.Empty;
        List<string> driveLetters = GetLogicalDriveStrings();
        foreach (string itemDriveLetter in driveLetters)
        {
            Win32ResponseDataStruct guidPath = GetVolumeGuidPath(itemDriveLetter);
            if (volumeGuidPath.Equals((string)guidPath.Data,StringComparison.OrdinalIgnoreCase))
            {
                bResponse = itemDriveLetter;
                break;
            }
        }
        return bResponse;
    }
}


/*

            uint volumeSerialNumber;
            uint maximumComponentLength;
            uint fileSystemFlags;

            StringBuilder dcdc2 = new(200);
            dcdc2.Length = 200;
            StringBuilder dcdc3 = new(200);
            dcdc3.Length = 200;
            bool ss2 = Kernel32Functions.GetVolumeInformation(dcdc.ToString(), dcdc2, 200,out volumeSerialNumber,
                out maximumComponentLength,
                out fileSystemFlags,
                dcdc3,
                200);
            string err2 = new Win32Exception(Marshal.GetLastWin32Error()).Message;

            


*/
