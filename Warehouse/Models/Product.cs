using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Product:ModelBase
    {
        public Product()
        {
            Id = 0;
            ProductName = "";
            Brand = "";
            Specification = "";
            Unit = "";
            Memo = "";
            UnitPrice = 0;
            StockQuantity = 0;
        }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Specification { get; set; }
        public string Unit { get; set; }
        public string Memo { get; set; }
        public double UnitPrice { get; set; }
        public double StockQuantity { get; set; }
    }
}
