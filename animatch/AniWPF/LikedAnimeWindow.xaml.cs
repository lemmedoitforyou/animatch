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

namespace AniWPF
{
    public partial class LikedAnimeWindow : Window
    {
        private readonly ILikedAnimeService likedAnimeService;
        private int id;
        private List<AnimeForForw> animeList;
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        public LikedAnimeWindow(ILikedAnimeService likedAnimeService, IAbstractFactory<RandomWindow> randomFactory, IAbstractFactory<MainWindow> mainFactory)
        {
            this.likedAnimeService = likedAnimeService;
            this.id = LogInWindow.CurrentUserID;
            InitializeComponent();

            List<AnimeModel> temp = likedAnimeService.GetLikedAnimesForUser(this.id);

            animeList = new List<AnimeForForw>();
            foreach (AnimeModel anime in temp)
            {
                animeList.Add(new AnimeForForw { Title = anime.Name, ImagePath = anime.Photo });
            }
            animeListView.ItemsSource = animeList;
            this.WindowState = WindowState.Maximized;
            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;
        }

        public class AnimeForForw
        {
            public string Title { get; set; }
            public string ImagePath { get; set; }
        }
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            this.randomFactory.Create(this).Show();
            this.Close();
        }
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            this.mainFactory.Create(this).Show();
            this.Close();
        }
        private void ButtonAdded_Click(object sender, RoutedEventArgs e)
        {
           //ds
        }
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            //
        }

    }
}
