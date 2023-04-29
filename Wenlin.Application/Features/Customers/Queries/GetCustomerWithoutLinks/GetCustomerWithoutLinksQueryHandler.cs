using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
using Wenlin.Application.Helpers;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerWithoutLinks;
public class GetCustomerWithoutLinksQueryHandler : IRequestHandler<GetCustomerWithoutLinksQuery, GetCustomerWithoutLinksQueryResponse>
{
    private readonly IAsyncRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly IPropertyCheckerService _propertyCheckerService;

    public GetCustomerWithoutLinksQueryHandler(IAsyncRepository<Customer> customerRepository, IMapper mapper, IPropertyCheckerService propertyCheckerService)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _propertyCheckerService = propertyCheckerService;
    }

    public async Task<GetCustomerWithoutLinksQueryResponse> Handle(GetCustomerWithoutLinksQuery request, CancellationToken cancellationToken)
    {
        var getCustomerWithoutLinksQueryResponse = new GetCustomerWithoutLinksQueryResponse();

        var hasProperties = _propertyCheckerService.TypeHasProperties<CustomerDetailVm>(request.Fields);

        if (!hasProperties.Key)
        {
            getCustomerWithoutLinksQueryResponse.Success = false;
            getCustomerWithoutLinksQueryResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>
            {
                { hasProperties.Value, new List<string> { "requested data shaping field doesn't exist" } }
            };

            return getCustomerWithoutLinksQueryResponse;
        }

        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            getCustomerWithoutLinksQueryResponse.Success = false;
            getCustomerWithoutLinksQueryResponse.NotFound = true;

            return getCustomerWithoutLinksQueryResponse;
        }

        getCustomerWithoutLinksQueryResponse.CustomerVm = _mapper.Map<CustomerDetailVm>(customer).ShapeData(request.Fields);

        return getCustomerWithoutLinksQueryResponse;
    }
}
