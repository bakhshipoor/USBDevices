﻿using CopyFilesToFlash.Models;
using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBDevicesLibrary.Interfaces.Storage;
using USBDevicesLibrary.USBDevices;

namespace CopyFilesToFlash.Commands;

public class StartCommand : AsyncCommandBase
{
    private readonly MainViewModel mainViewModel;

    public StartCommand(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        mainViewModel.StartStatus = true;
        List<Task> tasks = [];
        foreach (USBFlashDisk itemFlashDisk in mainViewModel.USBFlashDisks)
        {
            if (itemFlashDisk.TaskCurrent==0)
                tasks.Add(Task.Run(itemFlashDisk.StartTasks));
        }
        await Task.WhenAll(tasks);
    }
}