using AniBLL.Models;
using AniBLL.Services;
using AniWPF.StartupHelper;
using AniWPF.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AniWPF.Windows
{
    public partial class ViewProfileWindow : Window
    {
        private readonly ILogger<ViewProfileWindow> logger;

        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<RedactWindow> redactFactory;
        private readonly IAbstractFactory<LikedAnimeWindow> likedFactory;
        private readonly IAbstractFactory<SearchWindow> searchFactory;
        private readonly IAbstractFactory<AnimeWindow> animeFactory;
        private readonly IAbstractFactory<LogInWindow> loginFactory;

        private readonly IUserService userService;
        private readonly IAddedAnimeService addedAnimeService;
        private readonly IAnimeService animeService;

        private UserViewModel viewModel;
        private int id;
        private int viewuserid;
        private List<AnimeForForm> animeList;

        public static int CurrentId { get; set; }

        public ViewProfileWindow(IUserService userService, IAddedAnimeService addedAnimeService,
            IAnimeService animeService, IAbstractFactory<RandomWindow> randomFactory,
            IAbstractFactory<MainWindow> mainFactory, IAbstractFactory<RedactWindow> redactFactory,
            ILogger<ViewProfileWindow> logger, IAbstractFactory<LikedAnimeWindow> likedFactory,
            IAbstractFactory<SearchWindow> searchFactory, IAbstractFactory<AnimeWindow> animeFactory,
            IAbstractFactory<LogInWindow> loginFactory)
        {
            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;
            this.redactFactory = redactFactory;
            this.searchFactory = searchFactory;
            this.animeFactory = animeFactory;
            this.loginFactory = loginFactory;

            this.userService = userService;
            this.addedAnimeService = addedAnimeService;
            this.animeService = animeService;

            System.Random random = new System.Random();
            this.id = LogInWindow.CurrentUserID;
            this.viewuserid = AnimeWindow.UserId;
            this.viewModel = new UserViewModel(this.userService, this.viewuserid);
            this.DataContext = this.viewModel;
            List<AnimeModel> temp = addedAnimeService.GetAddedAnimesForUser(this.viewuserid);

            animeList = new List<AnimeForForm>();
            foreach (AnimeModel anime in temp)
            {
                animeList.Add(new AnimeForForm { Id = anime.Id, Title = anime.Name, ImagePath = anime.Photo });
            }
            animeItemsControl.ItemsSource = animeList;


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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Close button");
            this.animeFactory.Create(this).Show();
            this.Close();
        }
    }
}

