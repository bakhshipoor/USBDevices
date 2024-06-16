using CopyFilesToFlash.Commands;
using CopyFilesToFlash.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Input;
using System.Windows.Threading;
using USBDevicesLibrary;
using USBDevicesLibrary.Events;
using USBDevicesLibrary.Interfaces.Storage;
using USBDevicesLibrary.USBDevices;
using static USBDevicesLibrary.Events.EventTypesEnum;

namespace CopyFilesToFlash.ViewModels;

public class MainViewModel : ViewModelBase
{
    public event EventHandler<EventArgs>? FileSelectionChanged;
    public Configuration AppConfig;
    public USBDevicesList USBDevices { get; set; } = new();

    public ICommand AddFiles { get; set; }
    public ICommand RemoveFiles { get; set; }
    public ICommand Start { get; set; }
    public ICommand Stop { get; set; }

    public List<string> FileSystemTypes { get; } = new() { "NTFS", "FAT","FAT32", "exFAT"};

    public MainViewModel(Configuration _AppConfig)
    {
        AppConfig = _AppConfig;
        //USBDevices = new();
        USBDevices.DeviceChanged -= USBDevices_DeviceChanged;
        USBDevices.DeviceChanged += USBDevices_DeviceChanged;
        USBDevices.FilterDeviceStatus = true;
        USBDevices.CheckInterfacesStatus= true;
        USBDevices.ConnectedEventStatus = true;
        USBDevices.DisconnectedEventStatus = true;

        _TotalTasks = new(this);
        Configuration = new Configurations(this);
        USBFlashDisks = [];
        Files = [];
        VolumeLabelMaxLenght = 32;
        Configuration.VID= ((UserConfigurations)AppConfig.Sections["UserConfigurations"]).VID;
        Configuration.PID = ((UserConfigurations)AppConfig.Sections["UserConfigurations"]).PID;
        Configuration.VolumeLabel = ((UserConfigurations)AppConfig.Sections["UserConfigurations"]).VolumeLabel;
        Configuration.Format = ((UserConfigurations)AppConfig.Sections["UserConfigurations"]).Format;
        Configuration.FileSystemIndex = ((UserConfigurations)AppConfig.Sections["UserConfigurations"]).FileSystemIndex;
        Configuration.Eject = ((UserConfigurations)AppConfig.Sections["UserConfigurations"]).Eject;
        Files = ((UserConfigurations)AppConfig.Sections["UserConfigurations"]).GetFiles(this);

        TotalTasks.Format = Configuration.Format;
        TotalTasks.Eject = Configuration.Eject;
        TotalTasks.FilesCount = (uint)Files.Count;

        AddFiles = new AddFilesCommand(this);
        RemoveFiles = new RemoveFilesCommand(this);
        Start = new StartCommand(this);
        Stop = new StopCommand(this);
        
        UpdateUSBFilter();
        USBDevices.Start();
    }

    private void USBDevices_DeviceChanged(object? sender, USBDevicesLibrary.Events.USBDevicesEventArgs e)
    {
        if (sender!=null)
        {
            if (e.EventType== EventTypeEnum.Connected)
            {
                USBFlashDisk newUSBFlashDevice= new(this, e.Device);
                newUSBFlashDevice.TasksFinishedSuccessfully += NewUSBFlashDevice_TasksFinishedSuccessfully;
                USBFlashDisks.Add(newUSBFlashDevice);
                if (StartStatus)
                {
                    Task.Run(newUSBFlashDevice.StartTasks);
                }
            }
            else if (e.EventType == EventTypeEnum.Disconnected)
            {
                USBFlashDisk? usbFlashDiskToRemove = null;
                foreach (USBFlashDisk itemFlashDisk in USBFlashDisks)
                {
                    if (itemFlashDisk.USBFlashDevice.BaseDeviceProperties.Device_InstanceId.Equals(e.Device.BaseDeviceProperties.Device_InstanceId,StringComparison.OrdinalIgnoreCase))
                    {
                        usbFlashDiskToRemove = itemFlashDisk;
                        break;
                    }
                }
                if ( usbFlashDiskToRemove != null)
                    USBFlashDisks.Remove(usbFlashDiskToRemove);
            }
        }
    }

    private void NewUSBFlashDevice_TasksFinishedSuccessfully(object? sender, EventArgs e)
    {
        if (sender!=null && sender is USBFlashDisk)
        {
            USBFlashDisk usbFlashDiskToRemove = (USBFlashDisk)sender;
            usbFlashDiskToRemove.TasksFinishedSuccessfully -= NewUSBFlashDevice_TasksFinishedSuccessfully;
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.DataBind, () => USBFlashDisks.Remove(usbFlashDiskToRemove));
            //await Task.Run(() => { USBFlashDisks.Remove(usbFlashDiskToRemove); });

        }
    }

    public Configurations Configuration { get; set; }
    public ObservableCollection<FileToCopy> Files { get; set; }
    public ObservableCollection<USBFlashDisk> USBFlashDisks { get; set; }

    private uint _VolumeLabelMaxLenght;
    public uint VolumeLabelMaxLenght
    {
        get { return _VolumeLabelMaxLenght; }
        set 
        { 
            _VolumeLabelMaxLenght = value; 
            if (value == 11)
            {
                Configuration.VolumeLabel = StorageInterfaceHelpers.ValidateVolumeLabel(Configuration.VolumeLabel, true);
            }
            OnPropertyChanged(nameof(VolumeLabelMaxLenght)); 
        }
    }

    private TotalTasks _TotalTasks;
    public TotalTasks TotalTasks
    {
        get { return _TotalTasks; }
        set { _TotalTasks = value; }
    }

    private bool _StartStatus;
    public bool StartStatus
    {
        get { return _StartStatus; }
        set { _StartStatus = value; OnPropertyChanged(nameof(StartStatus)); }
    }



    public void OnFileSelectionChanged(FileToCopy fileToCopy)
    {
        FileSelectionChanged?.Invoke(this, new EventArgs());
    }

    public void OnTotalTasksChanged()
    {
        foreach (USBFlashDisk itemFlashDisks in USBFlashDisks)
        {
            itemFlashDisks.TaskTotal = TotalTasks.GetTotalTasks();
            itemFlashDisks.VolumeCount = 0;
            foreach (Volume itemVolume in itemFlashDisks.Volumes)
            {
                itemVolume.CheckVolumeIsValid();
                if (itemVolume.IsValid)
                    itemFlashDisks.VolumeCount++;
            }
            itemFlashDisks.MaxProgressValue = itemFlashDisks.TaskTotal * itemFlashDisks.VolumeCount;
        }
    }

    public void OnVolumeLabelChanged()
    {
        foreach (USBFlashDisk itemFlashDisks in USBFlashDisks)
        {
            foreach (Volume itemVolume in itemFlashDisks.Volumes)
            {
                itemVolume.ValidateNewVolumeLabel();
            }
        }
    }

    public void UpdateUSBFilter()
    {
        USBDevices.SetDeviceToFilter(Configuration.VID, Configuration.PID, "USBSTOR");
    }
}
