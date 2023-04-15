using Wenlin.SharedKernel.PropertyMapping;

namespace Wenlin.Application.Contracts.Infrastructure;
public interface IPropertyMappingService
{
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    KeyValuePair<bool, string> ValidMappingExistsFor<TSource, TDestination>(string fields);
}
