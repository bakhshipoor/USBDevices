using CopyFilesToFlash.Models;
using System.Collections.ObjectModel;

namespace CopyFilesToFlash.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        Configuration = new Configurations();
        Configuration.VID="ABCD";
        Configuration.PID = "WXYZ";
        Configuration.Format = true;
        Configuration.Eject = false;
        Files = [];
        File fi = new() { FileName = "0001.MP3", FilePath = "D:\\New\\", FileIndex = 1 };
        Files.Add(fi);
    }

    public Configurations Configuration { get; set; }
    public ObservableCollection<File> Files { get; set; }
}
