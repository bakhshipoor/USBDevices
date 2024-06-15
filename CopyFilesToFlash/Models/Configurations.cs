using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Interfaces.Storage;

namespace CopyFilesToFlash.Models;

public class Configurations : ViewModelBase
{
    private readonly MainViewModel mainViewModel;

    public Configurations(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
        _VID = string.Empty;
		_PID = string.Empty;
        _VolumeLabel = string.Empty;
    }

	private string _VID;
	public string VID
	{
		get { return _VID; }
		set 
		{
            if (!value.ToArray().All(char.IsAsciiHexDigit))
            {
				return;
            }
            _VID = value.ToUpper(); 
			OnPropertyChanged(nameof(VID)); 
			((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).VID = value;
            mainViewModel.UpdateUSBFilter();
        }
	}

	private string _PID;
	public string PID
	{
		get { return _PID; }
		set 
		{
            if (!value.ToArray().All(char.IsAsciiHexDigit))
            {
                return;
            }
            _PID = value.ToUpper(); 
			OnPropertyChanged(nameof(PID)); 
			((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).PID = value;
            mainViewModel.UpdateUSBFilter();
        }
	}

	private bool _Format;
	public bool Format
	{
		get { return _Format; }
		set 
		{ 
			_Format = value; 
            if (value)
            {
                if (FileSystemIndex == 0)
                    mainViewModel.VolumeLabelMaxLenght = 32;
                else
                    mainViewModel.VolumeLabelMaxLenght = 11;
            }
            else
                mainViewModel.VolumeLabelMaxLenght = 32;
            OnPropertyChanged(nameof(Format)); 
			((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).Format = value;
            mainViewModel.TotalTasks.Format = value;
        }
	}

    private int _FileSystemIndex;
    public int FileSystemIndex
    {
        get { return _FileSystemIndex; }
        set
        {
            _FileSystemIndex = value;
            if (_Format)
            {
                if (value == 0)
                    mainViewModel.VolumeLabelMaxLenght = 32;
                else
                    mainViewModel.VolumeLabelMaxLenght = 11;
            }
            else
                mainViewModel.VolumeLabelMaxLenght = 32;

            OnPropertyChanged(nameof(FileSystemIndex));
            ((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).FileSystemIndex = value;
        }
    }

    private string _VolumeLabel;
    public string VolumeLabel
    {
        get { return _VolumeLabel; }
        set
        {
            if (mainViewModel.VolumeLabelMaxLenght == 11)
                value = StorageInterfaceHelpers.ValidateVolumeLabel(value, true);
            else
                value = StorageInterfaceHelpers.ValidateVolumeLabel(value, false);
            _VolumeLabel = value;
            OnPropertyChanged(nameof(VolumeLabel));
            ((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).VolumeLabel = value;
            mainViewModel.OnVolumeLabelChanged();
        }
    }

    private bool _Eject;
	public bool Eject
	{
		get { return _Eject; }
		set 
		{ 
			_Eject = value; 
			OnPropertyChanged(nameof(Eject)); 
			((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).Eject = value;
            mainViewModel.TotalTasks.Eject = value;
        }
	}


}
