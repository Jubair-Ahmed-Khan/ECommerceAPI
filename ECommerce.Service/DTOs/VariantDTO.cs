namespace ECommerce.Service.DTOs;

//Product Variant DTO
public class VariantDTO
{
    public string Color { get; set; }
    public string Size { get; set; }
    public List<WarehouseStockDTO> WarehouseStocks { get; set; }
}
