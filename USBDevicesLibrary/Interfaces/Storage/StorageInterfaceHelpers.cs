using Microsoft.Win32.SafeHandles;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using USBDevicesLibrary.Devices;
using USBDevicesLibrary.Win32API;
using static USBDevicesLibrary.Win32API.CfgMgrData;
using static USBDevicesLibrary.Win32API.ClassesGUID;
using static USBDevicesLibrary.Win32API.Kernel32Data;
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
                    for (uint i = 0; i < byteDescriptor.Length; i++)
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

    public static ObservableCollection<DiskPartitionInterface> GetDiskDrivePartitions(DRIVE_LAYOUT_INFORMATION_EX driveLayoutInfoEx, DiskDriveInterface diskDrive)
    {
        uint diskNumber = diskDrive.DiskNumber;
        ObservableCollection<DiskPartitionInterface> diskDrivePartitions = [];
        if (driveLayoutInfoEx.PartitionCount > 0)
        {
            // Get Volumes
            ObservableCollection<DiskLogicalInterface> logicalDisks = GetDiskDriveVolumes(diskDrive);

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
                if (partition.PartitionStyle == PARTITION_STYLE.PARTITION_STYLE_MBR)
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
                    if ((itemLogicalDisk.StartingOffset == partition.StartingOffset) && (itemLogicalDisk.Size == partition.Size))
                    {
                        partition.Add(itemLogicalDisk);
                    }
                }


                diskDrivePartitions.Add(partition);
            }
        }
        return diskDrivePartitions;
    }

    public static ObservableCollection<DiskLogicalInterface> GetDiskDriveVolumes(DiskDriveInterface diskDrive)
    {
        uint diskNumber = diskDrive.DiskNumber;
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
                uint numberOfExtents = (volumeDiskExtents.LengthTransferred - 8) / 24; // !!!! ????
                for (int i = 0; i < numberOfExtents; i++)
                {
                    if (volumeDiskEx.Extents[i].DiskNumber == diskNumber)
                    {
                        find = true;
                        startingOffset = volumeDiskEx.Extents[i].StartingOffset;
                        extentLength = volumeDiskEx.Extents[i].ExtentLength;
                        break;
                    }
                }
                if (find)
                {
                    DiskLogicalInterface logicalDisk = new(itemVolume);
                    logicalDisk.DiskNumber = diskNumber;
                    logicalDisk.StartingOffset = startingOffset;
                    logicalDisk.Size = extentLength;
                    Win32ResponseDataStruct driveGuidPath = GetVolumeGuidPath(itemVolume.DevicePath);
                    if (driveGuidPath.Status)
                    {
                        logicalDisk.VolumePath_Guid = (string)driveGuidPath.Data;
                    }
                    string dosPath = GetVolumeDosPath(logicalDisk.VolumePath_Guid);
                    logicalDisk.VolumePath_Dos = dosPath;

                    if (!string.IsNullOrEmpty(logicalDisk.VolumePath_Dos))
                        logicalDisk.Name = logicalDisk.VolumePath_Dos;
                    else
                        logicalDisk.Name = logicalDisk.VolumePath_Guid;

                    // Volume Information
                    Win32ResponseDataStruct getVolumeInformation = GetVolumeInformation(logicalDisk.VolumePath_Guid);
                    if (getVolumeInformation.Status)
                    {
                        VolumeInformation volumeInformation = (VolumeInformation)getVolumeInformation.Data;
                        logicalDisk.VolumeName = volumeInformation.VolumeName;
                        logicalDisk.VolumeSerialNumber = volumeInformation.VolumeSerialNumber;
                        logicalDisk.FileSystem = volumeInformation.FileSystem;
                        logicalDisk.MaximumComponentLength = volumeInformation.MaximumComponentLength;
                        logicalDisk.FileSystemFlags = volumeInformation.FileSystemFlags;
                    }

                    Win32ResponseDataStruct getVolumeFreeSpace = GetVolumeFreeSpace(logicalDisk.VolumePath_Guid);
                    if (getVolumeFreeSpace.Status)
                    {
                        VolumeFreeSpace volumeFreeSpace = (VolumeFreeSpace)getVolumeFreeSpace.Data;
                        logicalDisk.BytesPerSector = volumeFreeSpace.BytesPerSector;
                        logicalDisk.SectorsPerCluster = volumeFreeSpace.SectorsPerCluster;
                        logicalDisk.TotalNumberOfClusters = volumeFreeSpace.TotalNumberOfClusters;
                        logicalDisk.NumberOfFreeClusters = volumeFreeSpace.NumberOfFreeClusters;
                        logicalDisk.Capacity = volumeFreeSpace.Capacity;
                        logicalDisk.FreeSpace = volumeFreeSpace.FreeSpace;
                    }

                    logicalDisk.DriveType = (DRIVE_TYPE)GetVolumeDriveType(logicalDisk.VolumePath_Guid).Data;

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

    public static Win32ResponseDataStruct GetLogicalDriveStrings()
    {
        Win32ResponseDataStruct bResponse = new();
        List<string> driveStrings = [];
        byte[] byteDriveLetters = new byte[1024];
        int lenghtTransfered = Kernel32Functions.GetLogicalDriveStrings(byteDriveLetters.Length, byteDriveLetters);
        int win32ErrorCode = Marshal.GetLastWin32Error();
        string win32ErrorMessage = new Win32Exception(win32ErrorCode).Message;
        if (lenghtTransfered > 0)
        {
            byte[] byteFinalDriveLetters = new byte[lenghtTransfered];
            Array.Copy(byteDriveLetters, byteFinalDriveLetters, lenghtTransfered);
            driveStrings = Encoding.ASCII.GetString(byteFinalDriveLetters).Remove(lenghtTransfered - 1, 1).Split('\0').ToList();
            bResponse.Status = true;
            bResponse.Data = driveStrings;
            bResponse.LengthTransferred = (uint)lenghtTransfered;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
        }
        return bResponse;
    }

    public static string GetVolumeDosPath(string volumeGuidPath)
    {
        string bResponse = string.Empty;
        Win32ResponseDataStruct getDriveLetters = GetLogicalDriveStrings();
        if (getDriveLetters.Status)
        {
            List<string> driveLetters = (List<string>)getDriveLetters.Data;
            foreach (string itemDriveLetter in driveLetters)
            {
                Win32ResponseDataStruct guidPath = GetVolumeGuidPath(itemDriveLetter);
                if (volumeGuidPath.Equals((string)guidPath.Data, StringComparison.OrdinalIgnoreCase))
                {
                    bResponse = itemDriveLetter;
                    break;
                }
            }
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetVolumeInformation(string volumePath)
    {
        Win32ResponseDataStruct bResponse = new();
        StringBuilder volumeName = new(260);
        volumeName.Length = 260;
        uint volumeSerialNumber;
        uint maximumComponentLength;
        uint fileSystemFlags;
        StringBuilder fileSystemName = new(260);
        fileSystemName.Length = 260;
        bool isSuccess = Kernel32Functions.GetVolumeInformation(
            volumePath,
            volumeName,
            volumeName.Length,
            out volumeSerialNumber,
            out maximumComponentLength,
            out fileSystemFlags,
            fileSystemName,
            fileSystemName.Length
            );
        if (isSuccess)
        {
            VolumeInformation volumeInformation = new();
            volumeInformation.VolumeName = volumeName.ToString();
            volumeInformation.VolumeSerialNumber = volumeSerialNumber;
            volumeInformation.MaximumComponentLength = maximumComponentLength;
            volumeInformation.FileSystem = fileSystemName.ToString();
            foreach (FILE_SYSTEM_FLAGS itemFileSystemFlag in Enum.GetValues(typeof(FILE_SYSTEM_FLAGS)))
            {
                if (((uint)itemFileSystemFlag & fileSystemFlags) != 0)
                {
                    volumeInformation.FileSystemFlags.Add(itemFileSystemFlag);
                }
            }
            bResponse.Status = true;
            bResponse.Data = volumeInformation;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetVolumeFreeSpace(string VolumePath)
    {
        Win32ResponseDataStruct bResponse = new();
        bool isSuccess = Kernel32Functions.GetDiskFreeSpace
            (
            VolumePath,
            out uint lpSectorsPerCluster,
            out uint lpBytesPerSector,
            out uint lpNumberOfFreeClusters,
            out uint lpTotalNumberOfClusters
            );
        if (isSuccess)
        {
            VolumeFreeSpace volumeFreeSpace = new();
            volumeFreeSpace.BytesPerSector = lpBytesPerSector;
            volumeFreeSpace.SectorsPerCluster = lpSectorsPerCluster;
            volumeFreeSpace.TotalNumberOfClusters = lpTotalNumberOfClusters;
            volumeFreeSpace.NumberOfFreeClusters = lpNumberOfFreeClusters;
            ulong totalSectors = lpBytesPerSector * lpSectorsPerCluster;
            volumeFreeSpace.Capacity = totalSectors * lpTotalNumberOfClusters;
            volumeFreeSpace.FreeSpace = totalSectors * lpNumberOfFreeClusters;
            bResponse.Status = true;
            bResponse.Data = volumeFreeSpace;
        }
        else
        {
            bResponse.Status = false;
            bResponse.Exception = new Win32Exception(Marshal.GetLastWin32Error());
        }
        return bResponse;
    }

    public static Win32ResponseDataStruct GetVolumeDriveType(string VolumePath)
    {
        Win32ResponseDataStruct bResponse = new();
        uint bResult = Kernel32Functions.GetDriveType(VolumePath);
        bResponse.Status = true;
        bResponse.Data = (DRIVE_TYPE)bResult;
        return bResponse;
    }

    public static Win32ResponseDataStruct EjectDiskDrive(DiskDriveInterface diskDrive)
    {
        Win32ResponseDataStruct bresponse = new();
        foreach (DiskPartitionInterface itemPartitions in diskDrive)
        {
            foreach (DiskLogicalInterface itemLogicalDrive in itemPartitions)
            {
                bresponse = EjectVolume(itemLogicalDrive);
                if (!bresponse.Status)
                {
                    return bresponse;
                }
            }
        }
        bresponse = EjectMedia(diskDrive.BaseDeviceProperties.DevInst);
        return bresponse;
    }

    public static Win32ResponseDataStruct EjectVolume(DiskLogicalInterface volume)
    {
        return EjectMedia(volume.BaseDeviceProperties.DevInst);
    }

    public static Win32ResponseDataStruct EjectMedia(uint devInst)
    {
        Win32ResponseDataStruct bresponse = new();
        uint MaxLenght = 260;
        StringBuilder vetoName = new((int)MaxLenght);
        vetoName.Length = (int)MaxLenght;
        PNP_VETO_TYPE vetoType;
        CONFIGRET configRet = SetupAPIFunctions.CM_Query_And_Remove_SubTreeW(devInst, out vetoType, vetoName, MaxLenght, Query_And_Remove_SubTree_FLAGS.CM_REMOVE_UI_OK);
        // Try Again
        if (configRet != CONFIGRET.CR_SUCCESS)
        {
            configRet = SetupAPIFunctions.CM_Query_And_Remove_SubTreeW(devInst, out vetoType, vetoName, MaxLenght, Query_And_Remove_SubTree_FLAGS.CM_REMOVE_UI_OK);
        }
        if (configRet == CONFIGRET.CR_SUCCESS)
        {
            bresponse.Status = true;
            Win32Exception exception = new(string.Format("{0} : {1}", vetoType, vetoName));
            exception.Source = "CM_Query_And_Remove_SubTreeW()";
            bresponse.Exception = exception;
        }
        else
        {
            bresponse.Status = false;
            Win32Exception exception = new(string.Format("{0} [ {1} : {2} ]", configRet, vetoType, vetoName));
            exception.Source = "CM_Query_And_Remove_SubTreeW()";
            bresponse.Exception = exception;
        }
        return bresponse;
    }

}