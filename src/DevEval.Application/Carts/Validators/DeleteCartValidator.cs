using DevEval.Application.Carts.Commands;
using FluentValidation;

namespace DevEval.Application.Carts.Validators
{
    public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Cart ID must be greater than zero.");
        }
    }
}