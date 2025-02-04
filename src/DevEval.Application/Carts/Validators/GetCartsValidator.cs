using DevEval.Application.Carts.Queries;
using FluentValidation;

namespace DevEval.Application.Carts.Validators
{
    public class GetCartsValidator : AbstractValidator<GetCartsQuery>
    {
        public GetCartsValidator()
        {
            RuleFor(query => query.Parameters.Page)
                .GreaterThan(0).WithMessage("Page number must be greater than zero.");

            RuleFor(query => query.Parameters.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than zero.");
        }
    }
}