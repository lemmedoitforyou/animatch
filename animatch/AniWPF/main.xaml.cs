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
    
    public partial class main : Window
    {
        private readonly IAnimeService _animeService;
        private AnimeViewModel _viewModel;

        public main(IAnimeService animeService)
        {
            InitializeComponent();
            _animeService = animeService;
            
        // Створюємо екземпляр ViewModel і встановлюємо його як DataContext
            _viewModel = new AnimeViewModel(_animeService);
            DataContext = _viewModel;
        }
        public class AnimeViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService _animeService;

            public AnimeViewModel(IAnimeService animeService)
            {
                _animeService = animeService;
            }
            public string AnimeName
            {
                get { return _animeService.GetById(1).Name; }
            }

            public string AnimeText
            {
                get { return _animeService.GetById(2).Text; }

            }
            public double AnimeRate
            {
                get
                {
                    return _animeService.GetById(1).Imdbrate;
                }
            }
            public string AnimePhoto
            {
                get
                {
                    return _animeService.GetById(1).Photo;
                }
            }

            public int AnimeYear
            {
                get
                {
                    return _animeService.GetById(1).Year;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
}
