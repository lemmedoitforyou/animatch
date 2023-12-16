using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AniWPF.StartupHelper;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Data.Common;

namespace AniWPF.Commands
{
    public static class AnimeCommand
    {
        public static ICommand AnimeButtonClickCommand { get; } = new RelayCommand(ExecuteAnimeButtonClick, CanExecuteAnimeButtonClick);

        private static void ExecuteAnimeButtonClick(object par)
        {
            if (par is MainWindow window)
            {
                MainWindow parameter = (MainWindow)par;
                parameter.logger.LogInformation("Click detail about anime button");
                AnimeWindow.ParentWindow = parameter;
                parameter.animeFactory.Create(parameter).Show();
                parameter.Close();
            }
            else if (par is RandomWindow)
            {
                RandomWindow parameter = (RandomWindow)par;
                parameter.logger.LogInformation("Click detail about anime button");
                AnimeWindow.ParentWindow = parameter;
                parameter.animeFactory.Create(parameter).Show();
                parameter.Close();
            }
        }

        private static bool CanExecuteAnimeButtonClick(object? parameter)
        {

            Debug.WriteLine("CanExecuteAnimeButtonClick called!");
            return true;
        }
    }
}
