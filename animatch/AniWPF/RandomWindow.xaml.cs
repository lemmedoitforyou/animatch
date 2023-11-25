using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class RandomWindow : Window
    {
        private readonly IAnimeService animeService;
        private readonly IAnimeGenreService animeGenreService;
        private int id;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        private AnimeViewModel viewModel;

        public RandomWindow(IAnimeService animeService, IAnimeGenreService animeGenreService, IAbstractFactory<ProfileWindow> profileFactory, IAbstractFactory<MainWindow> mainFactory)
        {
            this.animeService = animeService;
            this.animeGenreService = animeGenreService;
            this.id = LogInWindow.CurrentUserID;
            this.InitializeComponent();
            System.Random randomForAnime = new System.Random();
            this.profileFactory = profileFactory;

            // Створюємо екземпляр ViewModel і встановлюємо його як DataContext
            this.viewModel = new AnimeViewModel(this.animeService, this.animeGenreService, randomForAnime.Next(1, 50));
            this.DataContext = this.viewModel;

            this.WindowState = WindowState.Maximized;
            this.profileFactory = profileFactory;
            this.mainFactory = mainFactory;
        }

        public class AnimeViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService animeService;
            private readonly IAnimeGenreService animeGenreService;
            private int id;

            public AnimeViewModel(IAnimeService animeService, IAnimeGenreService animeGenreService, int id)
            {
                this.animeService = animeService;
                this.animeGenreService = animeGenreService;
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
                get
                {
                    return this.animeService.GetById(this.id).Imdbrate;
                }

                set
                {
                    // Встановлюємо значення rate в джерелі даних або де зручно.
                    this.OnPropertyChanged(nameof(this.AnimeRate)); // Сповіщаємо систему про зміну значення
                }
            }

            public string AnimePhoto
            {
                get
                {
                    return this.animeService.GetById(this.id).Photo;
                }
            }

            public int AnimeYear
            {
                get
                {
                    return this.animeService.GetById(this.id).Year;
                }
            }

            public string AnimeGenres
            {
                get
                {
                    List<string> temp = this.animeGenreService.GetGenresForAnime(this.id);
                    string result = "";
                    foreach (string item in temp)
                    {
                        result += item;
                    }
                    return result;
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Random randomForAnime = new System.Random();
            this.viewModel = new AnimeViewModel(this.animeService, this.animeGenreService, randomForAnime.Next(1, 50));
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            System.Random randomForAnime = new System.Random();
            this.viewModel = new AnimeViewModel(this.animeService, this.animeGenreService, randomForAnime.Next(1, 50));
            this.DataContext = this.viewModel;
        }
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            //this.randomFactory.Create(this).Show();
            //this.Close();
        }
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            this.profileFactory.Create(this).Show();
            this.Close();
        }

        private void ButtonAdded_Click(object sender, RoutedEventArgs e)
        {
            // Your code for the ButtonAdded_Click event handler
        }
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            this.mainFactory.Create(this).Show();
            this.Close();
        }
    }
}
