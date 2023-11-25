using AniBLL.Services;
using AniDAL.DataBaseClasses;
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
        private List<Animes> animeList;
        public LikedAnimeWindow(ILikedAnimeService likedAnimeService)
        {
            this.likedAnimeService = likedAnimeService;
            this.id = LogInWindow.CurrentUserID;
            InitializeComponent();

            List<Anime> temp = likedAnimeService.GetLikedAnimesForUser(this.id);

            animeList = new List<Animes>();
            foreach (Anime anime in temp)
            {
                animeList.Add(new Animes { Title = anime.Name, ImagePath = anime.Photo });
            }
            animeListView.ItemsSource = animeList;
            this.WindowState = WindowState.Maximized;
        }

        public class Animes
        {
            public string Title { get; set; }
            public string ImagePath { get; set; }
        }
    }
}
