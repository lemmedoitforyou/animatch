using System;
using Microsoft.Extensions.DependencyInjection;

namespace AniWPF.StartupHelper
{
    public static class ServiceExtensions
    {
        public static void AddFormFactory<TForm>(this IServiceCollection services)
            where TForm : class
        {
            services.AddTransient<TForm>();
            services.AddSingleton<Func<TForm>>(x => () =>
            {
                var formService = x.GetService<TForm>();
                if (formService == null)
                {
                    throw new InvalidOperationException($"Could not resolve service for {typeof(TForm)}");
                }

                return formService;
            });
            services.AddSingleton<IAbstractFactory<TForm>, AbstractFactory<TForm>>();
        }
    }
}
