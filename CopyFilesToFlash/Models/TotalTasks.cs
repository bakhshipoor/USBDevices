using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CopyFilesToFlash.Models;

public class TotalTasks : ViewModelBase
{
    private readonly MainViewModel mainViewModel;

    public TotalTasks(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
    }

    private bool _Format;
    public bool Format
    {
        get { return _Format; }
        set 
        { 
            _Format = value; 
            OnPropertyChanged(nameof(Format));
            mainViewModel.OnTotalTasksChanged();
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
            mainViewModel.OnTotalTasksChanged();
        }
    }

    private uint _FilesCount;
    public uint FilesCount
    {
        get { return _FilesCount; }
        set 
        { 
            _FilesCount = value; 
            OnPropertyChanged(nameof(FilesCount));
            mainViewModel.OnTotalTasksChanged();
        }
    }

    private ulong _TotalFileSize;
    public ulong TotalFileSize
    {
        get { return _TotalFileSize; }
        set 
        { 
            _TotalFileSize = value; 
            OnPropertyChanged(nameof(TotalFileSize));
            mainViewModel.OnTotalTasksChanged();
        }
    }


    public uint GetTotalTasks()
    {
        uint bResponse = 0;
        if (Format)
            bResponse++;
        if (Eject)
            bResponse++;
        bResponse += FilesCount;
        return bResponse;
    }
}
