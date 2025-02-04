using FluentValidation;
using DevEval.Application.Products.Queries;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(query => query.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than zero.");
    }
}