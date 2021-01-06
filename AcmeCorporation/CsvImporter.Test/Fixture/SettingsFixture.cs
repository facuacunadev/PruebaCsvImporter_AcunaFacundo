using CsvImporter.Common.Configurations;


namespace CsvImporter.Test.Fixture
{
    public class SettingsFixture
    {
        private AppSettings settings;

        public AppSettings ConfigureLocalSettings()
        {
            settings = new AppSettings();
            settings.ConnectionString = "Data Source=PF1LZJ1320L6\\SQLEXPRESS ;Initial Catalog=AcmeCorpDB; Integrated Security=SSPI";
            settings.TableName = "Items";

            char[] delimiter = { ';' };
            settings.CsvDelimiter = delimiter;
            settings.CsvFilePath = "..\\..\\..\\..\\Input\\Stock.xls";
            settings.CsvFileType = "Local";

            return settings;
        }


        public AppSettings ConfigureRemoteSettings()
        {
            settings = new AppSettings();
            settings.ConnectionString = "Data Source=PF1LZJ1320L6\\SQLEXPRESS ;Initial Catalog=AcmeCorpDB; Integrated Security=SSPI";
            settings.TableName = "Items";

            char[] delimiter = { ';' };
            settings.CsvDelimiter = delimiter;
            settings.CsvFilePath = "https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV";
            settings.CsvFileType = "Remote";

            return settings;
        }

    }
}
