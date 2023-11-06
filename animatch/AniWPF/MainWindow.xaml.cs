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
    public partial class MainWindow : Window
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly ChildForm _childForm;

        public MainWindow(IAnimeRepository animeAccess, ChildForm childForm)
        {
            InitializeComponent();
            _animeRepository = animeAccess;
            _childForm = childForm;
        }

        private void getAnime_Click(object sender, RoutedEventArgs e)
        {
            data.Text = _animeRepository.GetById(1).ToString();
        }

        private void openChildForm_Click(object sender, RoutedEventArgs e)
        {
            _childForm.Show();
        }
    }
}
