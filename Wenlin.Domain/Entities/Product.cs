using System.ComponentModel.DataAnnotations;

namespace Wenlin.Domain.Entities;
public class Product
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Category Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
}
