namespace Wenlin.Client.ViewModels;

public class ImageDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public Guid OwnerId { get; set; } = default!;
}
