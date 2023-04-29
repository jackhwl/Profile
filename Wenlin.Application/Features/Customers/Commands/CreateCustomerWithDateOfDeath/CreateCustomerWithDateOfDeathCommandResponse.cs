using System.Dynamic;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomerWithDateOfDeath;
public class CreateCustomerWithDateOfDeathCommandResponse : BaseResponse
{
	public CreateCustomerWithDateOfDeathCommandResponse() : base()
	{

	}
	public ExpandoObject CreateCustomerWithDateOfDeathExpandoObject { get; set; } = default!;
}