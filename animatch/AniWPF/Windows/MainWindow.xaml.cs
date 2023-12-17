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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using AniWPF.ViewModels;

namespace AniWPF
{
    public partial class MainWindow : Window
    {
        public ILogger<MainWindow> logger { get; private set; }

        public  static Window? ParentWindow { get; set; }
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAbstractFactory<LikedAnimeWindow> likedFactory;
        private readonly IAbstractFactory<SearchWindow> searchFactory;
        public IAbstractFactory<AnimeWindow> animeFactory { get; private set; }

        private readonly IAddedAnimeService addedAnimeService;
        private readonly ILikedAnimeService likedAnimeService;
        private readonly IDislikedAnimeService dislikedAnimeService;
        private readonly IWatchedAnimeService watchAnimeService;
        private readonly IAnimeService animeService;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;
        private readonly IGenreService genreService;
        private readonly IAnimeGenreService animeGenreService;

        private AnimeViewModel viewModel;
        private int id;
        private int UserRate;
            //private bool isButtonPressed = false;
        public static int randomAnimeId { get; set; }
        private List<Genres> genreList;

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
            IAbstractFactory<AnimeWindow> animeWindow, IReviewService reviewService, 
            IAnimeGenreService animeGenreService, IGenreService genreService, ILogger<MainWindow> logger)
        {
            this.randomFactory = rfactory;
            this.likedFactory = likedFactory;
            this.profileFactory = profileFactory;
            this.searchFactory = searchFactory;
            this.animeFactory = animeWindow;

            this.animeService = animeService;
            this.addedAnimeService = addedAnimeService;
            this.likedAnimeService = likedAnimeService;
            this.dislikedAnimeService = dislikedAnimeService;
            this.watchAnimeService = watchedAnimeService;
            this.userService = userService;
            this.reviewService = reviewService;
            this.genreService = genreService;
            this.animeGenreService = animeGenreService;

            this.id = LogInWindow.CurrentUserID;
            List<AnimeModel> animes = animeService.GetAll();
            this.dislikedanimes = dislikedAnimeService.GetDislikedAnimesForUser(id);
            this.likedanimes = likedAnimeService.GetLikedAnimesForUser(id);
            this.addedanimes = addedAnimeService.GetAddedAnimesForUser(id);
            this.watchedanimes = watchedAnimeService.GetWatchedAnimesForUser(id);
            this.UserRate = 0;

            this.uniqueAnimes = animes
                .Except(dislikedanimes)
                .Except(likedanimes)
                .Except(addedanimes)
                .Except(watchedanimes).ToList();

            Random random = new Random();

            if (ParentWindow != null)
            {
                if (ParentWindow.GetType() == typeof(AnimeWindow))
                {
                    randomAnimeId = AnimeWindow.AnimeId;
                }
            }
            else
            {
                randomAnimeId = random.Next(uniqueAnimes.Count);
            }
            

            this.viewModel = new AnimeViewModel(this.animeService, this.addedAnimeService, this.animeGenreService, randomAnimeId);
            this.DataContext = this.viewModel;

            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.logger = logger;
            UserRate = 0;
            this.logger.LogInformation("MainWindow created");
        }
        public class Genres
        {
            public string GenreName { get; set; }
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

            UserInfoModel newUserRank = userService.GetById(this.id);
            if (newUserRank.WatchedCount < 10)
            {
                newUserRank.Level = 1;
            }
            else if (newUserRank.WatchedCount < 30)
            {
                newUserRank.Level = 2;
            }
            else
            {
                newUserRank.Level = 3;
            }
            userService.UpdateLevel(this.id, newUserRank.Level);

            AnimeTextBlock.Visibility = Visibility.Collapsed;
            SendButton.Visibility = Visibility.Visible;
            //RatingSlider.Visibility = Visibility.Visible;
            StackPanelForRate.Visibility = Visibility.Visible;
            SadSmileButton.Visibility = Visibility.Visible;
            SadSmile.Visibility = Visibility.Visible;
            NormSmileButton.Visibility = Visibility.Visible;
            NormSmile.Visibility = Visibility.Visible;
            HappySmileButton.Visibility = Visibility.Visible;
            HappySmile.Visibility = Visibility.Visible;
            ReviewText.Visibility = Visibility.Visible;
            AddToProfileButton.Visibility = Visibility.Visible;
            BorderForSendBox.Visibility = Visibility.Visible;
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
            likeUnfill.Source = new BitmapImage(new Uri("https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/LikedIcon.png?raw=true"));
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
            this.viewModel = new AnimeViewModel(this.animeService, this.addedAnimeService, this.animeGenreService, randomAnimeId);

