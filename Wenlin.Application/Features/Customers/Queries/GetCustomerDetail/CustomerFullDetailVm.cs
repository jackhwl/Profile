namespace Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
public class CustomerFullDetailVm : ICustomerDetail
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string MainCategory { get; set; } = string.Empty;
}
