using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

//https://pioneercode.com/post/dependency-injection-logging-and-configuration-in-a-dot-net-core-console-app

namespace CMS
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            List<Talk> talks = new List<Talk>{
                new Talk("Writing Fast Tests Against Enterprise Rails", 60),
                new Talk("Overdoing it in Python", 45),
                new Talk("Lua for the Masses", 30),
                new Talk("Ruby Errors from Mismatched Gem Versions", 45),
                new Talk("Common Ruby Errors", 45),
                new Talk("Rails for Python Developers", 5),
                new Talk("Communicating Over Distance", 60),
                new Talk("Accounting-Driven Development", 45),
                new Talk("Woah", 30),
                new Talk("Sit Down and Write", 30),
                new Talk("Pair Programming vs Noise", 45),
                new Talk("Rails Magic", 60),
                new Talk("Ruby on Rails: Why We Should Move On", 60),
                new Talk("Clojure Ate Scala (on my project)", 45),
                new Talk("Programming in the Boondocks of Seattle", 30),
                new Talk("Ruby vs. Clojure for Back-End Development ", 30),
                new Talk("Ruby on Rails Legacy App Maintenance", 60),
                new Talk("A World Without HackerNews", 30),
                new Talk("User Interf1ace CSS in Rails Apps", 30)
            };

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<SchedulerService>().Initialize(talks);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add logging
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

            // Get table storage connection string for serilog
            //CloudStorageAccount logStorageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("LoggingStorage"));

            serviceCollection.AddTransient<SchedulerService>();
            serviceCollection.AddTransient<ITrackOrganizer, TrackOrganizer>();
        }
    }
}
