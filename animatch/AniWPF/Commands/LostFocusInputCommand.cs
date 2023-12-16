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
    public class LostFocusInputCommand
    {
        private readonly TextBox _textBox;
        private readonly string _text;

        public LostFocusInputCommand(TextBox textBox, string text)
        {
            _textBox = textBox;
            _text = text;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_textBox.Text))
            {
                _textBox.Text = _text;
            }
        }

        //public event EventHandler? CanExecuteChanged;
    }
}
