using System.Reflection;
using Wenlin.Application.Contracts.Infrastructure;

namespace Wenlin.Infrastructure.PropertyMapping;
public class PropertyCheckerService : IPropertyCheckerService
{
    public KeyValuePair<bool, string> TypeHasProperties<T>(string? fields)
    {
        if (string.IsNullOrWhiteSpace(fields))
        {
            return new KeyValuePair<bool, string>(true, "");
        }

        // the field are separated by ",", so we split it
        var fieldsAfterSplit = fields.Split(',');

        // check if the requested fields exist on source
        foreach (var field in fieldsAfterSplit)
        {
            // trim each field, as it might contain leading
            // or trailing spaces. Can't trim the var in foreach,
            // so use another var.
            var propertyName = field.Trim();

            // use reflection to check if the property can be
            // found on T.
            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // it can't be found, return false
            if (propertyInfo == null)
            {
                return new KeyValuePair<bool, string>(false, propertyName);
            }
        }

        // all checks out, return true
        return new KeyValuePair<bool, string>(true, "");
    }
}
