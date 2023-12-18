using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AniBLL.Models;
using AniBLL.Services;
using AniDAL.Repositories;
using AniWPF.StartupHelper;
using Microsoft.Extensions.Logging;

namespace AniWPF
{
    public partial class RegistrationWindow : Window, IWindowAware
    {
        private readonly ILogger<RegistrationWindow> logger;

        private readonly IAbstractFactory<LogInWindow> logInFactory;

        public Window ParentWindow { get; set; }

        private readonly IUserService userService;

        public RegistrationWindow(IUserService userService, IAbstractFactory<LogInWindow> lfactory,
                                  ILogger<RegistrationWindow> logger)
        {
            this.logInFactory = lfactory;

            this.userService = userService;

            this.logger = logger;
            this.logger.LogInformation("RegistrationWindow created");

            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            this.logger.LogInformation("Click Registration button");

            string username = in_login.Text;
            string email = in_email.Text;
            string password = in_password.Password;

            if (userService.IsExistUsername(username))
            {
                this.logger.LogInformation("User with this username already exists");
                MessageBox.Show("користувач з таким логіном вже існує");
                in_login.Text = String.Empty;
                in_email.Text = String.Empty;
                in_password.Password = String.Empty;
            }
            else if (userService.IsExistEmail(email))
            {
                this.logger.LogInformation("User with this email already exists");
                MessageBox.Show("користувач з такою поштою вже існує");
                in_login.Text = String.Empty;
                in_email.Text = String.Empty;
                in_password.Password = String.Empty;
            }
            else
            {
                int currentid = userService.GetLastId() + 1;
                UserInfoModel newUser = new UserInfoModel()
                {
                    Id = currentid,
                    Name = "лох",
                    Level = 1,
                    Text = "лалала",
                    Photo = "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/defaultUserPhoto.jpg",
                    WatchedCount = 0,
                    Username = username,
                    Email = email,
                    Password = password
                };

                userService.Insert(newUser);
                this.logger.LogInformation("New user was added successfully");
                in_login.Text = String.Empty;
                in_email.Text = String.Empty;
                in_password.Password = String.Empty;
                //MessageBox.Show("Реєстрація пройшла успішно!");
                logInFactory.Create(this.ParentWindow).Show();
                this.Close();

            }
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            in_login.Text = String.Empty;
            in_email.Text = String.Empty;
            in_password.Password = String.Empty;
            logInFactory.Create(this.ParentWindow).Show();
            this.Close();
        }
    }
}
