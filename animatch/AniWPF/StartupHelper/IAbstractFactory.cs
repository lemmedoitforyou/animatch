namespace AniWPF.StartupHelper
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}