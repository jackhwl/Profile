namespace Wenlin.Client.ViewModels;

public class GalleryIndexViewModel
{
    public IEnumerable<ImageDto> Images { get; private set; }
            = new List<ImageDto>();

    public GalleryIndexViewModel(IEnumerable<ImageDto> images)
    {
        Images = images;
    }
}
