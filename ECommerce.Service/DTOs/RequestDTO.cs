using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.DTOs
{
    public class RequestDTO
    {
        public int ProductID { get; set; }
        public int? WarehouseId { get; set; }
        public string Name { get; set; }
        public string VariantColor { get; set; }
        public string VariantSize { get; set; }
        public bool? InStock { get; set; }

    }
}
