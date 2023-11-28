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

namespace AniWPF
{
    public partial class MainWindow : Window, IWindowAware
    {
        public Window ParentWindow { get; set; }
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAbstractFactory<LikedAnimeWindow> likedFactory;

        private readonly IAddedAnimeService addedAnimeService;
        private readonly ILikedAnimeService likedAnimeService;
        private readonly IDislikedAnimeService dislikedAnimeService;
        private readonly IWatchedAnimeService watchAnimeService;
        private readonly IAnimeService animeService;
        private AnimeViewModel viewModel;
        private int id;


        public MainWindow(IAnimeService animeService, IAddedAnimeService addedAnimeService,
            IDislikedAnimeService dislikedAnimeService, ILikedAnimeService likedAnimeService,
            IWatchedAnimeService watchedAnimeService, IAbstractFactory<RandomWindow> rfactory,
            IAbstractFactory<ProfileWindow> profileFactory, IAbstractFactory<LikedAnimeWindow> likedFactory)
        {
            this.InitializeComponent();
            this.animeService = animeService;
            this.randomFactory = rfactory;
            this.likedFactory = likedFactory;

            this.addedAnimeService = addedAnimeService;
            this.likedAnimeService = likedAnimeService;
            this.dislikedAnimeService = dislikedAnimeService;
            this.watchAnimeService = watchedAnimeService;

            this.id = LogInWindow.CurrentUserID;
            List<AnimeModel> animes = animeService.GetAll();
            List<AnimeModel> dislikedanimes = dislikedAnimeService.GetDislikedAnimesForUser(id);
            List<AnimeModel> likedanimes = likedAnimeService.GetLikedAnimesForUser(id);
            List<AnimeModel> addedanimes = addedAnimeService.GetAddedAnimesForUser(id);
            List<AnimeModel> watchedanimes = watchedAnimeService.GetWatchedAnimesForUser(id);

            List<AnimeModel> uniqueAnimes = animes
                .Except(dislikedanimes)
                .Except(likedanimes)
                .Except(addedanimes)
                .Except(watchedanimes).ToList();

            Random random = new Random();

            // Генеруємо випадковий індекс
            int randomIndex = random.Next(uniqueAnimes.Count);
            // Вибираємо випадкове аніме
            AnimeModel randomAnime = uniqueAnimes[randomIndex];

            // Створюємо екземпляр ViewModel і встановлюємо його як DataContext
            this.viewModel = new AnimeViewModel(this.animeService, randomAnime.Id);
            this.DataContext = this.viewModel;
            this.profileFactory = profileFactory;
            //this.likedFactory = likedFactory;
            this.WindowState = WindowState.Maximized;
        }


        public class AnimeViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService animeService;
            private int id;

            public AnimeViewModel(IAnimeService animeService, int id)
            {
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

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            this.randomFactory.Create(this).Show();
            this.Close();
        }
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            this.profileFactory.Create(this).Show();
            this.Close();
        }

        private void ButtonLiked_Click(object sender, RoutedEventArgs e)
        {
            this.likedFactory.Create(this).Show();
            this.Close();
        }
        private async void LikeAnime_Click(object sender, RoutedEventArgs e)
        {
            // Змінити зображення на "заповнене"
            likeUnfill.Source = new BitmapImage(new Uri("https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/LikedFillIcon.png?raw=true"));

            // Затримка на 1 секунду
            await Task.Delay(1000);

            // Отримати нові дані та змінити їх
            Random random = new Random();
            this.viewModel = new AnimeViewModel(this.animeService, random.Next(1, 51));
            this.DataContext = this.viewModel;

            // Змінити зображення на "порожнє"
            likeUnfill.Source = new BitmapImage(new Uri("https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/LikedIcon.png?raw=true"));
        }

    }
}
