using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Queries.GetProductDetail;

public class ProductDetailVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryDto Category { get; set; } = default!;
}