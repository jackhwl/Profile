using MediatR;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
public class GetCustomersListQuery : IRequest<GetCustomersListQueryResponse>
{
    public CustomersResourceParameters CustomersResourceParameters { get; set; } = default!;
}
