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
using AniBLL.Services;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;
using AniWPF.StartupHelper;

namespace AniWPF
{
    public partial class RegistrationWindow : Window, IWindowAware
    {
        public Window ParentWindow { get; set; }
        private readonly IUserService userService;
        private readonly IAbstractFactory<LogInWindow> logInFactory;

        public RegistrationWindow(IUserService userService, IAbstractFactory<LogInWindow> lfactory)
        {
            InitializeComponent();
            this.userService = userService;
            this.logInFactory = lfactory;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string username = in_login.Text;
            string email = in_email.Text;
            string password = in_password.Text;

            if (userService.IsExistUsername(username))
            {
                MessageBox.Show("користувач з таким логіном вже існує");
            }
            else if (userService.IsExistEmail(email))
            {
                MessageBox.Show("користувач з такою поштою вже існує");
            }
            else
            {
                int currentid = userService.GetLastUserId() + 1;
                UserInfo newUser = new UserInfo()
                {
                    Id = currentid,
                    Name = "додати ім'я",
                    Level = 0,
                    Text = "додати підпис",
                    Photo = "defaultphoto.jpg",
                    WatchedCount = 0,
                    Username = username,
                    Email = email,
                    Password = password
                };

                userService.Insert(newUser);
                //MessageBox.Show("Реєстрація пройшла успішно!");
                logInFactory.Create(this.ParentWindow).Show(); // Передаємо батьківське вікно як батьківське
                this.Close();
            }
        }

    }
}
