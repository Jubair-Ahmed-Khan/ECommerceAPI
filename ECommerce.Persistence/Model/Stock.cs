using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Model
{
    public class Stock
    {
        public int ID { get; set; }
        public int VariantID { get; set; }
        public int WarehouseID { get; set; } 
        public int Quantity { get; set; }
        public Variant Variant { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
