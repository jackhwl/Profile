using System.ComponentModel.DataAnnotations;

namespace Wenlin.Client.ViewModels;

public class AddImageViewModel
{
    public List<IFormFile> Files { get; set; } = new List<IFormFile>();

    [Required]
    public string Title { get; set; }

    public AddImageViewModel(string title, List<IFormFile> files)
    {
        Title = title;
        Files = files;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AddImageViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }
}
