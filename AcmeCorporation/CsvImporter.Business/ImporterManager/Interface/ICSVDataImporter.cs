using CsvImporter.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.Business.ImporterManager.Interface
{
    public interface ICSVDataImporter
    {
        Task ImportCsvFile(AppSettings _settings);
    }
}
