using CsvImporter.Common.Validations;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CsvImporter.Test.Validation
{
    [Trait("Category", "Delimiter Validation")]
    public class UnitTestDelimiterValidation : IDisposable
    {
        private readonly ITestOutputHelper _output;
        public UnitTestDelimiterValidation(ITestOutputHelper output)
        {
            _output = output;
        }

        public void Dispose()
        {
        }


        [Fact]
        [Trait("Category", "Check valid delimiter for comma")]
        public void Test_CheckValidDelimiterComma_Ok()
        {
            _output.WriteLine("Check valid delimiter for comma");

            char[] delimiter = { ',' };
            var result = CSVDelimiterValidator.Validates(delimiter);

            Assert.True(result);
        }


        [Fact]
        [Trait("Category", "Check valid delimiter for semicolon")]
        public void Test_CheckValidDelimiterSemicolon_Ok()
        {
            _output.WriteLine("Check valid delimiter for semicolon");

            char[] delimiter = { ';' };
            var result = CSVDelimiterValidator.Validates(delimiter);

            Assert.True(result);
        }



        [Fact]
        [Trait("Category", "Check invalid delimiter for char array")]
        public void Test_CheckInvalidDelimiterLenght_Ok()
        {
            _output.WriteLine("Check invalid delimiter for char array");

            char[] delimiter = { '.',',' };
            var result = CSVDelimiterValidator.Validates(delimiter);

            Assert.False(result);
        }


        [Fact]
        [Trait("Category", "Check invalid delimiter for invalid char")]
        public void Test_CheckInvalidDelimiterChar_Ok()
        {
            _output.WriteLine("Executing GetCommand Test");

            char[] delimiter = { '*' };
            var result = CSVDelimiterValidator.Validates(delimiter);

            Assert.False(result);
        }
    }
}
