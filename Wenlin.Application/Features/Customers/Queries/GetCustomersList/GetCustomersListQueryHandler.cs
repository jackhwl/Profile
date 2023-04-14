using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.SharedKernel.Pagination;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
internal class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, GetCustomersListQueryResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomersListQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomersListQueryResponse> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        var getCustomersListQueryResponse = new GetCustomersListQueryResponse();

        var customers = await _customerRepository.GetCustomersAsync(request.CustomersResourceParameters);

        getCustomersListQueryResponse.CustomerListDto = new PagedList<CustomerListDto>(_mapper.Map<List<CustomerListDto>>(customers), customers.TotalCount, customers.CurrentPage, customers.PageSize) ;

        return getCustomersListQueryResponse;
    }
}
