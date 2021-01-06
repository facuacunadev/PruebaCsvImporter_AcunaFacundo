using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.Data.Repository.Interface
{
    public interface IAcmeDatabaseConnection
    {
        void DatabaseConnectionConfiguration(string connectionString, string tablename);

        Task InsertReaderIntoDatabase(IDataReader reader);
    }
}
