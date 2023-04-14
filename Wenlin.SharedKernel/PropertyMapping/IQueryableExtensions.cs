using System.Linq.Dynamic.Core;
namespace Wenlin.SharedKernel.PropertyMapping;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (mappingDictionary == null) throw new ArgumentNullException(nameof(mappingDictionary));

        if (string.IsNullOrWhiteSpace(orderBy)) return source;

        var orderByString = string.Empty;

        // the orderBy string is separated by ",", so we split it.
        var orderByAfterSplit = orderBy.Split(',');

        // apply each orderby clause
        foreach (var orderByClause in orderByAfterSplit)
        {
            var trimedOrderByClause = orderByClause.Trim();
            var orderDescending = trimedOrderByClause.EndsWith(" desc");

            var indexOfFirstSpace = trimedOrderByClause.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ? trimedOrderByClause : trimedOrderByClause.Remove(indexOfFirstSpace);

            // find the matching property
            if (!mappingDictionary.ContainsKey(propertyName))
            {
                throw new ArgumentException($"Key mapping for {propertyName} is missing.");
            }

            // get the PropertyMappingValue
            var propertyMappingValue = mappingDictionary[propertyName];

            if (propertyMappingValue == null) throw new ArgumentNullException(nameof(propertyMappingValue));

            // revert sort order if necessary
            if (propertyMappingValue.Revert) orderDescending = !orderDescending;

            // Run through the property names
            foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
            {
                orderByString = orderByString + (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ") + destinationProperty + (orderDescending ? " descending" : " ascending");
            }
        }

        return source.OrderBy(orderByString);
    }
}

