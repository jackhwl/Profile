using MediatR;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerWithLinks;
public class GetFullCustomerWithoutLinksQuery : IRequest<GetFullCustomerWithoutLinksQueryResponse>
{
    public Guid Id { get; set; }
    public string? Fields { get; set; }
}
