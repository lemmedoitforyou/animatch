using System.Windows;

namespace AniWPF.StartupHelper
{
    public interface IAbstractFactory<T>
    {

        T Create(Window parentWindow);
        Window ParentWindow { get; set; }
    }
}