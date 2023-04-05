using Microsoft.AspNetCore.Mvc;

namespace Wenlin.API.Helpers;

internal static class Utils
{
    internal static ValidationProblemDetails GetValidationProblemDetails(IDictionary<string, string[]> errors, string instance)
    {
        var validationProblemDetails = new ValidationProblemDetails(errors);

        // add additional info not added by default
        validationProblemDetails.Detail = "See the errors field for details.";
        validationProblemDetails.Instance = instance;

        // report invalid model state responses as validation issues
        validationProblemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
        validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
        validationProblemDetails.Title = "One or more validation errors occurred.";

        return validationProblemDetails;
    }
}
