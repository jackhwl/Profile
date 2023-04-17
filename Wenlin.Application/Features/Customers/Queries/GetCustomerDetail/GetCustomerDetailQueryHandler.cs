﻿using AutoMapper;
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

        var hasProperties = _propertyCheckerService.TypeHasProperties<CustomerDetailVm>(request.Fields);

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
        var customerVm = _mapper.Map<CustomerDetailVm>(customer);

        if (customer == null)
        {
            getCustomerDetailQueryResponse.Success = false;
            getCustomerDetailQueryResponse.NotFound = true;

            return getCustomerDetailQueryResponse;
        }

        getCustomerDetailQueryResponse.CustomerExpandoDetailVm = customerVm.ShapeData(request.Fields);

        return getCustomerDetailQueryResponse;
    }
}
