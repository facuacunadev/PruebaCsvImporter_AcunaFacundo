using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Common.Validations
{
    public static class CSVDelimiterValidator
    {
        public static bool Validates(char[] delimiter)
        {
            bool result = true;

            if(delimiter.Length > 1){
                result = false;
                
            } else if (!delimiter[0].Equals(',') && !delimiter[0].Equals(';'))
            {
                result = false;
            }

            return result;
        }
    }
}
