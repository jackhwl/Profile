using System.ComponentModel.DataAnnotations;

namespace Wenlin.Domain.Entities;
public class Customer
{
    [Key]
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTimeOffset DateOfBirth { get; set; }

    public DateTimeOffset? DateOfDeath { get; set; }

    public string MainCategory { get; set; } = string.Empty;

    public ICollection<Image> Images { get; set; }
        = new List<Image>();

    //public Customer(string firstName, string lastName, string mainCategory)
    //{
    //    FirstName = firstName;
    //    LastName = lastName;
    //    MainCategory = mainCategory;
    //}
}
