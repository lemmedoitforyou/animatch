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
using AniWPF.ViewModels;

namespace AniWPF
{
    public partial class ProfileWindow : Window
    {
        private readonly ILogger<ProfileWindow> logger;

        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<RedactWindow> redactFactory;
        private readonly IAbstractFactory<LikedAnimeWindow> likedFactory;
        private readonly IAbstractFactory<SearchWindow> searchFactory;
        private readonly IAbstractFactory<AnimeWindow> animeFactory;

        private readonly IUserService userService;
        private readonly IAddedAnimeService addedAnimeService;
        private readonly IAnimeService animeService;

        private UserViewModel viewModel;
        private int id;
        private List<AnimeForForm> animeList;

        public static int CurrentId { get; set; }

        public ProfileWindow(IUserService userService, IAddedAnimeService addedAnimeService, 
            IAnimeService animeService, IAbstractFactory<RandomWindow> randomFactory,
            IAbstractFactory<MainWindow> mainFactory, IAbstractFactory<RedactWindow> redactFactory,
            ILogger<ProfileWindow> logger, IAbstractFactory<LikedAnimeWindow> likedFactory, 
            IAbstractFactory<SearchWindow> searchFactory, IAbstractFactory<AnimeWindow> animeFactory)
        {
            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;
            this.redactFactory = redactFactory;
            this.searchFactory = searchFactory;
            this.animeFactory = animeFactory;

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
                animeList.Add(new AnimeForForm { Id = anime.Id, Title = anime.Name, ImagePath = anime.Photo });
            }
            animeListView.ItemsSource = animeList;


            this.logger = logger;
            this.logger.LogInformation("ProfileWindow created");
            this.likedFactory = likedFactory;
            this.searchFactory = searchFactory;
        }

        public class AnimeForForm
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ImagePath { get; set; }
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
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            //this.logger.LogInformation("Click Profile button");
            //this.profileFactory.Create(this).Show();
            //this.Close();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Search button");
            this.searchFactory.Create(this).Show();
            this.Close();
        }

        private void ButtonAdded_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click added button");
            this.likedFactory.Create(this).Show();
            this.Close();
        }

        private void AnimeButton_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click detail about anime button");

            if (sender is FrameworkElement button)
            {
                if (button.DataContext is AnimeForForm clickedItem)
                {
                    string temp = clickedItem.Title;
                    AnimeForForm foundAnime = this.animeList.FirstOrDefault(anime => anime.Title == temp);
                    if (foundAnime != null)
                    {
                        CurrentId = foundAnime.Id;
                    }

                    AnimeWindow.ParentWindow = this;
                    this.animeFactory.Create(this).Show();
                    this.Close();
                }
            }
        }

        private void ButtonLiked_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Liked button");
            this.likedFactory.Create(this).Show();
            this.Close();
        }
    }
}