using FluentValidation;
using DevEval.Application.Carts.Queries;

namespace DevEval.Application.Carts.Validators
{
    public class GetCartByIdValidator : AbstractValidator<GetCartByIdQuery>
    {
        public GetCartByIdValidator()
        {
            RuleFor(query => query.Id)
                .GreaterThan(0).WithMessage("Cart ID must be greater than zero.");
        }
    }
}