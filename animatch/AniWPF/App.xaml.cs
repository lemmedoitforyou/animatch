using System.Windows;
using AniBLL.Services;
using AniDAL;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;
using AniDAL.Repositories;
using AniWPF.StartupHelper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AniWPF;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    private readonly ILogger<App> _logger;

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
           .UseSerilog((host, loggerConfiguration) =>
           {
               loggerConfiguration.WriteTo.File("logs\\test.txt", rollingInterval: RollingInterval.Day)
                   .WriteTo.Debug()
                   .MinimumLevel.Error()
                   .MinimumLevel.Override("LoggingDemo.Commands", Serilog.Events.LogEventLevel.Debug);
           })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<LogInWindow>();
                services.AddFormFactory<MainWindow>();
                services.AddFormFactory<RegistrationWindow>();
                services.AddFormFactory<LogInWindow>();
                services.AddFormFactory<RandomWindow>();
                services.AddFormFactory<ProfileWindow>();
                services.AddFormFactory<LikedAnimeWindow>();
                services.AddFormFactory<RedactWindow>();
                services.AddFormFactory<SearchWindow>();

                services.AddTransient<ApplicationDbContext>();

                services.AddTransient<IAnimeRepository, AnimeRepository>();
                services.AddTransient<IAnimeService, AnimeService>();

                services.AddTransient<IUserInfoRepository, UserInfoRepository>();
                services.AddTransient<IUserService, UserService>();

                services.AddTransient<IAddedAnimeService, AddedAnimeService>();
                services.AddTransient<IAddedAnimeRepository, AddedAnimeRepository>();

                services.AddTransient<ILikedAnimeRepository, LikedAnimeRepository>();
                services.AddTransient<ILikedAnimeService, LikedAnimeService>();

                services.AddTransient<IDislikedAnimeRepository, DislikedAnimeRepository>();
                services.AddTransient<IDislikedAnimeService, DislikedAnimeService>();

                services.AddTransient<IAddedAnimeRepository, AddedAnimeRepository>();
                services.AddTransient<IAddedAnimeService, AddedAnimeService>();

                services.AddTransient<IWatchedAnimeRepository, WatchedAnimeRepository>();
                services.AddTransient<IWatchedAnimeService, WatchedAnimeService>();
               
                services.AddTransient<IAnimeGenreRepository, AnimeGenreRepository>();
                services.AddTransient<IAnimeGenreService, AnimeGenreService>();
            }).Build();

        _logger = AppHost.Services.GetRequiredService<ILogger<App>>();
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
