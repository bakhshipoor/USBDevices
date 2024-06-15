using CopyFilesToFlash.ViewModels;
using USBDevicesLibrary.Interfaces.Storage;
using System.ComponentModel;
using USBDevicesLibrary.Win32API;
using static USBDevicesLibrary.Win32API.FmIfsData;
using System.Runtime.InteropServices;
using static USBDevicesLibrary.Win32API.FmIfsFunctions;
using USBDevicesLibrary.Events;
using CopyFilesToFlash.Events;

namespace CopyFilesToFlash.Models;

public class Volume : DiskLogicalInterface
{
    private readonly MainViewModel mainViewModel;
    public event EventHandler<FormatEventArgs>? FormatChanged;

    public Volume(MainViewModel _MainViewModel, DiskLogicalInterface usbVolume)
    {
        mainViewModel = _MainViewModel;

		DevicePath = usbVolume.DevicePath;
        BaseDeviceProperties = usbVolume.BaseDeviceProperties;
        BaseClassProperties = usbVolume.BaseClassProperties;
        DiskNumber = usbVolume.DiskNumber;
        StartingOffset = usbVolume.StartingOffset;
        Size = usbVolume.Size;
        VolumePath_Guid = usbVolume.VolumePath_Guid;
        VolumePath_Dos = usbVolume.VolumePath_Dos;
        Name = usbVolume.Name;
        VolumeSerialNumber = usbVolume.VolumeSerialNumber;
        VolumeName = usbVolume.VolumeName;
        FileSystem = usbVolume.FileSystem;
        MaximumComponentLength = usbVolume.MaximumComponentLength;
        FileSystemFlags = usbVolume.FileSystemFlags;
        FreeSpace = usbVolume.FreeSpace;
        Capacity = usbVolume.Capacity;
        SectorsPerCluster = usbVolume.SectorsPerCluster;
        BytesPerSector = usbVolume.BytesPerSector;
        NumberOfFreeClusters = usbVolume.NumberOfFreeClusters;
        TotalNumberOfClusters = usbVolume.TotalNumberOfClusters;
        DriveType = usbVolume.DriveType;

        _VolumeLabel = string.Empty;
        _ErrorDescription = "No Error Detected.";

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

    private byte _TasksStatus;
    public byte TasksStatus
    {
        get { return _TasksStatus; }
        set { _TasksStatus = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(TasksStatus))); }
    }

    private string _ErrorDescription;
    public string ErrorDescription
    {
        get { return _ErrorDescription; }
        set { _ErrorDescription = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(ErrorDescription))); }
    }

    internal virtual void OnFormatChanged(FormatEventArgs e)
    {
        FormatChanged?.Invoke(this, e);
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

    public async void FormatVolume()
    {
        string filesystem = string.Empty;
        if (mainViewModel.Configuration.Format)
            filesystem = mainViewModel.FileSystemTypes[mainViewModel.Configuration.FileSystemIndex];
        else
            filesystem = FileSystem;
        FMIFS_CALLBACK formatCallBack = new FMIFS_CALLBACK(FormatCallBack);
        await FmIfsFunctions.FormatVolume(Name, FMIFS_MEDIA_TYPE.FmMediaRemovable, filesystem, VolumeLabel, true, formatCallBack);
    }

    public byte FormatCallBack(FMIFS_PACKET_TYPE PacketType, uint PacketLength, IntPtr PacketData)
    {
        uint percentage = 0;
        switch (PacketType)
        {
            case FMIFS_PACKET_TYPE.FmIfsCantLock:
                break;
            case FMIFS_PACKET_TYPE.FmIfsPercentCompleted:
                FMIFS_PERCENT_COMPLETE_INFORMATION percent = Marshal.PtrToStructure<FMIFS_PERCENT_COMPLETE_INFORMATION>(PacketData);
                percentage = percent.PercentCompleted;
                OnFormatChanged(new FormatEventArgs(this,true,false, percentage));
                break;
            case FMIFS_PACKET_TYPE.FmIfsTextMessage:
                FMIFS_TEXT_MESSAGE message = Marshal.PtrToStructure<FMIFS_TEXT_MESSAGE>(PacketData);
                break;
            case FMIFS_PACKET_TYPE.FmIfsFinished:
                FMIFS_FINISHED_INFORMATION success = Marshal.PtrToStructure<FMIFS_FINISHED_INFORMATION>(PacketData);
                OnFormatChanged(new FormatEventArgs(this, false, Convert.ToBoolean(success.Success), percentage));
                break;
            case FMIFS_PACKET_TYPE.FmIfsFormatReport:
                FMIFS_FORMAT_REPORT_INFORMATION formatReportInformation = Marshal.PtrToStructure<FMIFS_FORMAT_REPORT_INFORMATION>(PacketData);
                break;
        }
        return 1;
    }

}
