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
    public partial class ChildForm : Window
    {
        private readonly IUserService _userService;
        private readonly IAbstractFactory<MainWindow> _Main_factory;

        public ChildForm(IUserService userService, IAbstractFactory<MainWindow> Mfactory)
        {
            InitializeComponent();
            _userService = userService;
            _Main_factory = Mfactory;
        }

        private void registration_Click(object sender, RoutedEventArgs e)
        {
            string Username = in_login.Text;
            string Email = in_email.Text;
            string Password = in_password.Text;

            if (_userService.IsExistUsername(Username))
            {
                MessageBox.Show("користувач з таким логіном вже існує");
            }
            else if (_userService.IsExistEmail(Email))
            {
                MessageBox.Show("користувач з такою поштою вже існує");
            }
            else
            {
                int currentid = _userService.GetLastUserId() + 1;

                int Id = currentid;
                string Name = "додати ім'я";
                int Level = 0;
                string Text = "додати підпис";
                string Photo = "defaultphoto.jpg";
                int WatchedCount = 0;
               

                _userService.Add(Id, Username, Email, Password, Name, Level, Text, Photo, WatchedCount);
                MessageBox.Show("Реєстрація пройшла успішно!");
                _Main_factory.Create().Show();
            }
        }
    }
}
