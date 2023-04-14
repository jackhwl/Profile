﻿using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Domain.Entities;
using Wenlin.SharedKernel.PropertyMapping;

namespace Wenlin.Infrastructure.PropertyMapping;
public class PropertyMappingService : IPropertyMappingService
{
    private readonly Dictionary<string, PropertyMappingValue> _customerPropertyMapping = new(StringComparer.OrdinalIgnoreCase)
    {
        {"Id", new(new[] { "Id" }) },
        {"MainCategory", new(new[] { "MainCategory" }) },
        {"Age", new(new[] { "DateOfBirth" }, true) },
        {"Name", new(new[] { "FirstName", "LastName" }) }
    };

    private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<CustomerListDto, Customer>(_customerPropertyMapping));
    }
    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
        // get matching mapping
        var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

        if (matchingMapping.Count() == 1)
        {
            return matchingMapping.First().MappingDictionary;
        }

        throw new Exception($"Cannot find exact property mapping instance " + $"for <{typeof(TSource)}, {typeof(TDestination)}");
    }
}
