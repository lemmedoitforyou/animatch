using System;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class MainWindow : Window
    {
        private readonly IUserService userService;
        private readonly IAbstractFactory<main> mainFactory;
        private readonly IAbstractFactory<ChildForm> childFactory;

        public MainWindow(IUserService uService, IAbstractFactory<main> mfactory, IAbstractFactory<ChildForm> cfactory)
        {
            this.InitializeComponent();
            this.userService = uService;
            this.mainFactory = mfactory;
            this.childFactory = cfactory;
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            string loginValue = this.login.Text;
            string passwordValue = this.password.Password;
            var user = this.userService.GetByUsername(loginValue);

            if (user != null)
            {
                if (user.Password == passwordValue)
                {
                    MessageBox.Show("Користувача знайдено");
                    this.mainFactory.Create().Show();
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

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            this.childFactory.Create().Show();
        }
    }
}