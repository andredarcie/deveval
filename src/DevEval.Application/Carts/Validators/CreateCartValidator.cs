using FluentValidation;
using DevEval.Application.Carts.Commands;

namespace DevEval.Application.Carts.Validators
{
    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(command => command.Products)
                .NotNull().WithMessage("Products list cannot be null.")
                .Must(products => products.Count > 0).WithMessage("At least one product must be included in the cart.");

            RuleForEach(command => command.Products)
                .SetValidator(new CartProductValidator());
        }
    }
}