using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Domain.Entities;
using Wenlin.SharedKernel.Pagination;

namespace Wenlin.Application.Contracts.Persistence;
public interface ICustomerRepository : IAsyncRepository<Customer>
{
    Task<PagedList<Customer>> GetCustomersAsync(CustomersResourceParameters customersResourceParameters);
}
