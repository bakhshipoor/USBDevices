using CopyFilesToFlash.ViewModels;
using System.Collections.ObjectModel;
using USBDevicesLibrary.Interfaces.Storage;
using USBDevicesLibrary.USBDevices;
using USBDevicesLibrary.Win32API;

namespace CopyFilesToFlash.Models;

public class USBFlashDisk : ViewModelBase
{
    private readonly MainViewModel mainViewModel;
    private const string taskStopped = "Task Stopped";

    public USBFlashDisk(MainViewModel _MainViewModel, USBDevice usbDevice)
    {
        mainViewModel = _MainViewModel;

        _USBFlashDevice = usbDevice;
        _VID = string.Format("{0:X4}", usbDevice.IDVendor).ToUpper();
        _PID = string.Format("{0:X4}", usbDevice.IDProduct).ToUpper();
        _TaskTotal = mainViewModel.TotalTasks.GetTotalTasks();

        _VolumeCount = 0;
        Volumes = [];
        if (usbDevice.BaseDeviceProperties.Device_Service.Contains("USBSTOR", StringComparison.OrdinalIgnoreCase))
        {
            foreach (DiskDriveInterface itemDiskDrive in usbDevice)
            {
                _DiskSize = itemDiskDrive.DiskSize;
                foreach (DiskPartitionInterface itemPartitions in itemDiskDrive)
                {
                    foreach (DiskLogicalInterface itemLogicalDrive in itemPartitions)
                    {
                        Volume volume = new(mainViewModel, itemLogicalDrive);
                        volume.FormatChanged += Volume_FormatChanged;
                        Volumes.Add(volume);
                        if (volume.IsValid)
                            _VolumeCount++;
                    }
                }
            }
        }
        _TaskDescription = taskStopped;
    }

    private string _VID;
    public string VID
    {
        get { return _VID; }
        set { _VID = value; OnPropertyChanged(nameof(VID)); }
    }

    private string _PID;
    public string PID
    {
        get { return _PID; }
        set { _PID = value; OnPropertyChanged(nameof(PID)); }
    }

    private uint _VolumeCount;
    public uint VolumeCount
    {
        get { return _VolumeCount; }
        set { _VolumeCount = value; OnPropertyChanged(nameof(VolumeCount)); }
    }

    private uint _VolumeCurrent;
    public uint VolumeCurrent
    {
        get { return _VolumeCurrent; }
        set { _VolumeCurrent = value; OnPropertyChanged(nameof(VolumeCurrent)); }
    }

    private ulong _DiskSize;
    public ulong DiskSize
    {
        get { return _DiskSize; }
        set { _DiskSize = value; OnPropertyChanged(nameof(DiskSize)); }
    }

    private uint _TaskTotal;
    public uint TaskTotal
    {
        get { return _TaskTotal; }
        set { _TaskTotal = value; OnPropertyChanged(nameof(TaskTotal)); }
    }

    private uint _TaskCurrent;
    public uint TaskCurrent
    {
        get { return _TaskCurrent; }
        set { _TaskCurrent = value; OnPropertyChanged(nameof(TaskCurrent)); }
    }

    private int _TaskPrecentage;
    public int TaskPrecentage
    {
        get { return _TaskPrecentage; }
        set { _TaskPrecentage = value; OnPropertyChanged(nameof(TaskPrecentage)); }
    }

    private string _TaskDescription;
    public string TaskDescription
    {
        get { return _TaskDescription; }
        set { _TaskDescription = value; OnPropertyChanged(nameof(TaskDescription)); }
    }

    private bool _IsSelected;
    public bool IsSelected
    {
        get { return _IsSelected; }
        set { _IsSelected = value; OnPropertyChanged(nameof(IsSelected)); }
    }

    public ObservableCollection<Volume> Volumes {  get; set; }

    private USBDevice _USBFlashDevice;
    public USBDevice USBFlashDevice
    {
        get { return _USBFlashDevice; }
        set { _USBFlashDevice = value; }
    }

    private void Volume_FormatChanged(object? sender, Events.FormatEventArgs e)
    {
        if (sender!=null && sender is Volume)
        {
            Volume volume = (Volume)sender;
            if (!e.FormatIsWorking)
            {
                if (e.FormatIsSuccess)
                {
                    volume.TasksStatus = 1;
                }
                else
                {
                    volume.TasksStatus = 2;
                    volume.ErrorDescription = "Error While Format";
                }
            }
        }
    }

    public async void StartTasks()
    {
        uint volumeIndex = 1;
        foreach (Volume itemVolume in Volumes)
        {
            VolumeCurrent = volumeIndex;
            if (itemVolume.IsValid)
            {
                TaskCurrent = 1;
                if (mainViewModel.Configuration.Format)
                {
                    TaskDescription = $"Format Volume {volumeIndex}";
                    await Task.Run(() => itemVolume.FormatVolume());
                    // Tray Again
                    if (itemVolume.TasksStatus != 1)
                        await Task.Run(() => itemVolume.FormatVolume());
                    if (itemVolume.TasksStatus == 1)
                        itemVolume.FileSystem = mainViewModel.FileSystemTypes[mainViewModel.Configuration.FileSystemIndex];
                    else
                    {
                        volumeIndex++;
                        continue;
                    }    
                }
                bool bResult = itemVolume.SetVolumeLabel();
                TaskCurrent++;
                TaskDescription = $"Copying {mainViewModel.TotalTasks.FilesCount} Files";
                List<string> copyResult = await Task.Run(() => itemVolume.CopyFiles());
                if (copyResult!=null && copyResult.Count>0)
                {
                    itemVolume.TasksStatus = 2;
                    itemVolume.ErrorDescription = "Error On This Files: " + string.Join(" , ", copyResult);
                    volumeIndex++;
                    continue;
                }
                else
                {
                    itemVolume.TasksStatus = 1;
                    TaskCurrent += mainViewModel.TotalTasks.FilesCount - 1;
                }
                if (mainViewModel.Configuration.Eject)
                {
                    TaskCurrent++;
                    TaskDescription = $"Eject Volume {volumeIndex}";
                    Win32ResponseDataStruct ejectResult = StorageInterfaceHelpers.EjectVolume(itemVolume);
                    // Try Again
                    if (!ejectResult.Status)
                        ejectResult = StorageInterfaceHelpers.EjectVolume(itemVolume);
                    if (!ejectResult.Status)
                    {
                        //itemVolume.TasksStatus = 2;
                        //itemVolume.ErrorDescription = ejectResult.Exception.Message;
                    }
                }
            }
            volumeIndex++;
        }
        if (mainViewModel.Configuration.Eject)
        {
            foreach (DiskDriveInterface itemDiskDrive in USBFlashDevice)
            {
                Win32ResponseDataStruct ejectResult = StorageInterfaceHelpers.EjectDiskDrive(itemDiskDrive);
            }
        }
    }

}
