using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.DTOs
{
    public class ResponseDTO
    {
        public string ProductName { get; set; }
        public bool InStock { get; set; }
        public List<VariantDTO> Variants { get; set; }
    }
}
