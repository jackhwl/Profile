using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Images.Commands.CreateImage;
public class CreateImageDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;
    public Customer Customer { get; set; } = null!;
    public Guid OwnerId { get; set; }
}
