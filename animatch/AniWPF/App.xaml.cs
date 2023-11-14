using System.Windows;
using AniBLL.Services;
using AniDAL;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;
using AniDAL.Repositories;
using AniWPF.StartupHelper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AniWPF;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<LogInWindow>();
            services.AddFormFactory<MainWindow>();
            services.AddFormFactory<RegistrationWindow>();
            services.AddFormFactory<LogInWindow>();
            services.AddFormFactory<RandomWindow>();
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

        var startupForm = AppHost.Services.GetRequiredService<LogInWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
