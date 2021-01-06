using CsvImporter.Data.Repository.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CsvImporter.Data.Repository.Implementation
{
    public class AcmeDatabaseConnection : IAcmeDatabaseConnection
    {
        private string _connectionString;
        private string _tablename;

        SqlConnection connection;
        SqlCommand command;

        protected readonly ILogger<AcmeDatabaseConnection> _logger;

        public AcmeDatabaseConnection(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AcmeDatabaseConnection>();
        }

        /// <summary>
        /// Configuration for setting connection properties
        /// </summary>
        /// <param name="connectionString">connection string for database</param>
        /// <param name="tablename"></param>
        public void DatabaseConnectionConfiguration(string connectionString, string tablename)
        {
            _connectionString = connectionString;
            _tablename = tablename;
        }


        /// <summary>
        /// Inserts datareader information into Acme database
        /// </summary>
        /// <param name="reader">object with data information</param>
        /// <returns>asynchronus operation</returns>
        public async Task InsertReaderIntoDatabase(IDataReader reader)
        {
            if (GetCountRows() > 0)
                DeleteRecordsFromDB();

            try
            {
                SqlBulkCopy bulkcopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.UseInternalTransaction);
                bulkcopy.BatchSize = 1000;
                bulkcopy.DestinationTableName = _tablename;
                bulkcopy.NotifyAfter = 1000;
                bulkcopy.SqlRowsCopied += (sender, e) =>
                {
                    Console.WriteLine("Written: " + e.RowsCopied.ToString());
                };
                await bulkcopy.WriteToServerAsync(reader);   
                bulkcopy.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Deletes all records from table
        /// </summary>
        public void DeleteRecordsFromDB()
        {
            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand("DELETE FROM " + _tablename, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// Get the current count of rows from the table
        /// </summary>
        /// <returns>quantity rows</returns>
        public int GetCountRows()
        {
            int count = 0;
            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT COUNT(*) FROM " + _tablename, connection);
                count = (Int32)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return count;
        }
    }
}
