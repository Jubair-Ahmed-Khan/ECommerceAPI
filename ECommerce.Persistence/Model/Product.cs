using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SearchEngineFriendlyName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<Variant> Variants { get; set; }
    }
}
