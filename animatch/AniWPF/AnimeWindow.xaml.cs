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
    public partial class AnimeWindow : Window
    {
        private readonly ILogger<AnimeWindow> logger;

        public static Window ParentWindow { get; set; }
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAbstractFactory<LikedAnimeWindow> likedFactory;
        private readonly IAbstractFactory<SearchWindow> searchFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;

        private readonly IAddedAnimeService addedAnimeService;
        private readonly ILikedAnimeService likedAnimeService;
        private readonly IDislikedAnimeService dislikedAnimeService;
        private readonly IWatchedAnimeService watchAnimeService;
        private readonly IAnimeService animeService;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;
        private List<ReviewForForm> reviewList;


        private AnimeViewModel viewModel;
        private int id;
        public static int AnimeId { get; set; }


        

        public AnimeWindow(IAnimeService animeService, IAddedAnimeService addedAnimeService,
            IDislikedAnimeService dislikedAnimeService, ILikedAnimeService likedAnimeService,
            IWatchedAnimeService watchedAnimeService, IUserService userService,
            IAbstractFactory<RandomWindow> rfactory, IAbstractFactory<ProfileWindow> profileFactory,
            IAbstractFactory<LikedAnimeWindow> likedFactory, IAbstractFactory<SearchWindow> searchFactory,
            IAbstractFactory<MainWindow>mainFactory, IReviewService reviewService, ILogger<AnimeWindow> logger)
        {
            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.animeService = animeService;
            this.randomFactory = rfactory;
            this.likedFactory = likedFactory;
            this.profileFactory = profileFactory;
            this.searchFactory = searchFactory;
            this.mainFactory = mainFactory;

            this.addedAnimeService = addedAnimeService;
            this.likedAnimeService = likedAnimeService;
            this.dislikedAnimeService = dislikedAnimeService;
            this.watchAnimeService = watchedAnimeService;
            this.userService = userService;
            this.reviewService = reviewService;

            if (ParentWindow != null)
            {
                if (ParentWindow.GetType() == typeof(MainWindow))
                {
                    AnimeId = MainWindow.randomAnimeId;
                }
            }

            this.id = LogInWindow.CurrentUserID;
            List<ReviewModel> temp = reviewService.GetReviewsForAnime(AnimeId);
            reviewList = new List<ReviewForForm>();
            foreach (ReviewModel review in temp)
            {
                var reviewViewModel = new ReviewViewModel(reviewService, userService, review.Id);
                reviewList.Add(new ReviewForForm(reviewViewModel));
            }
            RewiewListView.ItemsSource = reviewList;

           

            this.viewModel = new AnimeViewModel(this.animeService, AnimeId, this.addedAnimeService);
            this.DataContext = this.viewModel;

            this.logger = logger;
            this.logger.LogInformation("MainWindow created");
            InitializeComponent();
        }
        public class ReviewForForm
        {
            public string ReviewText { get; set; }
            public string ReviewUserName { get; set; }
            public string ReviewUserPhoto { get; set; }

            // Додайте конструктор, що приймає дані з ReviewViewModel
            public ReviewForForm(ReviewViewModel reviewViewModel)
            {
                ReviewText = reviewViewModel.ReviewText;
                ReviewUserName = reviewViewModel.UserName;
                ReviewUserPhoto = reviewViewModel.UserPhoto;
            }
        }
        public class ReviewViewModel : INotifyPropertyChanged
        {
            private readonly IReviewService reviewServise;
            private readonly IUserService userService;
            private int id;

            public ReviewViewModel(IReviewService reviewService,IUserService userService, int id)
            {
                this.reviewServise = reviewService;
                this.userService = userService;
                this.id = id;
            }
            public string ReviewText
            {
                get { return this.reviewServise.GetById(this.id).Text; }
            }
            public string UserName
            {
                get { return this.userService.GetById(this.reviewServise.GetById(this.id).UserId).Name; }
            }
            public string UserPhoto
            {
                get { return this.userService.GetById(this.reviewServise.GetById(this.id).UserId).Photo; }
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
            private readonly IAddedAnimeService addedAnime;
            
            private int id;

            public AnimeViewModel(IAnimeService animeService, int id, IAddedAnimeService addedAnime)
            {
                this.addedAnime = addedAnime;
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
            public string UserLikedAnime
            {
                get { return $"{addedAnime.CountUserWhoAddAnime(this.id)} користувачів вподобали це аніме"; }
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
        private void LikeAnime_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click LikeAnime button");
            likeUnfill.Source = new BitmapImage(new Uri("https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/LikedFillIcon.png?raw=true"));

            LikedAnimeModel temp = new LikedAnimeModel
            {
                Id = likedAnimeService.GetLastUserId() + 1,
                UserId = this.id,
                AnimeId = AnimeId
            };
            likedAnimeService.Insert(temp);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParentWindow != null)
            {
                if (ParentWindow.GetType() == typeof(MainWindow))
                {
                    MainWindow.ParentWindow = this;
                    // Show the new instance of the ParentWindow
                    this.mainFactory.Create(this).Show();
                    MainWindow.ParentWindow = null;
                }
            }

            // Close the current AnimeWindow
            this.Close();
        }

        private void logoButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainFactory.Create(this).Show();
            this.Close();
        }
    }
}