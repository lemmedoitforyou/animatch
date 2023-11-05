using AniDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using AniDAL;

namespace AniWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly ChildForm __childForm;

        public MainWindow(IAnimeRepository animeAccess, ChildForm childForm)
        {
            InitializeComponent();
            _animeRepository = animeAccess;
        }

        private void getAnime_Click(object sender, RoutedEventArgs e)
        {
            data.Text = _animeRepository.GetAll().ToString();
        }

        private void openChildForm_Click(object sender, RoutedEventArgs e)
        {
            __childForm.Show();
        }
    }
}
