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
    
    public partial class RedactWindow : Window
    {
        private readonly IUserService userService;
        private readonly IAddedAnimeService addedAnimeService;
        private readonly IAnimeService animeService;
        private UserViewModel viewModel;
        private int id;
        //private List<Animes> animeList;
        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        public RedactWindow(IUserService userService, IAddedAnimeService addedAnimeService, IAnimeService animeService, IAbstractFactory<RandomWindow> randomFactory, IAbstractFactory<MainWindow> mainFactory)
        {
            this.id = LogInWindow.CurrentUserID;
           
            this.userService = userService;
            this.viewModel = new UserViewModel(this.userService, this.id);
            this.DataContext = this.viewModel;
            this.addedAnimeService = addedAnimeService;
            this.animeService = animeService;
            List<Anime> temp = addedAnimeService.GetAddedAnimesForUser(this.id);
            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;
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

                set { }
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
