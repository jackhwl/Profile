using Wenlin.Application.Responses;
using Wenlin.SharedKernel.Pagination;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
public class GetCustomersListQueryResponse : BaseResponse
{
	public GetCustomersListQueryResponse() : base()
    {

	}
    public PagedList<CustomerListDto> CustomerListDto { get; set; } = default!;
}
