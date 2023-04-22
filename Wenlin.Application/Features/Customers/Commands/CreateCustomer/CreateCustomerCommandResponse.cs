using System.Dynamic;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomer;
public class CreateCustomerCommandResponse : BaseResponse
{
	public CreateCustomerCommandResponse() : base()
	{

	}
    public ExpandoObject CreateCustomerExpandoObject { get; set; } = default!;
}
