using AniBLL.Models;
using AniBLL.Services;
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

        private List<AnimeForForw> animeList;

        private readonly IAnimeService animeService;

        private int id;
        
        public SearchWindow(IAnimeService animeService, ILogger<SearchWindow> logger)
        {
            this.animeService = animeService;

            id = LogInWindow.CurrentUserID;

            this.logger = logger;
            this.logger.LogInformation("SearchWindow created");

            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        public class AnimeForForw
        {
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
                if(anime.Name.Contains(searchText))
                {
                    animeList.Add(new AnimeForForw { Title = anime.Name, ImagePath = anime.Photo, IMDBRate = anime.Imdbrate});
                }
            }
            animeListView.ItemsSource = animeList;
            this.logger.LogInformation("List of anime waas shown");
        }
    }
}
