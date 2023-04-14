using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Domain;
using Wenlin.Domain.Entities;
using Wenlin.SharedKernel.Pagination;
using Wenlin.SharedKernel.PropertyMapping;

namespace Wenlin.Persistence.Repositories;
public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly IPropertyMappingService _propertyMappingService;
    public CustomerRepository(WenlinContext dbContext, IPropertyMappingService propertyMappingService) : base(dbContext)
    {
        _propertyMappingService= propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
    }

    public async Task<PagedList<Customer>> GetCustomersAsync(CustomersResourceParameters customersResourceParameters)
    {
        if (customersResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(customersResourceParameters));
        }

        //if (string.IsNullOrWhiteSpace(customersResourceParameters.MainCategory) && string.IsNullOrWhiteSpace(customersResourceParameters.SearchQuery))
        //{
        //    return await ListAllAsync();
        //}

        // collection to start from
        var collection = _dbContext.Set<Customer>() as IQueryable<Customer>;

        if (!string.IsNullOrWhiteSpace(customersResourceParameters.MainCategory))
        {
            var mainCategory = customersResourceParameters.MainCategory.Trim();
            collection = collection.Where(a => a.MainCategory == mainCategory);
        }

        if (!string.IsNullOrWhiteSpace(customersResourceParameters.SearchQuery))
        {
            var searchQuery = customersResourceParameters.SearchQuery.Trim();
            collection = collection.Where(a => a.MainCategory.Contains(searchQuery) || a.FirstName.Contains(searchQuery) || a.LastName.Contains(searchQuery));
        }

        if (!string.IsNullOrWhiteSpace(customersResourceParameters.OrderBy))
        {
            // get property mapping dictionary
            var customerPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CustomerListDto, Customer>();

            collection = collection.ApplySort(customersResourceParameters.OrderBy, customerPropertyMappingDictionary);
        }

        var pageSize = customersResourceParameters.PageSize;
        var pageNumber = customersResourceParameters.PageNumber;

        return await PagedList<Customer>.CreateAsync(collection, pageNumber, pageSize);
    }
}
