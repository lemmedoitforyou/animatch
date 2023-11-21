using System;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class LogInWindow : Window, IWindowAware
    {
        public static int CurrentUserID { get; set; }
        public Window ParentWindow { get; set; }
        private readonly IUserService userService;
        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<RegistrationWindow> registrationFactory;

        public LogInWindow(IUserService uService, IAbstractFactory<MainWindow> mfactory, IAbstractFactory<RegistrationWindow> rfactory)
        {
            InitializeComponent();
            userService = uService;
            mainFactory = mfactory;
            registrationFactory = rfactory;
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
                    //MessageBox.Show("Користувача знайдено");
                    mainFactory.Create(this).Show(); // Передаємо поточне вікно як батьківське
                    CurrentUserID = user.Id;
                    this.Close();
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
            registrationFactory.Create(this).Show();
            this.Close();// Передаємо поточне вікно як батьківське
        }
    }
}