using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Helpers;
using Wenlin.Domain.Entities;
using Wenlin.SharedKernel.Pagination;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
internal class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, GetCustomersListQueryResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IPropertyMappingService _propertyMappingService;
    private readonly IPropertyCheckerService _propertyCheckerService;

    public GetCustomersListQueryHandler(ICustomerRepository customerRepository, IMapper mapper, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService)
    {
        _customerRepository = customerRepository ??
            throw new ArgumentNullException(nameof(customerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        _propertyCheckerService = propertyCheckerService ?? throw new ArgumentNullException(nameof(propertyCheckerService));
    }

    public async Task<GetCustomersListQueryResponse> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        var getCustomersListQueryResponse = new GetCustomersListQueryResponse();

        var validMapping = _propertyMappingService.ValidMappingExistsFor<CustomerListDto, Customer>(request.CustomersResourceParameters.OrderBy);

        var hasProperties = _propertyCheckerService.TypeHasProperties<CustomerListDto>(request.CustomersResourceParameters.Fields);

        if (!validMapping.Key || !hasProperties.Key)
        {
            getCustomersListQueryResponse.Success = false;
            getCustomersListQueryResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>();

            if (!validMapping.Key)
            {
                getCustomersListQueryResponse.ValidationErrors.Add(validMapping.Value, new List<string> { "mapping is missing" });
            }

            if (!hasProperties.Key)
            {
                getCustomersListQueryResponse.ValidationErrors.Add(hasProperties.Value, new List<string> { "requested data shaping field doesn't exist" });
            }

            return getCustomersListQueryResponse;
        }

        var customers = await _customerRepository.GetCustomersAsync(request.CustomersResourceParameters);

        var customerLists = new PagedList<CustomerListDto>(_mapper.Map<List<CustomerListDto>>(customers), customers.TotalCount, customers.CurrentPage, customers.PageSize);

        getCustomersListQueryResponse.CustomerListDto = customerLists;
        getCustomersListQueryResponse.CustomerExpandoListDto = _mapper.Map<IEnumerable<CustomerListDto>>(customers).ShapeData(request.CustomersResourceParameters.Fields);

        return getCustomersListQueryResponse;
    }
}
