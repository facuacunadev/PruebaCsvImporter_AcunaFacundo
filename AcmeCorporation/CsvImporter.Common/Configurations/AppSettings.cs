using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Common.Configurations
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }

        public string CsvFilePath { get; set; }
        public char[] CsvDelimiter { get; set; }
        public string CsvFileType { get; set; }
    }
}
