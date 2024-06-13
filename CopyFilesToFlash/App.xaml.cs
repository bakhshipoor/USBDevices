using CopyFilesToFlash.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Configuration;
using CopyFilesToFlash;

namespace CopyFilesToFlas;


public partial class App : Application
{
    
    private readonly IHost _host;
    public Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

    public App()
    {
        if (AppConfig.Sections["UserConfigurations"] is null)
        {
            AppConfig.Sections.Add("UserConfigurations", new UserConfigurations());
        }
        

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                
                services.AddSingleton<MainViewModel>(new MainViewModel(AppConfig));
                services.AddSingleton((services) => new MainWindow()
                {
                    DataContext = services.GetRequiredService<MainViewModel>()
                });
            })
            .Build();

        
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host.Dispose();
        AppConfig.Save();
        base.OnExit(e);
    }
}
