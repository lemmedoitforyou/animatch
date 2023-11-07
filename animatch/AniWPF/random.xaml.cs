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
using AniDAL.Repositories;
using AniWPF.StartupHelper;
using System.Windows.Controls;
using System.ComponentModel;
using AniBLL.Services;

namespace AniWPF
{
   
    public partial class random : Window
    {

        private readonly IAnimeService _animeService;
        private AnimeViewModel _viewModel;
        public random(IAnimeService animeService)
        {


            InitializeComponent();
            _animeService = animeService;

            Random randomForAnime = new Random();
            // Створюємо екземпляр ViewModel і встановлюємо його як DataContext
            _viewModel = new AnimeViewModel(_animeService, randomForAnime.Next(1,50));
            DataContext = _viewModel;
        }
        public class AnimeViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService _animeService;
            private int id;

            public AnimeViewModel(IAnimeService animeService, int id)
            {
                _animeService = animeService;
                this.id = id;
            }
            public string AnimeName
            {
                get { return _animeService.GetById(id).Name; }
            }

            public string AnimeText
            {
                get { return _animeService.GetById(id).Text; }

            }
            public double AnimeRate
            {
                get
                {
                    return _animeService.GetById(id).Imdbrate;
                }
                set
                {
                    // Встановлюємо значення rate в джерелі даних або де зручно.
                    OnPropertyChanged(nameof(AnimeRate)); // Сповіщаємо систему про зміну значення
                }
            }
            public string AnimePhoto
            {
                get
                {
                    return _animeService.GetById(id).Photo;
                }
            }

            public int AnimeYear
            {
                get
                {
                    return _animeService.GetById(id).Year;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random randomForAnime = new Random();
            _viewModel = new AnimeViewModel(_animeService, randomForAnime.Next(1, 50));
        }

        private void random_Click(object sender, RoutedEventArgs e)
        {
            Random randomForAnime = new Random();
            _viewModel = new AnimeViewModel(_animeService, randomForAnime.Next(1, 50));
            DataContext = _viewModel;
        }
    }
}

