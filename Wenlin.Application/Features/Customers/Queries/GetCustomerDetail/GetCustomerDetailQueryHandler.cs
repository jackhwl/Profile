using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Helpers;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
internal class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, GetCustomerDetailQueryResponse>
{
    private readonly IAsyncRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly IPropertyCheckerService _propertyCheckerService;

    public GetCustomerDetailQueryHandler(IAsyncRepository<Customer> customerRepository, IMapper mapper, IPropertyCheckerService propertyCheckerService)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _propertyCheckerService = propertyCheckerService;
    }

    public async Task<GetCustomerDetailQueryResponse> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        var getCustomerDetailQueryResponse = new GetCustomerDetailQueryResponse();

        // check if the inputted media type is a valid media type
        if (request.MediaType == null)
        {
            getCustomerDetailQueryResponse.Success = false;
            getCustomerDetailQueryResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>
            {
                { "MediaType", new List<string> { "Accept header media type value is not a valid media type." } }
            };

            return getCustomerDetailQueryResponse;
        }

        var isCustomerFullDetail = request.MediaType == "vnd.wenlin.customer.full";

        var hasProperties = isCustomerFullDetail ? _propertyCheckerService.TypeHasProperties<CustomerFullDetailVm>(request.Fields)  : _propertyCheckerService.TypeHasProperties<CustomerDetailVm> (request.Fields);

        if (!hasProperties.Key)
        {
            getCustomerDetailQueryResponse.Success = false;
            getCustomerDetailQueryResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>
            {
                { hasProperties.Value, new List<string> { "requested data shaping field doesn't exist" } }
            };

            return getCustomerDetailQueryResponse;
        }

        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            getCustomerDetailQueryResponse.Success = false;
            getCustomerDetailQueryResponse.NotFound = true;

            return getCustomerDetailQueryResponse;
        }

        object customerVm;
        if (isCustomerFullDetail)
            customerVm = request.IncludeLink ? _mapper.Map<CustomerFullDetailVm>(customer).ShapeData(request.Fields) : _mapper.Map<CustomerFullDetailVm>(customer);
        else
            customerVm = request.IncludeLink ? _mapper.Map<CustomerDetailVm>(customer).ShapeData(request.Fields) : _mapper.Map<CustomerDetailVm>(customer);

        getCustomerDetailQueryResponse.CustomerVm = customerVm;

        return getCustomerDetailQueryResponse;
    }
}
