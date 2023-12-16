using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AniWPF.Commands
{
    public static class AnimeFromListCommand
    {
        public static ICommand AnimeButtonClickCommand { get; } = new RelayCommand(ExecuteAnimeButtonClick, CanExecuteAnimeButtonClick);

        private static void ExecuteAnimeButtonClick(object parameter)
        {
            //if (parameter is Tuple<MainWindow, AnimeForForw> tuple)
            //{
            //    MainWindow mainWindow = tuple.Item1;
            //    AnimeForForw clickedItem = tuple.Item2;

            //    string temp = clickedItem.Title;
            //    AnimeForForw foundAnime = mainWindow.animeList.FirstOrDefault(anime => anime.Title == temp);
            //    if (foundAnime != null)
            //    {
            //        mainWindow.CurrentId = foundAnime.Id;
            //    }

            //    AnimeWindow.ParentWindow = mainWindow;
            //    mainWindow.animeFactory.Create(mainWindow).Show();
            //    mainWindow.Close();
            //}
        }

        private static bool CanExecuteAnimeButtonClick(object? parameter)
        {

            Debug.WriteLine("CanExecuteAnimeButtonClick called!");
            return true;
        }
    }
}
