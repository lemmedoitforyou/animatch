using System.ComponentModel;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class MainWindow : Window, IWindowAware
    {
        public Window ParentWindow { get; set; }
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;
        private readonly IAnimeService animeService;
        private AnimeViewModel viewModel;

        public MainWindow(IAnimeService animeService, IAbstractFactory<RandomWindow> rfactory, IAbstractFactory<ProfileWindow> profileFactory)
        {
            this.InitializeComponent();
            this.animeService = animeService;
            this.randomFactory = rfactory;

            // Створюємо екземпляр ViewModel і встановлюємо його як DataContext
            this.viewModel = new AnimeViewModel(this.animeService, 1);
            this.DataContext = this.viewModel;
            this.profileFactory = profileFactory;
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
            this.randomFactory.Create(this.ParentWindow).Show();
        }
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            this.profileFactory.Create(this.ParentWindow).Show();
        }

        private void ButtonAdded_Click(object sender, RoutedEventArgs e)
        {
            // Your code for the ButtonAdded_Click event handler
        }

    }
}
