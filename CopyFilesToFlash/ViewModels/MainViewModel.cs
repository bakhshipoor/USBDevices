using CopyFilesToFlash.Commands;
using CopyFilesToFlash.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Input;
using USBDevicesLibrary;
using USBDevicesLibrary.USBDevices;

namespace CopyFilesToFlash.ViewModels;

public class MainViewModel : ViewModelBase
{
    public event EventHandler<EventArgs>? FileSelectionChanged;
    public Configuration AppConfig;
    public USBDevicesList USBDevices { get; set; } = new();

    public ICommand AddFiles { get; set; }
    public ICommand RemoveFiles { get; set; }

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

        AddFiles = new AddFilesCommand(this);
        RemoveFiles = new RemoveFilesCommand(this);

        UpdateUSBFilter();
        USBDevices.Start();

    }

    private void USBDevices_DeviceChanged(object? sender, USBDevicesLibrary.Events.USBDevicesEventArgs e)
    {
        USBFlashDisks.Clear();
        foreach (USBDevice itemUSBDevice in USBDevices.USBDevices)
        {
            List<string> drivePaths = itemUSBDevice.GetUSBDeviceVolumPaths();
            foreach (string itemDrivePath in drivePaths)
            {
                USBFlashDisk usbFlashDisk = new(this);
                usbFlashDisk.USBFlashDevice = itemUSBDevice;
                usbFlashDisk.VID = string.Format("{0:X4}", itemUSBDevice.IDVendor).ToUpper();
                usbFlashDisk.PID = string.Format("{0:X4}", itemUSBDevice.IDProduct).ToUpper();
                usbFlashDisk.Path = itemDrivePath;
                USBFlashDisks.Add(usbFlashDisk);
            }
        }
    }

    public Configurations Configuration { get; set; }
    public ObservableCollection<FileToCopy> Files { get; set; }
    public ObservableCollection<USBFlashDisk> USBFlashDisks { get; set; }

    private uint _VolumeLabelMaxLenght;
    public uint VolumeLabelMaxLenght
    {
        get { return _VolumeLabelMaxLenght; }
        set { _VolumeLabelMaxLenght = value; OnPropertyChanged(nameof(VolumeLabelMaxLenght)); }
    }



    public void OnFileSelectionChanged(FileToCopy fileToCopy)
    {
        FileSelectionChanged?.Invoke(this, new EventArgs());
    }

    public void UpdateUSBFilter()
    {
        USBDevices.SetDeviceToFilter(Configuration.VID, Configuration.PID, "USBSTOR");
    }
}
