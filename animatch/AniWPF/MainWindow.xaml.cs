using AniDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using AniDAL;
using AniWPF.StartupHelper;
using AniBLL.Services;

namespace AniWPF
{
    public partial class MainWindow : Window
    {
        private readonly IAnimeService _animeService;
        private readonly IAbstractFactory<ChildForm> _factory;

        public MainWindow(IAnimeService animeService, IAbstractFactory<ChildForm> factory)
        {
            InitializeComponent();
            _animeService = animeService;
            _factory = factory;
        }

        private void getAnime_Click(object sender, RoutedEventArgs e)
        {
            data.Text = _animeService.GetById(1).ToString();
        }

        private void openChildForm_Click(object sender, RoutedEventArgs e)
        {
            _factory.Create().Show();
        }
    }
}
