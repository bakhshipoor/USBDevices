using CopyFilesToFlash.Commands;
using CopyFilesToFlash.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CopyFilesToFlas;

namespace CopyFilesToFlash.ViewModels;

public class MainViewModel : ViewModelBase
{
    public event EventHandler<EventArgs>? FileSelectionChanged;
    public Configuration AppConfig;
    public MainViewModel(Configuration _AppConfig)
    {
        AppConfig = _AppConfig;
        UserConfigurations configs = (UserConfigurations)AppConfig.GetSection("UserConfigurations");
        Configuration = new Configurations(this);
        Files = [];

        Configuration.VID= configs.VID;
        Configuration.PID = configs.PID;
        Configuration.Format = configs.Format;
        Configuration.Eject = configs.Eject;

        
        Files = configs.GetFiles(this);

        AddFiles = new AddFilesCommand(this);
        RemoveFiles = new RemoveFilesCommand(this);
    }

    

    public Configurations Configuration { get; set; }
    public ObservableCollection<FileToCopy> Files { get; set; }
    public ICommand AddFiles { get; set; }
    public ICommand RemoveFiles { get; set; }

    public void OnFileSelectionChanged(FileToCopy fileToCopy)
    {
        FileSelectionChanged?.Invoke(this, new EventArgs());
    }
}
