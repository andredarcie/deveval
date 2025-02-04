using DevEval.Application.Products.Dtos;
using FluentValidation;

public class RatingValidator : AbstractValidator<RatingDto?>
{
    public RatingValidator()
    {
        RuleFor(x => x!.Rate)
            .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");

        RuleFor(x => x!.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Rating count must be zero or greater.");
    }
}