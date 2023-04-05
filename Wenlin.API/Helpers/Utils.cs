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

    internal static ProblemDetails GetProblemDetails(string errorMessage, string instance)
    {
        var problemDetails = new ProblemDetails();

        // add additional info not added by default
        problemDetails.Detail = errorMessage;
        problemDetails.Instance = instance;

        problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
        problemDetails.Status = StatusCodes.Status500InternalServerError;
        problemDetails.Title = "One or more errors occurred.";

        return problemDetails;
    }
}
