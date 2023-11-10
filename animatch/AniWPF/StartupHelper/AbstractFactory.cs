using System;

namespace AniWPF.StartupHelper
{
    public class AbstractFactory<T> : IAbstractFactory<T>
    {
        private readonly Func<T> factory;

        public AbstractFactory(Func<T> factory)
        {
            this.factory = factory;
        }

        public T Create()
        {
            return this.factory();
        }
    }
}
