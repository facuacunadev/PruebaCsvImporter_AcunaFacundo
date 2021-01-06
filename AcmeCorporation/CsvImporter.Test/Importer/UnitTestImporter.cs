using CsvImporter.Business.ImporterManager.Implementation;
using CsvImporter.Business.ImporterManager.Interface;
using CsvImporter.Common.Configurations;
using CsvImporter.Data.Repository.Implementation;
using CsvImporter.Data.Repository.Interface;
using CsvImporter.Test.Fixture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CsvImporter.Test
{
    [Trait("Category: Importer", "Import data unit testing")]
    public class UnitTestImporter : IDisposable
    {
        private readonly ITestOutputHelper _output;

        private CSVDataReader reader;
        private AcmeDatabaseConnection dataConnection;
        private CSVDataImporterManager importer;

        public UnitTestImporter(ITestOutputHelper output)
        {
            _output = output;
        }

        public void Dispose()
        {
        }



        [Fact]
        [Trait("Category", "Import CSV data from local source")]
        public void Test_ImportCSVFileLocal_Ok()
        {
            _output.WriteLine("Import CSV data from local source");

            SettingsFixture fixtureSettings = new SettingsFixture();
            var settings = ConfigureSettingsLocal(fixtureSettings);

            #region Arrange
            ConfigureLoggers();
            #endregion

            #region Act
            var res = importer.ImportCsvFile(settings);
            #endregion

            #region Assert
            Assert.True(dataConnection.GetCountRows() > 0);
            #endregion
        }


        [Fact]
        [Trait("Category", "Import CSV data from remote source")]
        public void Test_ImportCSVFileRemote_Ok()
        {
            _output.WriteLine("Import CSV data from remote source");

            SettingsFixture fixtureSettings = new SettingsFixture();
            var settings = ConfigureSettingsRemote(fixtureSettings);

            #region Arrange
            ConfigureLoggers();
            #endregion

            #region Act
            var res = importer.ImportCsvFile(settings);
            #endregion

            #region Assert
            Assert.True(dataConnection.GetCountRows() > 0);
            #endregion
        }



        private AppSettings ConfigureSettingsLocal(SettingsFixture fixture)
        {
            return fixture.ConfigureLocalSettings();
        }

        private AppSettings ConfigureSettingsRemote(SettingsFixture fixture)
        {
            return fixture.ConfigureRemoteSettings();
        }


        public CSVDataImporterManager ConfigureLoggers()
        {
            var serviceProvider = new ServiceCollection()
               .AddLogging()
               .BuildServiceProvider();
            var factoryReader = serviceProvider.GetService<ILoggerFactory>();
            var factoryConnection = serviceProvider.GetService<ILoggerFactory>();
            var factoryImporter = serviceProvider.GetService<ILoggerFactory>();

            reader = new CSVDataReader(factoryReader);
            dataConnection = new AcmeDatabaseConnection(factoryConnection);
            importer = new CSVDataImporterManager(reader, dataConnection, factoryImporter);

            return importer;
        }

    }
}