using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CsvImporter.Business.ImporterManager.Interface
{
    public interface ICSVDataReader : IDataReader, IDisposable
    {
        void DatareaderConfiguration(string filePath, char delimiter, string fileType, bool firstRowHeader = true);
    }
}
