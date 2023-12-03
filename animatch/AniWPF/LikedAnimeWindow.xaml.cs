using AniBLL.Services;
using AniBLL.Models;
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
using AniWPF.StartupHelper;
using static AniWPF.ProfileWindow;
using Microsoft.Extensions.Logging;

namespace AniWPF
{
    public partial class LikedAnimeWindow : Window
    {
        private readonly ILogger<LikedAnimeModel> logger;

        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;

        private readonly ILikedAnimeService likedAnimeService;

        private int id;
        private List<AnimeForForw> animeList;

        public LikedAnimeWindow(ILikedAnimeService likedAnimeService, IAbstractFactory<RandomWindow> randomFactory, 
                                IAbstractFactory<MainWindow> mainFactory, ILogger<LikedAnimeModel> logger)
        {
            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;

            this.likedAnimeService = likedAnimeService;

            this.id = LogInWindow.CurrentUserID;

            List<AnimeModel> temp = likedAnimeService.GetLikedAnimesForUser(this.id);

            animeList = new List<AnimeForForw>();
            foreach (AnimeModel anime in temp)
            {
                animeList.Add(new AnimeForForw { Title = anime.Name, ImagePath = anime.Photo });
            }
            animeListView.ItemsSource = animeList;

            this.logger = logger;
            this.logger.LogInformation("LikedAnimeWindow created");
            this.logger.LogInformation("List of liked anime was shown");

            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        public class AnimeForForw
        {
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
            //
        }

    }
}
