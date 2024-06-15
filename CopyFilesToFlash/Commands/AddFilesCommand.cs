using CopyFilesToFlash.Models;
using CopyFilesToFlash.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CopyFilesToFlash.Commands;

public class AddFilesCommand : CommandBase
{
    private readonly ObservableCollection<FileToCopy> files;
    private readonly MainViewModel mainViewModel;

    public AddFilesCommand(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
        files = mainViewModel.Files;
    }

    public override void Execute(object parameter)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "All files (*.*)|*.*";
        openFileDialog.Multiselect = true;
        openFileDialog.Title = "Select Files To Add To Copy List";
        if (openFileDialog.ShowDialog()==true)
        {
            int memberIndex = 0;
            foreach (string itemFilePath in openFileDialog.FileNames)
            {
                if (!CheckFileIsExistInList(itemFilePath))
                {
                    FileToCopy file = new(mainViewModel);
                    file.FilePath = itemFilePath;
                    file.FileName = openFileDialog.SafeFileNames[memberIndex];
                    file.FileSize = new FileInfo(itemFilePath).Length;
                    files.Add(file);
                }
                memberIndex++;
            }
            memberIndex = 1;
            ulong totalFileSize = 0;
            foreach (FileToCopy itemFile in files)
            {
                itemFile.FileIndex = memberIndex;
                totalFileSize += (ulong)itemFile.FileSize;
                memberIndex++;
            }
            ((UserConfigurations)mainViewModel.AppConfig.Sections["UserConfigurations"]).SetFiles(files);
            mainViewModel.TotalTasks.FilesCount = (uint)files.Count;
            mainViewModel.TotalTasks.TotalFileSize = totalFileSize;
        }
    }

    private bool CheckFileIsExistInList(string fileName)
    {
        bool bResponse = false;
        foreach (FileToCopy itemFile in files)
        {
            if (itemFile.FilePath.Equals(fileName,StringComparison.OrdinalIgnoreCase))
            {
                bResponse = true;
                break;
            }
        }
        return bResponse;
    }
}
