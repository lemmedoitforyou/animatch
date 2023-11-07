using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;
using AniDAL;
using AniDAL.DbContext;
using AniWPF.StartupHelper;
using AniBLL.Services;

namespace AniWPF;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    public App()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddFormFactory<main>();
            services.AddFormFactory<ChildForm>();
            services.AddFormFactory<MainWindow>();
            services.AddFormFactory<random>();
            services.AddTransient<IAnimeRepository, AnimeRepository>();
            services.AddTransient<IAnimeService, AnimeService>();
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ApplicationDbContext>();
        }).Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }
    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
