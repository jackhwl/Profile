using Wenlin.Application.Features.Products.Queries.GetProductsList;
using Wenlin.Application.Helpers;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
public class GetCustomersListQueryResponse : BaseResponse
{
	public GetCustomersListQueryResponse() : base()
    {

	}
    public PagedList<CustomerListDto> CustomerListDto { get; set; } = default!;
}
