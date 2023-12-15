using AniBLL.Models;
using AniBLL.Services;
using AniWPF.ViewModels;
using AniWPF.StartupHelper;
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

namespace AniWPF
{
    public partial class SearchWindow : Window
    {
        private readonly ILogger<SearchWindow> logger;

        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAbstractFactory<LikedAnimeWindow> likedFactory;
        private readonly IAbstractFactory<AnimeWindow> animeFactory;

        private List<AnimeForForw> animeList;

        private readonly IAnimeService animeService;

        private int id;

        public static int CurrentId { get; set; }
        
        public SearchWindow(IAnimeService animeService, IAbstractFactory<MainWindow> mainFactory, 
            ILogger<SearchWindow> logger, IAbstractFactory<RandomWindow> randomFactory, 
            IAbstractFactory<ProfileWindow> profileFactory, IAbstractFactory<LikedAnimeWindow> likedFactory,
            IAbstractFactory<AnimeWindow> animeFactory)
        {
            this.mainFactory = mainFactory;
            this.randomFactory = randomFactory;
            this.profileFactory = profileFactory;
            this.likedFactory = likedFactory;
            this.animeFactory = animeFactory;

            this.animeService = animeService;

            id = LogInWindow.CurrentUserID;

            this.logger = logger;
            this.logger.LogInformation("SearchWindow created");

            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        public class AnimeForForw
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ImagePath { get; set; }
            public double IMDBRate { get; set; }
        }

        private void SearchTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = textBox.Tag?.ToString();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                textBox.Text = textBox.Tag?.ToString();
            }
            else
            {
                textBox.Foreground = Brushes.Black;
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Search button");
            string searchText = searchTextBox.Text;
            List<AnimeModel> temp = animeService.GetAll();

            animeList = new List<AnimeForForw>();
            foreach (AnimeModel anime in temp)
            {
                if(anime.Name.ToLower().Contains(searchText.ToLower()))
                {
                    animeList.Add(new AnimeForForw { Id = anime.Id, Title = anime.Name, ImagePath = anime.Photo, IMDBRate = anime.Imdbrate});
                }
            }
            this.animeItemsControl.ItemsSource = this.animeList;
            this.logger.LogInformation("List of anime was shown");
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

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Очистити текст при отриманні фокусу
            if (searchTextBox.Text == "Enter anime title")
            {
                searchTextBox.Text = string.Empty;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Встановити текст підказки при втраті фокусу, якщо користувач не ввів жоден текст
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Enter anime title";
            }
        }
    }
}
