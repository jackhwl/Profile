namespace Wenlin.Domain.Entities;
public class Image
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;
    public Customer Customer { get; set; } = null!;
    public Guid OwnerId { get; set; }

}
