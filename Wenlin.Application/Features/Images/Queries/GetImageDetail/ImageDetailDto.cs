using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Images.Queries.GetImageDetail;
public class ImageDetailDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public Guid OwnerId { get; set; } = default!;
}
