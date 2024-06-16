using CopyFilesToFlash.Events;
using CopyFilesToFlash.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using USBDevicesLibrary.Interfaces.Storage;
using USBDevicesLibrary.USBDevices;
using USBDevicesLibrary.Win32API;

namespace CopyFilesToFlash.Models;

public class USBFlashDisk : ViewModelBase
{
    private readonly MainViewModel mainViewModel;
    private const string taskStopped = "Task Stopped";
    public event EventHandler<EventArgs>? TasksFinishedSuccessfully;

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
                        volume.CopyFileFinished += Volume_CopyFileFinished;
                        Volumes.Add(volume);
                        if (volume.IsValid)
                            _VolumeCount++;
                    }
                }
            }
        }
        _TaskDescription = taskStopped;
        _MaxProgressValue = _TaskTotal * _VolumeCount;
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

    private int _TaskPercentage;
    public int TaskPercentage
    {
        get { return _TaskPercentage; }
        set { _TaskPercentage = value; OnPropertyChanged(nameof(TaskPercentage)); }
    }

    private uint _MaxProgressValue;
    public uint MaxProgressValue
    {
        get { return _MaxProgressValue; }
        set { _MaxProgressValue = value; OnPropertyChanged(nameof(MaxProgressValue)); }
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

    private byte _TasksStatus;
    public byte TasksStatus
    {
        get { return _TasksStatus; }
        set { _TasksStatus = value; OnPropertyChanged(nameof(TasksStatus)); }
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

    private void Volume_CopyFileFinished(object? sender, CopyEventArgs e)
    {
        
        TaskDescription = $"Copy File {e.FileToCopy.FileName}";
        if (!e.CopyStatus && e.FileException != null)
        {
            e.Volume.TasksStatus = 2;
            e.Volume.ErrorDescription += $" Error While Copyng File {e.FileToCopy.FileName}, Error: {e.FileException.Message},   ";
        }
        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, ()=> TaskCurrent++);
        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, () => TaskPercentage++);
    }

    internal virtual void OnTasksFinishedSuccessfully(EventArgs e)
    {
        TasksFinishedSuccessfully?.Invoke(this, e);
    }

    public async void StartTasks()
    {
        uint volumeIndex = 1;
        TasksStatus = 0;
        TaskPercentage = 0;
        List<Volume> successfullyFinisedVolumes = [];
        foreach (Volume itemVolume in Volumes)
        {
            VolumeCurrent = volumeIndex;
            if (itemVolume.IsValid)
            {
                TaskCurrent = 1;
                TaskPercentage++;
                // Format
                if (mainViewModel.Configuration.Format)
                {
                    TaskDescription = $"Format Volume {itemVolume.Name}";
                    await Task.Run(() => itemVolume.FormatVolume());
                    // Tray Again
                    if (itemVolume.TasksStatus != 1)
                        await Task.Run(() => itemVolume.FormatVolume());
                    if (itemVolume.TasksStatus == 1)
                        itemVolume.FileSystem = mainViewModel.FileSystemTypes[mainViewModel.Configuration.FileSystemIndex];
                    else
                    {
                        goto EndOfVolumeTask;
                    }    
                }

                // Set Volume Label
                bool bResult = itemVolume.SetVolumeLabel();

                // Copy Files
                //TaskCurrent++;
                //TaskDescription = $"Copying {mainViewModel.TotalTasks.FilesCount} Files";
                await Task.Run(() => itemVolume.CopyFiles());
                if (itemVolume.TasksStatus != 1)
                {
                    goto EndOfVolumeTask;
                }

                // Eject
                if (mainViewModel.Configuration.Eject)
                {
                    TaskCurrent++;
                    TaskPercentage++;
                    TaskDescription = $"Eject Volume {itemVolume.Name}";
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
                successfullyFinisedVolumes.Add(itemVolume);
            }
            EndOfVolumeTask:
            itemVolume.FormatChanged -= Volume_FormatChanged;
            itemVolume.CopyFileFinished -= Volume_CopyFileFinished;
            volumeIndex++;
        }

        if (mainViewModel.Configuration.Eject)
        {
            foreach (DiskDriveInterface itemDiskDrive in USBFlashDevice)
            {
                Win32ResponseDataStruct ejectResult = StorageInterfaceHelpers.EjectDiskDrive(itemDiskDrive);
            }
        }

        if (Volumes.Count==successfullyFinisedVolumes.Count)
        {
            TasksStatus = 1;
            OnTasksFinishedSuccessfully(new EventArgs());
        }
        else
        {
            TaskCurrent = 0;
            TaskPercentage = 0;
            TasksStatus = 2;
        }
    }

}
