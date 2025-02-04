using FluentValidation;
using DevEval.Application.Carts.Commands;

namespace DevEval.Application.Carts.Validators
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Cart ID must be greater than zero.");

            RuleFor(command => command.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");

            RuleFor(command => command.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(BeAValidDate).WithMessage("Date must be in a valid format (YYYY-MM-DD).");

            RuleFor(command => command.Products)
                .NotNull().WithMessage("Products list cannot be null.")
                .Must(products => products.Count > 0).WithMessage("At least one product must be included in the cart.");

            RuleForEach(command => command.Products)
                .SetValidator(new CartProductValidator());
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }
    }
}