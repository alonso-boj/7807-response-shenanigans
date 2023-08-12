using Company.Store.API.Models.Requests;
using FluentValidation;

namespace Company.Store.API.Validators;

public class RegisterCustomerRequestValidator : AbstractValidator<RegisterCustomerRequest>
{
    public RegisterCustomerRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");

        RuleFor(request => request.Age)
            .NotEmpty()
            .WithMessage("Age cannot be zero or empty");
    }
}
