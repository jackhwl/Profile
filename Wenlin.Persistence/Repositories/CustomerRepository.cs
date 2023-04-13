﻿using Microsoft.EntityFrameworkCore;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Domain;
using Wenlin.Domain.Entities;

namespace Wenlin.Persistence.Repositories;
public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(WenlinContext dbContext) : base(dbContext)
    {

    }
    public async Task<IEnumerable<Customer>> GetCustomersAsync(CustomersResourceParameters customersResourceParameters)
    {
        if (customersResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(customersResourceParameters));
        }

        if (string.IsNullOrWhiteSpace(customersResourceParameters.MainCategory) && string.IsNullOrWhiteSpace(customersResourceParameters.SearchQuery))
        {
            return await ListAllAsync();
        }

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

        return await collection.ToListAsync();
    }
}