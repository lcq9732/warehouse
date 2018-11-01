using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class WarehouseOut : ModelBase
    {
        public string SendBy { get; set; }
        public string RequestedBy { get; set; }
        public string AuditBy { get; set; }
        public DateTime BillDate { get; set; }
        public string ReviewedBy { get; set; }
        [NotMapped]
        public IList<WarehouseOutItem> Items { get; set; }
    }

    public class WarehouseOutItem : ModelBase
    {
        public WarehouseOutItem()
        {
            Product_Id = 0;
        }
        public int Product_Id { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string Brand { get; set; }
        [NotMapped]
        public string Specification { get; set; }
        [NotMapped]
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string Memo { get; set; }
        public int WarehouseOut_Id { get; set; }
    }
}
