using Wenlin.Application.Features.Products.Queries.GetProductsList;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
public class GetCustomersListQueryResponse : BaseResponse
{
	public GetCustomersListQueryResponse() : base()
    {

	}
    public List<CustomerListDto> CustomerListDto { get; set; } = default!;
}
