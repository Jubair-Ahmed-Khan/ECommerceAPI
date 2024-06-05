namespace ECommerce.Service.DTOs;

//Product Response DTO
public class ResponseDTO
{
    public string ProductName { get; set; }
    public bool InStock { get; set; }
    public List<VariantDTO> Variants { get; set; }
}
