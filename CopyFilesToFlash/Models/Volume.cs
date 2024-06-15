using CopyFilesToFlash.ViewModels;
using USBDevicesLibrary.Interfaces.Storage;
using System.ComponentModel;

namespace CopyFilesToFlash.Models;

public class Volume : DiskLogicalInterface
{
    private readonly MainViewModel mainViewModel;

    public Volume(MainViewModel _MainViewModel, DiskLogicalInterface usbDiskDrive)
    {
        mainViewModel = _MainViewModel;

		DevicePath = usbDiskDrive.DevicePath;
        BaseDeviceProperties = usbDiskDrive.BaseDeviceProperties;
        BaseClassProperties = usbDiskDrive.BaseClassProperties;
        DiskNumber = usbDiskDrive.DiskNumber;
        StartingOffset = usbDiskDrive.StartingOffset;
        Size = usbDiskDrive.Size;
        VolumePath_Guid = usbDiskDrive.VolumePath_Guid;
        VolumePath_Dos = usbDiskDrive.VolumePath_Dos;
        Name = usbDiskDrive.Name;
        VolumeSerialNumber = usbDiskDrive.VolumeSerialNumber;
        VolumeName = usbDiskDrive.VolumeName;
        FileSystem = usbDiskDrive.FileSystem;
        MaximumComponentLength = usbDiskDrive.MaximumComponentLength;
        FileSystemFlags = usbDiskDrive.FileSystemFlags;
        FreeSpace = usbDiskDrive.FreeSpace;
        Capacity = usbDiskDrive.Capacity;
        SectorsPerCluster = usbDiskDrive.SectorsPerCluster;
        BytesPerSector = usbDiskDrive.BytesPerSector;
        NumberOfFreeClusters = usbDiskDrive.NumberOfFreeClusters;
        TotalNumberOfClusters = usbDiskDrive.TotalNumberOfClusters;
        DriveType = usbDiskDrive.DriveType;

        _VolumeLabel = string.Empty;
        CheckVolumeIsValid();
        SetVolumeLabel();
    }

	private bool _IsValid;
	public bool IsValid
    {
		get { return _IsValid; }
		set { _IsValid = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid))); }
	}

    private string _VolumeLabel;
    public string VolumeLabel
    {
        get { return _VolumeLabel; }
        set { _VolumeLabel = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(VolumeLabel))); }
    }


    public void CheckVolumeIsValid()
    {
        if (mainViewModel.TotalTasks.TotalFileSize < FreeSpace)
            IsValid = true;
        else
            IsValid = false;
    }

    public void SetVolumeLabel()
    {
        if (FileSystem.Equals("NTFS",StringComparison.OrdinalIgnoreCase))
        {
            VolumeLabel = StorageInterfaceHelpers.ValidateVolumeLabel(mainViewModel.Configuration.VolumeLabel, false);
        }
        else
        {
            VolumeLabel = StorageInterfaceHelpers.ValidateVolumeLabel(mainViewModel.Configuration.VolumeLabel, true);
        }
    }

}
