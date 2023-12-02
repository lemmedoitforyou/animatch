using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AniBLL.Models;
using AniBLL.Services;
using AniWPF.StartupHelper;
using AniWPF;
using Microsoft.Extensions.Logging;

namespace AniWPF
{
    public partial class ProfileWindow : Window
    {
        private readonly ILogger<ProfileWindow> logger;

        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<RedactWindow> redactFactory;

        private readonly IUserService userService;
        private readonly IAddedAnimeService addedAnimeService;
        private readonly IAnimeService animeService;

        private UserViewModel viewModel;
        private int id;
        private List<AnimeForForm> animeList;


        public ProfileWindow(IUserService userService, IAddedAnimeService addedAnimeService, 
            IAnimeService animeService, IAbstractFactory<RandomWindow> randomFactory, 
            IAbstractFactory<MainWindow> mainFactory, IAbstractFactory<RedactWindow> redactFactory,
            ILogger<ProfileWindow> logger)
        {
            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;
            this.redactFactory = redactFactory;

            this.userService = userService;
            this.addedAnimeService = addedAnimeService;
            this.animeService = animeService;

            System.Random random = new System.Random();
            this.id = LogInWindow.CurrentUserID;
            this.viewModel = new UserViewModel(this.userService, this.id);
            this.DataContext = this.viewModel;
            List<AnimeModel> temp = addedAnimeService.GetAddedAnimesForUser(this.id);

            animeList = new List<AnimeForForm>();
            foreach (AnimeModel anime in temp)
            {
                animeList.Add(new AnimeForForm { Title = anime.Name, ImagePath = anime.Photo });
            }
            animeListView.ItemsSource = animeList;

            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.logger = logger;
            this.logger.LogInformation("ProfileWindow created");
        }

        public class AnimeForForm
        {
            public string Title { get; set; }
            public string ImagePath { get; set; }
        }
        public class UserViewModel : INotifyPropertyChanged
        {
            private readonly IUserService userService;
            private int id;

            public UserViewModel(IUserService userService, int id)
            {
                this.userService = userService;
                this.id = id;
            }

            public string UserName
            {
                get { return this.userService.GetById(this.id).Name; }
                set { }
            }

            public string UserText
            {
                get { return this.userService.GetById(this.id).Text; }
                set { }
            }

            public string UserLevel
            {
                get
                {
                    int level = this.userService.GetById(this.id).Level;
                    switch (level)
                    {
                        case 1:
                            return "новачок";
                        case 2:
                            return "досвічений анімешник";
                        case 3:
                            return "любитель конкретних жанрів";
                        default:
                            return "лох";
                    }
                }

                set{}
            }

            public string UserPhoto
            {
                get
                {
                    return this.userService.GetById(this.id).Photo;
                }
            }

            public int UserWachedCount
            {
                get
                {
                    return this.userService.GetById(this.id).WatchedCount;
                }
                set { }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class AnimeViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService animeService;
            private readonly IAddedAnimeService addedAnimeService;

            private int id;

            public AnimeViewModel(IAnimeService animeService, int id, IAddedAnimeService addedAnimeService)
            {
                this.animeService = animeService;
                this.id = id;
                this.addedAnimeService = addedAnimeService;
            }

            public string AnimeName
            {
                get { return this.animeService.GetById(this.id).Name; }
            }

            public string AnimeText
            {
                get { return this.animeService.GetById(this.id).Text; }
            }

            public double AnimeRate
            {
                get
                {
                    return this.animeService.GetById(this.id).Imdbrate;
                }
                set
                {
                    this.OnPropertyChanged(nameof(this.AnimeRate));
                }
            }

            public string AnimePhoto
            {
                get
                {
                    return this.animeService.GetById(this.id).Photo;
                }
            }

            public int AnimeYear
            {
                get
                {
                    return this.animeService.GetById(this.id).Year;
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RedactClick(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Redact button");
            this.redactFactory.Create(this).Show();
            this.Close();
        }
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Random button");
            this.randomFactory.Create(this).Show();
            this.Close();
        }
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Main button");
            this.mainFactory.Create(this).Show();
            this.Close();
        }
        private void ButtonAdded_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Added button");
            // Your code for the ButtonAdded_Click event handler
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Added button");
            // Your code for the ButtonAdded_Click event handler
        }
    }

}