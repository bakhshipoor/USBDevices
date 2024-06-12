using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesToFlash.Models;

public class File : ViewModelBase
{
    public File()
    {
		_FileName = string.Empty;
        _FilePath = string.Empty;
    }

	private string _FileName;
	public string FileName
	{
		get { return _FileName; }
		set { _FileName = value; OnPropertyChanged(nameof(FileName)); }
	}

	private string _FilePath;
	public string FilePath
	{
		get { return _FilePath; }
		set { _FilePath = value; OnPropertyChanged(nameof(FilePath)); }
	}

	private int _FileIndex;
	public int FileIndex
	{
		get { return _FileIndex; }
		set { _FileIndex = value; OnPropertyChanged(nameof(FileIndex)); }
	}


}
