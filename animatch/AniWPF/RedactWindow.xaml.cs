using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AniBLL.Models;
using AniBLL.Services;
using AniWPF.StartupHelper;
using Microsoft.Extensions.Logging;

namespace AniWPF
{
    
    public partial class RedactWindow : Window
    {
        private readonly ILogger<RedactWindow> logger;

        private readonly IAbstractFactory<RandomWindow> randomFactory;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<ProfileWindow> profileFactory;

        private readonly IUserService userService;
        private readonly IAddedAnimeService addedAnimeService;
        private readonly IAnimeService animeService;

        private UserViewModel viewModel;
        private int id;

        public RedactWindow(IUserService userService, IAddedAnimeService addedAnimeService, 
            IAnimeService animeService, IAbstractFactory<RandomWindow> randomFactory, 
            IAbstractFactory<MainWindow> mainFactory, IAbstractFactory<ProfileWindow> profileFactory,
            ILogger<RedactWindow> logger)
        {
            this.randomFactory = randomFactory;
            this.mainFactory = mainFactory;
            this.profileFactory = profileFactory;

            this.userService = userService;
            this.addedAnimeService = addedAnimeService;
            this.animeService = animeService;

            this.id = LogInWindow.CurrentUserID;

            this.viewModel = new UserViewModel(this.userService, this.id);
            this.DataContext = this.viewModel;
            List<AnimeModel> temp = addedAnimeService.GetAddedAnimesForUser(this.id);

            this.logger = logger;
            this.logger.LogInformation("RedactWindow created");

            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
            
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
                set {  }
            }

            public string UserText
            {
                get { return this.userService.GetById(this.id).Text; }
                set {   }
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

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Watched button, changes was canceled");
            this.profileFactory.Create(this).Show();
            this.Close();
        }
        
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Watched button, changes was save");
            userService.UpdateTitleAndText(id,name.Text, description.Text);
            this.profileFactory.Create(this).Show();
            this.Close();
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Main button");
            this.mainFactory.Create(this).Show();
            this.Close();
        }
    }
}
