using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Win32API;
using static USBDevicesLibrary.Win32API.NTDDDiskData;
using static USBDevicesLibrary.Win32API.NTDDStorData;

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
                diskDrivePartitions.Add(partition);
            }
        }
        return diskDrivePartitions;
    }

}
