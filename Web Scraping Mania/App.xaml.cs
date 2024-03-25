using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Web_Scraping_Mania.ViewModels;
using Web_Scraping_Mania.Views;

namespace Web_Scraping_Mania
{
    public partial class App : Application
    {

        public static IHost hosting { get; set; }
        public App()
        {
            hosting = Host.CreateDefaultBuilder().ConfigureServices((hostConext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<AddNewTabViewModel>();
                services.AddSingleton<ParseByTagAndArgViewModel>();
                services.AddSingleton<DownloadFromWebViewModel>();
            }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await hosting.StartAsync();
            var window = hosting.Services.GetRequiredService<MainWindow>();
            window.Show();
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await hosting.StopAsync();

            base.OnExit(e);
        }
    }
}


