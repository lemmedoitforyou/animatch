using System;
using System.Windows;

namespace AniWPF.StartupHelper
{
    public class AbstractFactory<T> : IAbstractFactory<T>
    {
        private readonly Func<Window, T> factory;

        public AbstractFactory(Func<Window, T> factory)
        {
            this.factory = factory;
        }
       
        public T Create(Window parentWindow)
        {
            return this.factory(parentWindow);
        }
    }
}
