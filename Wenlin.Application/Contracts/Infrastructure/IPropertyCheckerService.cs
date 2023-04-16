namespace Wenlin.Application.Contracts.Infrastructure;
public interface IPropertyCheckerService
{
    KeyValuePair<bool, string> TypeHasProperties<T>(string? fields);
}
