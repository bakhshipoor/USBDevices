using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CopyFilesToFlash.Models;

[Serializable]
public class FileToCopy : ViewModelBase
{
	private readonly MainViewModel mainViewModel;
    public FileToCopy(MainViewModel _MainViewModel)
    {
		_FileName = string.Empty;
        _FilePath = string.Empty;
		mainViewModel = _MainViewModel;
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

	private bool _IsSelected;
	public bool IsSelected
	{
		get { return _IsSelected; }
		set { _IsSelected = value; OnPropertyChanged(nameof(IsSelected)); mainViewModel.OnFileSelectionChanged(this); }
	}


}
