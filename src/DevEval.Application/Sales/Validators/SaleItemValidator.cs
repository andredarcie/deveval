using DevEval.Application.Sales.Dtos;
using FluentValidation;

namespace DevEval.Application.Sales.Validators
{
    public class SaleItemValidator : AbstractValidator<SaleItemDto>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            RuleFor(item => item.Discount)
                .InclusiveBetween(0, 1).WithMessage("Discount must be between 0 and 1.");
        }
    }
}
