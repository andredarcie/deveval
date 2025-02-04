using FluentValidation;
using DevEval.Application.Products.Commands;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than zero.");

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Product title is required.")
            .MaximumLength(100).WithMessage("Product title must not exceed 100 characters.");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(command => command.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(command => !string.IsNullOrEmpty(command.Description));

        RuleFor(command => command.Category)
            .NotEmpty().WithMessage("Product category is required.")
            .MaximumLength(50).WithMessage("Product category must not exceed 50 characters.");

        RuleFor(command => command.Image)
            .Must(IsValidUrl).WithMessage("Image must be a valid URL.")
            .When(command => !string.IsNullOrEmpty(command.Image));

        RuleFor(command => command.Rating)
            .SetValidator(new RatingValidator())
            .When(command => command.Rating != null);
    }

    private bool IsValidUrl(string imageUrl)
    {
        return Uri.TryCreate(imageUrl, UriKind.Absolute, out _);
    }
}