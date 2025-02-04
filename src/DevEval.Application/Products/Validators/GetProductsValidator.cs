using FluentValidation;
using DevEval.Application.Products.Queries;

public class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsValidator()
    {
        RuleFor(query => query.Parameters.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(query => query.Parameters.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.");
    }
}