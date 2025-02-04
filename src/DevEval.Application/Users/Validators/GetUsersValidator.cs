using DevEval.Application.Users.Queries;
using FluentValidation;

namespace DevEval.Application.Users.Validators
{
    public class GetUsersValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersValidator()
        {
            RuleFor(query => query.Parameters.Page)
                .GreaterThan(0).WithMessage("Page number must be greater than zero.");

            RuleFor(query => query.Parameters.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than zero.");
        }
    }
}