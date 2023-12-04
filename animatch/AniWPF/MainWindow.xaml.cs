using System.ComponentModel;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;
using System;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AniBLL.Models;
using AniWPF;
using Microsoft.Extensions.Logging;

namespace AniWPF
{
    public partial class MainWindow : Window, IWindowAware
    {
        private readonly ILogger<MainWindow> logger;

        public Window ParentWindow { get; set; }
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAbstractFactory<LikedAnimeWindow> likedFactory;
        private readonly IAbstractFactory<SearchWindow> searchFactory;
        private readonly IAbstractFactory<AnimeWindow> animeFactory;

        private readonly IAddedAnimeService addedAnimeService;
        private readonly ILikedAnimeService likedAnimeService;
        private readonly IDislikedAnimeService dislikedAnimeService;
        private readonly IWatchedAnimeService watchAnimeService;
        private readonly IAnimeService animeService;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;

        private AnimeViewModel viewModel;
        private int id;
        public static int randomAnimeId { get; set; }

        private List<AnimeModel> uniqueAnimes;
        private List<AnimeModel> dislikedanimes;
        private List<AnimeModel> likedanimes;
        private List<AnimeModel> addedanimes;
        private List<AnimeModel> watchedanimes;

        public MainWindow(IAnimeService animeService, IAddedAnimeService addedAnimeService,
            IDislikedAnimeService dislikedAnimeService, ILikedAnimeService likedAnimeService,
            IWatchedAnimeService watchedAnimeService, IUserService userService,
            IAbstractFactory<RandomWindow> rfactory, IAbstractFactory<ProfileWindow> profileFactory,
            IAbstractFactory<LikedAnimeWindow> likedFactory, IAbstractFactory<SearchWindow> searchFactory,
            IAbstractFactory<AnimeWindow> animeWindow, IReviewService reviewService, ILogger<MainWindow> logger)
        {
            this.animeService = animeService;
            this.randomFactory = rfactory;
            this.likedFactory = likedFactory;
            this.profileFactory = profileFactory;
            this.searchFactory = searchFactory;
            this.animeFactory = animeWindow;

            this.addedAnimeService = addedAnimeService;
            this.likedAnimeService = likedAnimeService;
            this.dislikedAnimeService = dislikedAnimeService;
            this.watchAnimeService = watchedAnimeService;
            this.userService = userService;
            this.reviewService = reviewService;

            this.id = LogInWindow.CurrentUserID;
            List<AnimeModel> animes = animeService.GetAll();
            this.dislikedanimes = dislikedAnimeService.GetDislikedAnimesForUser(id);
            this.likedanimes = likedAnimeService.GetLikedAnimesForUser(id);
            this.addedanimes = addedAnimeService.GetAddedAnimesForUser(id);
            this.watchedanimes = watchedAnimeService.GetWatchedAnimesForUser(id);

            this.uniqueAnimes = animes
                .Except(dislikedanimes)
                .Except(likedanimes)
                .Except(addedanimes)
                .Except(watchedanimes).ToList();

            Random random = new Random();

            randomAnimeId = random.Next(uniqueAnimes.Count);

            this.viewModel = new AnimeViewModel(this.animeService, randomAnimeId);
            this.DataContext = this.viewModel;

            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.logger = logger;
            this.logger.LogInformation("MainWindow created");
        }


        public class AnimeViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService animeService;

            private int id;
            public AnimeViewModel(IAnimeService animeService, int id)
            {
                this.animeService = animeService;
                this.id = id;
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
                get { return Math.Round(this.animeService.GetById(this.id).Imdbrate, 2); }
            }

            public string AnimePhoto
            {
                get { return this.animeService.GetById(this.id).Photo; }
            }

            public int AnimeYear
            {
                get { return this.animeService.GetById(this.id).Year; }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Random button");
            this.randomFactory.Create(this).Show();
            this.Close();
        }
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Profile button");
            this.profileFactory.Create(this).Show();
            this.Close();
        }

        private void ButtonLiked_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Liked button");
            this.likedFactory.Create(this).Show();
            this.Close();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Search button");
            this.searchFactory.Create(this).Show();
            this.Close();
        }

        private void Watched_Button_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Watched button");
            WatchedAnimeModel temp = new WatchedAnimeModel
            {
                Id = watchAnimeService.GetLastId() + 1,
                AnimeId = randomAnimeId,
                UserId = this.id
            };
            watchAnimeService.Insert(temp);
            userService.WatchAnime(this.id);

            SendButton.Visibility = Visibility.Visible;
            RatingSlider.Visibility = Visibility.Visible;
            ReviewText.Visibility = Visibility.Visible;
        }
        private async void LikeAnime_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click LikeAnime button");
            likeUnfill.Source = new BitmapImage(new Uri("https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/LikedFillIcon.png?raw=true"));

            LikedAnimeModel temp = new LikedAnimeModel
            {
                Id = likedAnimeService.GetLastUserId() + 1,
                UserId = this.id,
                AnimeId = randomAnimeId
            };
            likedAnimeService.Insert(temp);

            await Task.Delay(1000);

            UploadNextAnime();
        }

        private void Dislike_Button_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Dislike button");
            DislikedAnimeModel temp = new DislikedAnimeModel
            {
                Id = dislikedAnimeService.GetLastId() + 1,
                AnimeId = randomAnimeId,
                UserId = this.id
            };
            dislikedAnimeService.Insert(temp);

            UploadNextAnime();
        }

        private void UploadNextAnime()
        {
            this.uniqueAnimes.RemoveAt(randomAnimeId);
            Random random = new Random();
            randomAnimeId = random.Next(uniqueAnimes.Count);
            this.viewModel = new AnimeViewModel(this.animeService, randomAnimeId);

            this.DataContext = this.viewModel;
            this.logger.LogInformation("Anime:" + animeService.GetById(randomAnimeId).Name + " was shown");
        }

        private void SendReview_Click(object sender, RoutedEventArgs e)
        {
            string text = ReviewText.Text;
            int rate = (int)RatingSlider.Value;
            ReviewModel temp = new ReviewModel()
            {
                Id = reviewService.GetLastId() + 1,
                UserId = this.id,
                AnimeId = randomAnimeId,
                Text = text,
                Rate = rate
            };
            reviewService.Insert(temp);

            UploadNextAnime();

            SendButton.Visibility = Visibility.Collapsed;
            RatingSlider.Visibility = Visibility.Collapsed;
            ReviewText.Visibility = Visibility.Collapsed;
        }
        
        private void AnimeButton_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Anime button");
            this.animeFactory.Create(this).Show();
            this.Close();
        }
    }
}