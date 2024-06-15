using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesToFlash.Commands;

public class StopCommand : AsyncCommandBase
{
    private readonly MainViewModel mainViewModel;

    public StopCommand(MainViewModel _MainViewModel)
    {
        mainViewModel = _MainViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        mainViewModel.StartStatus = false;
    }
}
