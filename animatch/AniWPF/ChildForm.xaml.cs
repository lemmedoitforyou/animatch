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
    public partial class ChildForm : Window, IWindowAware
    {
        public Window ParentWindow { get; set; }
        private readonly IUserService userService;
        private readonly IAbstractFactory<MainWindow> mainFactory;

        public ChildForm(IUserService userService, IAbstractFactory<MainWindow> mfactory)
        {
            InitializeComponent();
            this.userService = userService;
            this.mainFactory = mfactory;
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

                int id = currentid;
                string name = "додати ім'я";
                int level = 0;
                string text = "додати підпис";
                string photo = "defaultphoto.jpg";
                int watchedCount = 0;

                userService.Add(id, username, email, password, name, level, text, photo, watchedCount);
                MessageBox.Show("Реєстрація пройшла успішно!");
                mainFactory.Create(this.ParentWindow).Show(); // Передаємо батьківське вікно як батьківське
            }
        }
    }
}
