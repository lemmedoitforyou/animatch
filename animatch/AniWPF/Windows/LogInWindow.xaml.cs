using System;
using System.Windows;
using AniBLL.Services;
using AniWPF.StartupHelper;
using Microsoft.Extensions.Logging;

namespace AniWPF
{
    public partial class LogInWindow : Window, IWindowAware
    {
        private readonly ILogger<LogInWindow> logger;

        private readonly IAbstractFactory<MainWindow> mainFactory;
        private readonly IAbstractFactory<RegistrationWindow> registrationFactory;

        private readonly IUserService userService;

        public static int CurrentUserID { get; set; }
        public Window ParentWindow { get; set; }

        public LogInWindow(IUserService uService, IAbstractFactory<MainWindow> mfactory, 
                           IAbstractFactory<RegistrationWindow> rfactory, ILogger<LogInWindow> logger)
        {
            mainFactory = mfactory;
            registrationFactory = rfactory;

            userService = uService;

            InitializeComponent();
            this.WindowState = WindowState.Maximized;

            this.logger = logger;
            this.logger.LogInformation("LogInWindow created");
        }

        public void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            string loginValue = login.Text;
            string passwordValue = password.Password;
            var user = userService.GetByUsername(loginValue);

            if (user != null)
            {
                if (user.Password == passwordValue)
                {
                    //MessageBox.Show("Користувача знайдено");
                    this.logger.LogInformation("User was found, login successfully");
                    CurrentUserID = user.Id;
                    mainFactory.Create(this).Show(); 
                    
                    this.Close();
                    
                }
                else
                {
                    this.logger.LogInformation("User was found, but the password is incorrect");
                    MessageBox.Show("Пароль неправильний");
                }
            }
            else
            {
                this.logger.LogInformation("User was not found");
                MessageBox.Show("Користувача не знайдено");
            }
        }

        public void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Register button");
            registrationFactory.Create(this).Show();
            this.Close();
        }
    }
}