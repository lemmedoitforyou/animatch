using System;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class MainWindow : Window, IWindowAware
    {
        public Window ParentWindow { get; set; }
        private readonly IUserService userService;
        private readonly IAbstractFactory<Main> mainFactory;
        private readonly IAbstractFactory<ChildForm> childFactory;

        public MainWindow(IUserService uService, IAbstractFactory<Main> mfactory, IAbstractFactory<ChildForm> cfactory)
        {
            InitializeComponent();
            userService = uService;
            mainFactory = mfactory;
            childFactory = cfactory;
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            string loginValue = login.Text;
            string passwordValue = password.Password;
            var user = userService.GetByUsername(loginValue);

            if (user != null)
            {
                if (user.Password == passwordValue)
                {
                    MessageBox.Show("Користувача знайдено");
                    mainFactory.Create(this).Show(); // Передаємо поточне вікно як батьківське
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
            childFactory.Create(this).Show(); // Передаємо поточне вікно як батьківське
        }
    }
}