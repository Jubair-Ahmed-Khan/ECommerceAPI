using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.DTOs
{
    public class FilterDTO
    {

        public string ProductName { get; set; }
        public string WarehouseName { get; set; }
        public string VariantColor { get; set; }
        public string VariantSize { get; set; }
        public bool? InStock { get; set; }

    }
}
