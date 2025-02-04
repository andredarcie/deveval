using FluentValidation;
using DevEval.Application.Products.Commands;

namespace DevEval.Application.Products.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Product title is required.")
                .MaximumLength(100).WithMessage("Product title must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Product category is required.")
                .MaximumLength(50).WithMessage("Product category must not exceed 50 characters.");

            RuleFor(x => x.Image)
                .Must(IsValidUrl).WithMessage("Image must be a valid URL.")
                .When(x => !string.IsNullOrEmpty(x.Image));

            RuleFor(x => x.Rating)
                .SetValidator(new RatingValidator())
                .When(x => x.Rating != null);
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}