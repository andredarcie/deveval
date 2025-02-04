using FluentValidation;
using DevEval.Application.Users.Commands;

namespace DevEval.Application.Users.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            // Validate the user ID
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");

            // Validate the email
            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            // Validate the username
            RuleFor(command => command.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            // Validate the password
            RuleFor(command => command.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[!\\@#\$%\^&\*\(\)_\+\-=\[\]\{\};':"",./<>?\|]")
                .WithMessage("Password must contain at least one special character.");

            // Validate the full name (NameDto)
            RuleFor(command => command.Name)
                .SetValidator(new NameValidator());

            // Validate the address (AddressDto)
            RuleFor(command => command.Address)
                .SetValidator(new AddressValidator());

            // Validate the phone number
            RuleFor(command => command.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid international format (e.g., +123456789).");

            // Validate the user status
            RuleFor(command => command.Status)
                .IsInEnum().WithMessage("Status must be a valid UserStatus value.");

            // Validate the user role
            RuleFor(command => command.Role)
                .IsInEnum().WithMessage("Role must be a valid UserRole value.");
        }
    }
}