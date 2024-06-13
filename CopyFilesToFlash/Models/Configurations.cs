using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesToFlash.Models;

public class Configurations : ViewModelBase
{
    private readonly MainViewModel mainViewModel;

    public Configurations(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
        _VID = string.Empty;
		_PID = string.Empty;
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
			OnPropertyChanged(nameof(Format)); 
			((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).Format = value; 
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
		}
	}


}
