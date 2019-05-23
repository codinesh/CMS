using CMS.Core;
using CMS.Model;
using CMS.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CMS
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            await serviceProvider.GetService<ConferenceTrackSchedulerClient>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            serviceCollection.AddLogging();

            // build configuration
            var configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", false)
              .Build();
            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));
            serviceCollection.Configure<ConferenceSettings>(configuration.GetSection("ConferenceSettings"));

            serviceCollection.AddTransient<IScheduler, SchedulerService>();
            serviceCollection.AddTransient<ITrackOrganizer, BestFitTrackOrganizer>();
            serviceCollection.AddTransient<ITextInputReader, TextInputReader>();
            serviceCollection.AddTransient<ConferenceTrackSchedulerClient>();
        }
    }
}
