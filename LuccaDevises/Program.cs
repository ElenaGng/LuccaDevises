using System;
using System.IO;
using System.Text;
using LuccaDevises.Module;
using Microsoft.Extensions.DependencyInjection;

namespace LuccaDevises
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();

            var service = serviceProvider.GetService<ICurrencyExchange>();
            service.ConvertCurrencyFromExternalFile(args[0]);
        }

        private static void RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IFileManager, FileManager>();
            serviceCollection.AddScoped<ICurrencyExchange, CurrencyExchange>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
