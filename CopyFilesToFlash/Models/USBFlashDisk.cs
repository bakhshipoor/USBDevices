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

    public USBFlashDisk(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
        _Path = string.Empty;
        _VID = string.Empty;
        _PID = string.Empty;
        _USBFlashDevice = new();
    }

    private string _Path;
    public string Path
    {
        get { return _Path; }
        set { _Path = value; OnPropertyChanged(nameof(Path)); }
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

    private USBDevice _USBFlashDevice;
    public USBDevice USBFlashDevice
    {
        get { return _USBFlashDevice; }
        set { _USBFlashDevice = value; }
    }

}
