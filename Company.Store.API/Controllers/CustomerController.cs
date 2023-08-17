using Company.Store.API.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.Store.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : StandardController
{
    private readonly IValidator<RegisterCustomerRequest> _validator;

    public CustomerController(IValidator<RegisterCustomerRequest> validator)
    {
        _validator = validator;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterCustomerAsync([FromBody] RegisterCustomerRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            ValidationProblemDetails validationProblemDetails = GetValidationProblemDetails(validationResult);

            return UnprocessableEntity(validationProblemDetails);
        }

        return Ok();
    }
}
