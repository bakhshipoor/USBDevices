using CopyFilesToFlash.Models;
using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesToFlash.Commands;

public class RemoveFilesCommand :CommandBase
{
    private readonly ObservableCollection<FileToCopy> files;
    private readonly MainViewModel mainViewModel;

    public RemoveFilesCommand(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
        files = mainViewModel.Files;
        mainViewModel.FileSelectionChanged += MainViewModel_FileSelectionChanged; ; ;
    }

    private void MainViewModel_FileSelectionChanged(object? sender, EventArgs e)
    {
        OnCanExecuteChanged();
    }

    public override void Execute(object parameter)
    {
        ObservableCollection<FileToCopy> removeFiles = [];
        foreach (FileToCopy itemFile in files)
        {
            if (itemFile.IsSelected)
                removeFiles.Add(itemFile);
        }
        foreach (FileToCopy itemFile in removeFiles)
        {
            files.Remove(itemFile);
        }
        int memberIndex = 1;
        foreach (FileToCopy itemFile in files)
        {
            itemFile.FileIndex = memberIndex;
            memberIndex++;
        }
        ((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).SetFiles(files);
        mainViewModel.TotalTasks.FilesCount = (uint)files.Count;
        OnCanExecuteChanged();
    }

    public override bool CanExecute(object parameter)
    {
        bool canExecute = false;
        foreach (FileToCopy itemFile in files)
        {
            if (itemFile.IsSelected)
            {
                canExecute = true;
                break;
            }
        }
        return canExecute && base.CanExecute(parameter);
    }
}
