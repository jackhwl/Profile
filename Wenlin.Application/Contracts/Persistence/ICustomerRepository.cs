using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Application.Helpers;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Contracts.Persistence;
public interface ICustomerRepository : IAsyncRepository<Customer>
{
    Task<PagedList<Customer>> GetCustomersAsync(CustomersResourceParameters customersResourceParameters);
}
