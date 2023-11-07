using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AniWPF.StartupHelper
{
    public static class ServiseExtensions
    {
        public static void AddFormFactory<TForm>(this IServiceCollection services)
    where TForm : class
        {
            services.AddTransient<TForm>();
            services.AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!);
            services.AddSingleton<IAbstractFactory<TForm>, AbstractFactory<TForm>>();
        }
    }
}
