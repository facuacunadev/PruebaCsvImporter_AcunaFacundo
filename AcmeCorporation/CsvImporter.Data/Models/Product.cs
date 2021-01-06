using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Data.Models
{
    public class Item
    {
        public string PointOfSale { get; set; }
        public string Product { get; set; }
        public int Date { get; set; }
        public DateTime Stock { get; set; }
    }
}
