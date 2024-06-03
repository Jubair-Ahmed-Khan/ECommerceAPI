using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.DTOs
{
    public class VariantDTO
    {
        public string Color { get; set; }
        public string Size { get; set; }
        public List<WarehouseStockDTO> WarehouseStocks { get; set; }
    }
}
