namespace ECommerce.Persistence.Model;

//Warehouse Entity
public class Warehouse
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public ICollection<Stock> Stocks { get; set; }
}
