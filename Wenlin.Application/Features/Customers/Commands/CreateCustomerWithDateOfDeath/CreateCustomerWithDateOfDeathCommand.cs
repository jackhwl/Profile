using MediatR;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomerWithDateOfDeath;
public class CreateCustomerWithDateOfDeathCommand : IRequest<CreateCustomerWithDateOfDeathCommandResponse>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public DateTimeOffset? DateOfDeath { get; set; }
    public string MainCategory { get; set; } = string.Empty;
    public string? Fields { get; set; }
}
