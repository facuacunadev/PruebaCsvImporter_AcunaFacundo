using CsvImporter.Business.ImporterManager.Interface;
using CsvImporter.Common.Configurations;
using CsvImporter.Common.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CsvImporter
{
    public class App
    {
        private AppSettings _settings;
        private readonly IConfigurationRoot _config;
        private readonly ICSVDataImporter _dataImporter;
        private readonly ILogger<App> _logger;


        public App(IConfigurationRoot config, ICSVDataImporter dataImporter, ILoggerFactory loggerFactory)
        {
            _config = config;
            _dataImporter = dataImporter;
            _logger = loggerFactory.CreateLogger<App>();
        }



        /// <summary>
        /// Run main task for data importer
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            _settings = new AppSettings();
            GetConfigurationSettings(ref _settings);

            if (!ValidatesDelimiter(_settings.CsvDelimiter))
                throw new Exception("Invalid delimiter format, delimiter only can be comma or semicolon");

            await ExecuteImportCsvFile(_settings);
        }


        /// <summary>
        /// Executes data importer
        /// </summary>
        /// <param name="_settings">settings parameters</param>
        /// <returns></returns>
        private async Task ExecuteImportCsvFile(AppSettings _settings)
        {
            _logger.LogInformation("Start Import CsvFile", "App");
            await _dataImporter.ImportCsvFile(_settings);
        }




        #region Configuration

        private bool ValidatesDelimiter(char[] csvDelimiter)
        {
            return CSVDelimiterValidator.Validates(csvDelimiter);
        }


        private void GetConfigurationSettings(ref AppSettings _settings)
        {
            _settings.ConnectionString = _config.GetSection("ConnectionStrings:DatabaseConnection").Value;
            _settings.TableName = _config.GetSection("ConnectionStrings:DatabaseTable").Value;

            _settings.CsvFilePath = _config.GetSection("Data:InputFile:FilePath").Value;
            _settings.CsvDelimiter = _config.GetSection("Data:InputFile:Delimiter").Value.ToCharArray();
            _settings.CsvFileType = _config.GetSection("Data:InputFile:Type").Value;
        }
        #endregion

    }
}