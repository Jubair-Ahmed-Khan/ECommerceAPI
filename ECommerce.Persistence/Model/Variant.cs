using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Model
{
    public class Variant
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        
    }
}
