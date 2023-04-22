using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomer;
public class CreateCustomerCommandResponse : BaseResponse
{
	public CreateCustomerCommandResponse() : base()
	{

	}
    public CreateCustomerDto Customer { get; set; } = default!;
}
