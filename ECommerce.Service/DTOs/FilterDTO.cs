namespace ECommerce.Service.DTOs;

//Product Filter DTO
public class FilterDTO
{
    public string ProductName { get; set; }
    public string WarehouseName { get; set; }
    public string VariantColor { get; set; }
    public string VariantSize { get; set; }
    public bool? InStock { get; set; }
}
