using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AniBLL.Services;
using AniDAL.DataBaseClasses;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class ProfileWindow : Window
    {
        private readonly IUserService userService;
        private readonly IAddedAnimeService addedAnimeService;
        private readonly IAnimeService animeService;
        private UserViewModel viewModel;
        private List<Anime> usersAdded;
        private int id;
        private AnimeListViewModel animeListViewModel;
        public ProfileWindow(IUserService userService, IAddedAnimeService addedAnimeService, IAnimeService animeService)
        {

            System.Random random = new System.Random();
            this.id = LogInWindow.CurrentUserID;
            this.InitializeComponent();
            this.userService = userService;
            this.viewModel = new UserViewModel(this.userService, this.id);
            this.DataContext = this.viewModel;
            this.addedAnimeService = addedAnimeService;
            this.animeService = animeService;
            this.usersAdded = addedAnimeService.GetAddedAnimesForUser(this.id);
            this.animeListViewModel = new AnimeListViewModel(this.animeService, this.addedAnimeService, this.usersAdded);
            this.animeListBox.DataContext = this.animeListViewModel;
            
            //foreach (Anime anime in this.usersAdded)
            //{
            //    animeListBox.Items.Add(new MyItem { ImagePath = anime.Photo, ItemText = anime.Name });
            //}

        }
        public class MyItem
        {
            public string ImagePath { get; set; }
            public string ItemText { get; set; }
        }
        public class UserViewModel : INotifyPropertyChanged
        {
            private readonly IUserService userService;
            private int id;

            public UserViewModel(IUserService userService, int id)
            {
                this.userService = userService;
                this.id = id;
            }

            public string UserName
            {
                get { return this.userService.GetById(this.id).Name; }
                set { }
            }

            public string UserText
            {
                get { return this.userService.GetById(this.id).Text; }
                set { }
            }

            public string UserLevel
            {
                get
                {
                    int level = this.userService.GetById(this.id).Level;
                    switch (level)
                    {
                        case 1:
                            return "новачок";
                        case 2:
                            return "досвічений анімешник";
                        case 3:
                            return "любитель конкретних жанрів";
                        default:
                            return "лох";
                    }
                }

                set
                {
                }
            }

            public string UserPhoto
            {
                get
                {
                    return this.userService.GetById(this.id).Photo;
                }
            }

            public int UserWachedCount
            {
                get
                {
                    return this.userService.GetById(this.id).WatchedCount;
                }
                set { }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class AnimeViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService animeService;
            private readonly IAddedAnimeService addedAnimeService;
            private int id;

            public AnimeViewModel(IAnimeService animeService, int id, IAddedAnimeService addedAnimeService)
            {
                this.animeService = animeService;
                this.id = id;
                this.addedAnimeService = addedAnimeService;
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
        public class AnimeListViewModel : INotifyPropertyChanged
        {
            private readonly IAnimeService animeService;
            private readonly IAddedAnimeService addedAnimeService;
            private readonly List<Anime> animeList;

            public AnimeListViewModel(IAnimeService animeService, IAddedAnimeService addedAnimeService, List<Anime> animeList)
            {
                this.animeService = animeService;
                this.addedAnimeService = addedAnimeService;
                this.animeList = animeList;
                this.Animes = new ObservableCollection<AnimeViewModel>(
                    animeList.Select(a => new AnimeViewModel(this.animeService, a.Id, this.addedAnimeService))
                );
            }

            public ObservableCollection<AnimeViewModel> Animes { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RedactClick(object sender, RoutedEventArgs e)
        {
            System.Random random = new System.Random();
            this.viewModel = new UserViewModel(this.userService, random.Next(1, 50));
            this.DataContext = this.viewModel;
        }
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            //this.randomFactory.Create(this.ParentWindow).Show();
        }
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            //this.profileFactory.Create(this.ParentWindow).Show();
        }

        private void ButtonAdded_Click(object sender, RoutedEventArgs e)
        {
            // Your code for the ButtonAdded_Click event handler
        }
    }

}