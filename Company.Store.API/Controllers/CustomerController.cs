﻿using Company.Store.API.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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