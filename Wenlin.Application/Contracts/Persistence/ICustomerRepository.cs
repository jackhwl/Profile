using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Contracts.Persistence;
public interface ICustomerRepository : IAsyncRepository<Customer>
{
    Task<IEnumerable<Customer>> GetCustomersAsync(CustomersResourceParameters customersResourceParameters);
}
