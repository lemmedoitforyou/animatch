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
using System.Windows;
using AniWPF;
using System;
using AniDAL;
using AniWPF.StartupHelper;
using AniBLL.Services;

namespace AniWPF
{
    public partial class MainWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IAbstractFactory<Main> _main_factory;
        private readonly IAbstractFactory<ChildForm> _child_factory;

        public MainWindow(IUserService userService, IAbstractFactory<Main> mfactory, IAbstractFactory<ChildForm> cfactory)
        {
            InitializeComponent();
            _userService = userService;
            _main_factory = mfactory;
            _child_factory = cfactory;
        }

        private void buttonEnter_Click(object sender, RoutedEventArgs e)
        {
            string loginValue = login.Text;
            string passwordValue = password.Password;
            var user = _userService.GetByUsername(loginValue);

            if (user != null)
            {
                if (user.Password == passwordValue)
                {
                    MessageBox.Show("Користувача знайдено");
                    _main_factory.Create().Show();
                }
                else
                {
                    MessageBox.Show("Пароль неправильний");
                }
            }
            else
            {
                MessageBox.Show("Користувача не знайдено");
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            _child_factory.Create().Show();
        }
    }
}