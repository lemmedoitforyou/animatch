using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AniWPF.Commands
{
    class LostFocusInputCommand
    {
        public static ICommand AnimeButtonClickCommand { get; } = new RelayCommand(ExecuteAnimeButtonClick, CanExecuteAnimeButtonClick);

        private static void ExecuteAnimeButtonClick(object? parameter)
        {
            if (parameter is Tuple<string, TextBox> tuple)
            {
                if (string.IsNullOrWhiteSpace(tuple.Item2.Text))
                {
                    tuple.Item2.Text = "Enter anime title";
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
