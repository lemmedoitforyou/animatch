using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AniBLL.Models;
using AniBLL.Services;
using AniWPF.StartupHelper;
using Microsoft.Extensions.Logging;

namespace AniWPF
{
    public partial class LikedAnimeWindow : Window
    {
        public readonly ILogger<LikedAnimeWindow> logger;

        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<SearchWindow> searchFactory;
        public readonly IAbstractFactory<AnimeWindow> animeFactory;

        private readonly ILikedAnimeService likedAnimeService;

        private int id;
        private List<AnimeForForw> animeList;

        public static int CurrentId { get; set; }

        public LikedAnimeWindow(ILikedAnimeService likedAnimeService, IAbstractFactory<RandomWindow> randomFactory,
                                IAbstractFactory<MainWindow> mainFactory, IAbstractFactory<ProfileWindow> profileFactory,
                                IAbstractFactory<SearchWindow> searchFactory, IAbstractFactory<AnimeWindow> animeFactory,
                                ILogger<LikedAnimeWindow> logger)
        {
            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;
            this.profileFactory = profileFactory;
            this.searchFactory = searchFactory;
            this.animeFactory = animeFactory;

            this.likedAnimeService = likedAnimeService;

            this.id = LogInWindow.CurrentUserID;

            List<AnimeModel> temp = likedAnimeService.GetLikedAnimesForUser(this.id);

            this.animeList = new List<AnimeForForw>();
            foreach (AnimeModel anime in temp)
            {
                this.animeList.Add(new AnimeForForw { Id = anime.Id, Title = anime.Name, ImagePath = anime.Photo });
            }

            this.animeItemsControl.ItemsSource = this.animeList;

            this.logger = logger;
            this.logger.LogInformation("LikedAnimeWindow created");
            this.logger.LogInformation("List of liked anime was shown");
        }

        public class AnimeForForw
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string ImagePath { get; set; }
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
            this.logger.LogInformation("Click Profile button");
            this.profileFactory.Create(this).Show();
            this.Close();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Search button");
            this.searchFactory.Create(this).Show();
            this.Close();
        }

        private void AnimeButton_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click detail about anime button");

            if (sender is FrameworkElement button)
            {
                if (button.DataContext is AnimeForForw clickedItem)
                {
                    string temp = clickedItem.Title;
                    AnimeForForw foundAnime = this.animeList.FirstOrDefault(anime => anime.Title == temp);
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
    }
}
