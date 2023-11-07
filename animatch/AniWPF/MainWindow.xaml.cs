using AniDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using AniDAL;
using AniWPF.StartupHelper;
using System.Windows.Controls;

namespace AniWPF
{
    public partial class MainWindow : Window
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IAbstractFactory<ChildForm> _factory;
       

        public MainWindow(IAnimeRepository animeAccess,IUserInfoRepository userInfoAccess, IAbstractFactory<ChildForm> factory)
        {
            InitializeComponent();
            _animeRepository = animeAccess;
            _userInfoRepository = userInfoAccess;

            _factory = factory;
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void login_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void buttonEnter_Click(object sender, RoutedEventArgs e)
        {
            string Login = login.Text;
            string Password = password.Password;
            if (_userInfoRepository.GetByUsername(Login).Password == Password)
            {
                MessageBox.Show("користувача знайдено");
            }
            else
            {
                MessageBox.Show("користувача не знайдено");
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            _factory.Create().Show();
        }
    }
}
