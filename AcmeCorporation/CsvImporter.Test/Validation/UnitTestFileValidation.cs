using CsvImporter.Common.Validations;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CsvImporter.Test
{
    [Trait("Category", "Unit test")]
    public class UnitTestFileValidation : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private CSVFileValidator fileValidator;
        private string filePathOk = "..\\..\\..\\..\\Input\\Stock.xls";
        private string filePathNOOk = "..\\..\\..\\..\\Input\\WrongName.xls";
        private string fileUrlOk = "https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV";
        private string fileUrlNOOk = "https://storage10082020.blob.core.windows.net/Stock.CSV";

        public UnitTestFileValidation(ITestOutputHelper output)
        {
            _output = output;
        }

        public void Dispose()
        {
        }


        [Fact]
        [Trait("File Validation", "Check valid if file exists in local folder")]
        public void Test_CheckFileExistsLocal_Ok()
        {
            _output.WriteLine("Check valid if file exists - Ok result");

            fileValidator = new CSVFileValidator();
            var result = fileValidator.ValidatesFileExist(filePathOk, "Local");

            Assert.True(result);
        }



        [Fact]
        [Trait("File Validation", "Check valid if file no exists in local folder")]
        public void Test_CheckFileExistsLocal_NoOk()
        {
            _output.WriteLine("Check valid if file no exists - No Ok result");

            CSVFileValidator fileValidator = new CSVFileValidator();
            var result = fileValidator.ValidatesFileExist(filePathNOOk, "Local");

            Assert.False(result);
        }


        [Fact]
        [Trait("File Validation", "Check valid if file exists in local url")]
        public void Test_CheckFileExistsRemote_Ok()
        {
            _output.WriteLine("Check valid if file exists - Ok result");

            CSVFileValidator fileValidator = new CSVFileValidator();
            var result = fileValidator.ValidatesFileExist(fileUrlOk, "Remote");

            Assert.True(result);
        }



        [Fact]
        [Trait("File Validation", "Check valid if file no exists in local url")]
        public void Test_CheckFileExistsRemote_NoOk()
        {
            _output.WriteLine("Check valid if file no exists - No Ok result");

            CSVFileValidator fileValidator = new CSVFileValidator();
            var result = fileValidator.ValidatesFileExist(fileUrlNOOk, "Remote");

            Assert.False(result);
        }

    }
}
