using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.USBDevices;

namespace CopyFilesToFlash.Models;

public class USBFlashDisk : ViewModelBase
{
    private readonly MainViewModel mainViewModel;
    private const string taskStopped = "Task Stopped";

    public USBFlashDisk(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
        _VID = string.Empty;
        _PID = string.Empty;
        _VolumePaths = [];
        _TaskDescription = taskStopped;
        _ErrorDescription = string.Empty;
        _USBFlashDevice = new();
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

    private List<string> _VolumePaths;
    public List<string> VolumePaths
    {
        get { return _VolumePaths; }
        set { _VolumePaths = value; OnPropertyChanged(nameof(VolumePaths)); }
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

    private int _TaskCurrent;
    public int TaskCurrent
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

    private bool _TasksFinished;
    public bool TasksFinished
    {
        get { return _TasksFinished; }
        set { _TasksFinished = value; OnPropertyChanged(nameof(TasksFinished)); }
    }

    private bool _TasksIsSuccessfull;
    public bool TasksIsSuccessfull
    {
        get { return _TasksIsSuccessfull; }
        set { _TasksIsSuccessfull = value; OnPropertyChanged(nameof(TasksIsSuccessfull)); }
    }

    private string _ErrorDescription;
    public string ErrorDescription
    {
        get { return _ErrorDescription; }
        set { _ErrorDescription = value; OnPropertyChanged(nameof(ErrorDescription)); }
    }

    private bool _IsSelected;
    public bool IsSelected
    {
        get { return _IsSelected; }
        set { _IsSelected = value; OnPropertyChanged(nameof(IsSelected)); }
    }



    private USBDevice _USBFlashDevice;
    public USBDevice USBFlashDevice
    {
        get { return _USBFlashDevice; }
        set { _USBFlashDevice = value; }
    }

}
