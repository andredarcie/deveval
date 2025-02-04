using FluentValidation;
using DevEval.Application.Products.Commands;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than zero.");
    }
}