using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Store.API.Controllers;

public class StandardController : ControllerBase
{
    internal ValidationProblemDetails GetValidationProblemDetails(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        ValidationProblemDetails validationProblemDetails = new(ModelState)
        {
            Type = $"https://httpstatuses.com/{StatusCodes.Status422UnprocessableEntity}",
            Detail = "See the errors property for information about which fields are invalid",
            Instance = HttpContext.Request.Path,
            Status = StatusCodes.Status422UnprocessableEntity
        };

        return validationProblemDetails;
    }
}
