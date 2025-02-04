using FluentValidation;
using DevEval.Application.Users.Queries;

namespace DevEval.Application.Users.Validators
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(query => query.Id)
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }
}