            this.DataContext = this.viewModel;
            this.logger.LogInformation("Anime:" + animeService.GetById(randomAnimeId).Name + " was shown");
        }

        private void SendReview_Click(object sender, RoutedEventArgs e)
        {
            string text = ReviewText.Text;
            int rate = UserRate;
            ReviewModel temp = new ReviewModel()
            {
                Id = reviewService.GetLastId() + 1,
                UserId = this.id,
                AnimeId = randomAnimeId,
                Text = text,
                Rate = rate
            };
            reviewService.Insert(temp);
            UserRate = 0;
            HappySmileButton.RenderTransform = new ScaleTransform(1.0, 1.0);
            NormSmileButton.RenderTransform = new ScaleTransform(1.0, 1.0);
            SadSmileButton.RenderTransform = new ScaleTransform(1.0, 1.0);
            ReviewText.Text = "Введіть ваш відгук";
            UploadNextAnime();

            SendButton.Visibility = Visibility.Collapsed;
            ReviewText.Visibility = Visibility.Collapsed;
            AddToProfileButton.IsEnabled = true;
            AddToProfileButton.Visibility = Visibility.Collapsed;
            AddToProfileButton.Visibility = Visibility.Collapsed;
            BorderForSendBox.Visibility = Visibility.Collapsed;
            StackPanelForRate.Visibility = Visibility.Collapsed;
            SadSmileButton.Visibility = Visibility.Collapsed;
            SadSmile.Visibility = Visibility.Collapsed;
            NormSmileButton.Visibility = Visibility.Collapsed;
            NormSmile.Visibility = Visibility.Collapsed;
            HappySmileButton.Visibility = Visibility.Collapsed;
            HappySmile.Visibility = Visibility.Collapsed;
            AnimeTextBlock.Visibility = Visibility.Visible;
        }

        //private void AnimeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.logger.LogInformation("Click detail about anime button");
        //    AnimeWindow.ParentWindow = this;
        //    this.animeFactory.Create(this).Show();
        //    this.Close();
        //}
        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Filter button");
            List<GenreModel> temp = genreService.GetAll();

            genreList = new List<Genres>();
            foreach (GenreModel genre in temp)
            {
                genreList.Add(new Genres { GenreName = genre.Name });
            }
            card.Visibility = Visibility.Collapsed;
            ButtonButton.Visibility = Visibility.Collapsed;
            filter.Visibility = Visibility.Visible;
            genreListView.ItemsSource = genreList;
            this.logger.LogInformation("List of anime was shown");
        }

        private void AddToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            AddedAnimeModel temp = new AddedAnimeModel
            {
                Id = addedAnimeService.GetLastId() + 1,
                AnimeId = randomAnimeId,
                UserId = this.id
            };
            addedAnimeService.Insert(temp);

            AddToProfileButton.IsEnabled = false;
        }

        private void SadSmileButton_Click(object sender, RoutedEventArgs e)
        {
            UserRate = 1;
            ToggleSmileButton(SadSmileButton, SadSmile);
        }

        private void NormSmileButton_OnClick(object sender, RoutedEventArgs e)
        {
            UserRate = 2;
            ToggleSmileButton(NormSmileButton, NormSmile);
        }

        private void HappySmileButton_OnClick(object sender, RoutedEventArgs e)
        {
            UserRate = 3;
            ToggleSmileButton(HappySmileButton, HappySmile);
        }

        private void ResetSmileButtonScale(Button button, Image smileImage)
        {
            // Скидання масштабу для заданої кнопки та зображення смайла
            button.RenderTransform = new ScaleTransform(1.0, 1.0);
            smileImage.RenderTransform = new ScaleTransform(1.0, 1.0);
        }

        private void ToggleSmileButton(Button button, Image smileImage)
        {
            // Скидання масштабу для всіх смайлів
            ResetSmileButtonScale(SadSmileButton, SadSmile);
            ResetSmileButtonScale(NormSmileButton, NormSmile);
            ResetSmileButtonScale(HappySmileButton, HappySmile);

            // Збільшення масштабу для вибраної кнопки та зображення смайла
            button.RenderTransform = new ScaleTransform(1.1, 1.1);
            smileImage.Visibility = Visibility.Visible;
            smileImage.RenderTransform = new ScaleTransform(1.1, 1.1);
        }

        private void ReviewText_OnGotFocus(object sender, RoutedEventArgs e)
        {
            // Очистити текст при отриманні фокусу
            if (ReviewText.Text == "Введіть ваш відгук")
            {
                ReviewText.Text = string.Empty;
            }
        }

        private void ReviewText_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace (ReviewText.Text))
            {
                ReviewText.Text = "Введіть ваш відгук";
            }
        }
    }
}