using FluentValidation;
using DevEval.Application.Users.Dtos;

namespace DevEval.Application.Users.Validators
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

            RuleFor(address => address.ZipCode)
                .NotEmpty().WithMessage("Zip code is required.")
                .Matches(@"^\d{5}-\d{4}|\d{5}$").WithMessage("Zip code must be in a valid format (e.g., 12345 or 12345-6789).");

            RuleFor(address => address.Number)
                .GreaterThan(0).WithMessage("Number must be greater than zero.");
        }
    }
}
