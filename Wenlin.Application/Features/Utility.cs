using FluentValidation;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features;
internal static class Utility
{
    internal static async Task ValidateCommand<T>(T request, AbstractValidator<T> validator, BaseResponse response)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new Dictionary<string, IEnumerable<string>>();
            foreach (var error in validationResult.Errors)
            {
                if (response.ValidationErrors.ContainsKey(error.PropertyName))
                {
                    var errorMsgs = response.ValidationErrors[error.PropertyName].ToList();
                    errorMsgs.Add(error.ErrorMessage);
                    response.ValidationErrors[error.PropertyName] = errorMsgs;
                }
                else
                    response.ValidationErrors.Add(error.PropertyName, new List<string> { error.ErrorMessage });
            }
        }
    }
}
