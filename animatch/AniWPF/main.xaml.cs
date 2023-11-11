using System.ComponentModel;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class Main : Window
    {
        private readonly IAbstractFactory<Random> randomFactory;
        private readonly IAnimeService animeService;
        private AnimeViewModel viewModel;

        public Main(IAnimeService animeService, IAbstractFactory<Random> rfactory)
        {
            this.InitializeComponent();
            this.animeService = animeService;
            this.randomFactory = rfactory;

            // Створюємо екземпляр ViewModel і встановлюємо його як DataContext
            this.viewModel = new AnimeViewModel(this.animeService, 1);
            this.DataContext = this.viewModel;
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
            this.randomFactory.Create().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.randomFactory.Create().Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.randomFactory.Create().Show();
        }
    }
}
