using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;
using Wenlin.SharedKernel.Pagination;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
internal class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, GetCustomersListQueryResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IPropertyMappingService _propertyMappingService;

    public GetCustomersListQueryHandler(ICustomerRepository customerRepository, IMapper mapper, IPropertyMappingService propertyMappingService)
    {
        _customerRepository = customerRepository ??
            throw new ArgumentNullException(nameof(customerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
    }

    public async Task<GetCustomersListQueryResponse> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        var getCustomersListQueryResponse = new GetCustomersListQueryResponse();

        var validMapping = _propertyMappingService.ValidMappingExistsFor<CustomerListDto, Customer>(request.CustomersResourceParameters.OrderBy);

        if (!validMapping.Key)
        {
            getCustomersListQueryResponse.Success = false;
            getCustomersListQueryResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>
            {
                { validMapping.Value, new List<string> { "mapping is missing" } }
            };

            return getCustomersListQueryResponse;
        }

        var customers = await _customerRepository.GetCustomersAsync(request.CustomersResourceParameters);

        getCustomersListQueryResponse.CustomerListDto = new PagedList<CustomerListDto>(_mapper.Map<List<CustomerListDto>>(customers), customers.TotalCount, customers.CurrentPage, customers.PageSize) ;

        return getCustomersListQueryResponse;
    }
}
