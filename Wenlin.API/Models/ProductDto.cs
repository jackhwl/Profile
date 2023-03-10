namespace Wenlin.API.Models;

public class ProductDto
{
    public int ProductId { get; set; }
    public Guid ProductGuid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
