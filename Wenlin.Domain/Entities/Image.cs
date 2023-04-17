using System.ComponentModel.DataAnnotations;

namespace Wenlin.Domain.Entities;
public class Image
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public Guid OwnerId { get; set; } = default!;
}
