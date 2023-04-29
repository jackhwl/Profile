using MediatR;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerWithoutLinks;
public class GetCustomerWithoutLinksQuery : IRequest<GetCustomerWithoutLinksQueryResponse>
{
    public Guid Id { get; set; }
    public string? Fields { get; set; }
}