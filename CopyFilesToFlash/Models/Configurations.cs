using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesToFlash.Models;

public class Configurations : ViewModelBase
{
    public Configurations()
    {
		_VID = string.Empty;
		_PID = string.Empty;
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

	private bool _Format;
	public bool Format
	{
		get { return _Format; }
		set { _Format = value; OnPropertyChanged(nameof(Format)); }
	}

	private bool _Eject;
	public bool Eject
	{
		get { return _Eject; }
		set { _Eject = value; OnPropertyChanged(nameof(Eject)); }
	}


}
