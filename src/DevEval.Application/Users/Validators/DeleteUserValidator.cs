using DevEval.Application.Users.Commands;
using FluentValidation;

namespace DevEval.Application.Users.Validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }
}