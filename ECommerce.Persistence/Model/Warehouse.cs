using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Model
{
    public class Warehouse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
