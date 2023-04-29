using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
using Wenlin.Application.Helpers;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerWithLinks;
public class GetFullCustomerWithoutLinksQueryHandler : IRequestHandler<GetFullCustomerWithoutLinksQuery, GetFullCustomerWithoutLinksQueryResponse>
{
    private readonly IAsyncRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly IPropertyCheckerService _propertyCheckerService;

    public GetFullCustomerWithoutLinksQueryHandler(IAsyncRepository<Customer> customerRepository, IMapper mapper, IPropertyCheckerService propertyCheckerService)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _propertyCheckerService = propertyCheckerService;
    }

    public async Task<GetFullCustomerWithoutLinksQueryResponse> Handle(GetFullCustomerWithoutLinksQuery request, CancellationToken cancellationToken)
    {
        var getCustomerWithLinksQueryResponse = new GetFullCustomerWithoutLinksQueryResponse();

        var hasProperties = _propertyCheckerService.TypeHasProperties<CustomerFullDetailVm>(request.Fields);

        if (!hasProperties.Key)
        {
            getCustomerWithLinksQueryResponse.Success = false;
            getCustomerWithLinksQueryResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>
            {
                    { hasProperties.Value, new List<string> { "requested data shaping field doesn't exist" } }
                };

            return getCustomerWithLinksQueryResponse;
        }

        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            getCustomerWithLinksQueryResponse.Success = false;
            getCustomerWithLinksQueryResponse.NotFound = true;

            return getCustomerWithLinksQueryResponse;
        }

        getCustomerWithLinksQueryResponse.CustomerVm = _mapper.Map<CustomerFullDetailVm>(customer).ShapeData(request.Fields);

        return getCustomerWithLinksQueryResponse;
    }
}
