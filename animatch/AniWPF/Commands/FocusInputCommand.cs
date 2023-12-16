using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AniWPF.Commands
{
    public static class FocusInputCommand
    {
        public static ICommand AnimeButtonClickCommand { get; } = new RelayCommand(ExecuteAnimeButtonClick, CanExecuteAnimeButtonClick);

        private static void ExecuteAnimeButtonClick(object? parameter)
        {
            if (parameter is Tuple<string, TextBox> tuple)
            {
                if (tuple.Item2.Text == tuple.Item1)
                {
                    tuple.Item2.Text = string.Empty;
                }
            }
        }

        private static bool CanExecuteAnimeButtonClick(object? parameter)
        {
            Debug.WriteLine("CanExecuteAnimeButtonClick called!");
            return true;
        }
    }
}
