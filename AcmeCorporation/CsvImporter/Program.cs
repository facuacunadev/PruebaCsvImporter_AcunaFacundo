using CsvImporter.Business.ImporterManager.Implementation;
using CsvImporter.Business.ImporterManager.Interface;
using CsvImporter.Data.Repository.Implementation;
using CsvImporter.Data.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CsvImporter
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        /// <summary>
        /// Run the main method 
        /// </summary>
        /// <param name="args">console parameters</param>
        /// <returns></returns>
        static async Task MainAsync(string[] args)
        {
            // Create service collection
            Log.Information("Creating service collection");
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureSettings(serviceCollection);
            ConfigureLogging(serviceCollection);
            ConfigureServices(serviceCollection);
         
            
            // Create service provider
            Log.Information("Building service provider");
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                Log.Information("Starting service");
                await serviceProvider.GetService<App>().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                Log.Information("Ending service");
                Log.CloseAndFlush();
            }
        }


        /// <summary>
        /// Adds serviceCollection to configure dependencies
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection list</param>
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //Add Dependency Injection
            serviceCollection.AddTransient<ICSVDataImporter, CSVDataImporterManager>();
            serviceCollection.AddTransient<ICSVDataReader, CSVDataReader>();
            serviceCollection.AddTransient<IAcmeDatabaseConnection, AcmeDatabaseConnection>();


            // Add app
            serviceCollection.AddTransient<App>();
        }

        /// <summary>
        /// Adds configuration settings to service collection
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void ConfigureSettings(IServiceCollection serviceCollection)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        }


        /// <summary>
        /// Adds configurations and instances for log libraries 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void ConfigureLogging(IServiceCollection serviceCollection) {

            var loggingpath = configuration.GetSection("ConfigurationLog:FilePath").Value;

            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                 .MinimumLevel.Debug()
                 .WriteTo.File(loggingpath, Serilog.Events.LogEventLevel.Debug, 
                    rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                 .Enrich.FromLogContext()
                 .CreateLogger();

            // Add logging
            serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true);
            }));
            serviceCollection.AddLogging();
        }
    }
}
