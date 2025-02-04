using FluentValidation;
using DevEval.Application.Users.Dtos;

public class NameValidator : AbstractValidator<NameDto>
{
    public NameValidator()
    {
        RuleFor(name => name.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(name => name.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
    }
}