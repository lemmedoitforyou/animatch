using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AniBLL.Models;
using AniBLL.Services;
using AniWPF.StartupHelper;
using AniWPF.ViewModels;
using AniWPF.Windows;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IAbstractFactory<ViewProfileWindow> viewProfileFactory;

        private readonly IAddedAnimeService addedAnimeService;
        private readonly ILikedAnimeService likedAnimeService;
        private readonly IDislikedAnimeService dislikedAnimeService;
        private readonly IWatchedAnimeService watchAnimeService;
        private readonly IAnimeService animeService;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;
        private readonly IAnimeGenreService animeGenreService;


        private List<ReviewForForm> reviewList;

        private AnimeViewModel viewModel;
        private int id;
        public static int UserId;

        public static int AnimeId { get; set; }

        public AnimeWindow(IAnimeService animeService, IAddedAnimeService addedAnimeService,
            IDislikedAnimeService dislikedAnimeService, ILikedAnimeService likedAnimeService,
            IWatchedAnimeService watchedAnimeService, IUserService userService,
            IAbstractFactory<RandomWindow> rfactory, IAbstractFactory<ProfileWindow> profileFactory,
            IAbstractFactory<LikedAnimeWindow> likedFactory, IAbstractFactory<SearchWindow> searchFactory,
            IAbstractFactory<MainWindow> mainFactory, IReviewService reviewService,
            IAnimeGenreService animeGenreService, ILogger<AnimeWindow> logger, IAbstractFactory<ViewProfileWindow> viewProfileFactory)
        {
            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.animeService = animeService;
            this.randomFactory = rfactory;
            this.likedFactory = likedFactory;
            this.profileFactory = profileFactory;
            this.searchFactory = searchFactory;
            this.mainFactory = mainFactory;
            this.viewProfileFactory = viewProfileFactory;

            this.addedAnimeService = addedAnimeService;
            this.likedAnimeService = likedAnimeService;
            this.dislikedAnimeService = dislikedAnimeService;
            this.watchAnimeService = watchedAnimeService;
            this.userService = userService;
            this.reviewService = reviewService;
            this.animeGenreService = animeGenreService;

            if (ParentWindow != null)
            {
                if (ParentWindow.GetType() == typeof(MainWindow))
                {
                    AnimeId = MainWindow.randomAnimeId;
                }

                else if (ParentWindow.GetType() == typeof(LikedAnimeWindow))
                {
                    AnimeId = LikedAnimeWindow.CurrentId;
                }

                else if (ParentWindow.GetType() == typeof(ProfileWindow))
                {
                    AnimeId = ProfileWindow.CurrentId;
                }

                else if (ParentWindow.GetType() == typeof(RandomWindow))
                {
                    AnimeId = RandomWindow.randomAnimeId;
                }

                else if (ParentWindow.GetType() == typeof(SearchWindow))
                {
                    AnimeId = SearchWindow.CurrentId;
                }

                else if (ParentWindow.GetType() == typeof(ViewProfileWindow))
                {
                    AnimeId = ViewProfileWindow.CurrentId;
                }
            }


            this.id = LogInWindow.CurrentUserID;
            List<ReviewModel> temp = reviewService.GetReviewsForAnime(AnimeId);
            this.reviewList = new List<ReviewForForm>();
            foreach (ReviewModel review in temp)
            {
                var reviewViewModel = new ReviewViewModel(reviewService, userService, review.Id);
                this.reviewList.Add(new ReviewForForm(reviewViewModel));
            }

            this.RewiewListView.ItemsSource = this.reviewList;

            this.viewModel = new AnimeViewModel(this.animeService, this.addedAnimeService, this.animeGenreService, AnimeId);
            this.DataContext = this.viewModel;

            this.logger = logger;
            this.logger.LogInformation("MainWindow created");

            this.InitializeComponent();
            this.viewProfileFactory = viewProfileFactory;
        }

        public class ReviewForForm
        {
            public string ReviewText { get; set; }

            public string ReviewUserName { get; set; }

            public string ReviewUserPhoto { get; set; }
            public string ReviewRatingImage { get; set; }
            public int ReviewUserId { get; set; }

            public ReviewForForm(ReviewViewModel reviewViewModel)
            {
                this.ReviewText = reviewViewModel.ReviewText;
                this.ReviewUserName = reviewViewModel.UserName;
                this.ReviewUserPhoto = reviewViewModel.UserPhoto;
                this.ReviewRatingImage = reviewViewModel.ReviewImage;
                this.ReviewUserId = reviewViewModel.UserId;
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


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParentWindow != null)
            {
                if (ParentWindow.GetType() == typeof(MainWindow))
                {
                    MainWindow.ParentWindow = this;
                    this.mainFactory.Create(this).Show();
                    MainWindow.ParentWindow = null;
                }
                else if (ParentWindow.GetType() == typeof(LikedAnimeWindow))
                {
                    this.likedFactory.Create(this).Show();
                }
                else if (ParentWindow.GetType() == typeof(ProfileWindow))
                {
                    this.profileFactory.Create(this).Show();
                }
                else if (ParentWindow.GetType() == typeof(RandomWindow))
                {
                    RandomWindow.ParentWindow = this;
                    this.randomFactory.Create(this).Show();
                    RandomWindow.ParentWindow = null;
                }
                else if (ParentWindow.GetType() == typeof(SearchWindow))
                {
                    this.searchFactory.Create(this).Show();
                }
                else if (ParentWindow.GetType() == typeof(ViewProfileWindow))
                {
                    this.viewProfileFactory.Create(this).Show();
                }
            }

            this.Close();
        }

        private void LogoButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainFactory.Create(this).Show();
            this.Close();
        }


        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement button)
            {
                if (button.DataContext is ReviewForForm clickedItem)
                {
                    string temp = clickedItem.ReviewUserName;
                    ReviewForForm foundUser = this.reviewList.FirstOrDefault(user => user.ReviewUserName == temp);
                    if (foundUser != null)
                    {
                        UserId = foundUser.ReviewUserId;
                        
                    }
                     
                    this.viewProfileFactory.Create(this).Show();
                    this.Close();
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.mainFactory.Create(this).Show();
            this.Close();
        }
    }
}
