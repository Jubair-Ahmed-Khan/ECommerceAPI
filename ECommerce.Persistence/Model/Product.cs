namespace ECommerce.Persistence.Model;

//Product Entity
public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string SearchEngineFriendlyName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public ICollection<Variant> Variants { get; set; }
}
