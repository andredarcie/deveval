﻿using FluentValidation;
using DevEval.Application.Products.Queries;

public class GetProductsByCategoryValidator : AbstractValidator<GetProductsByCategoryQuery>
{
    public GetProductsByCategoryValidator()
    {
        RuleFor(query => query.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

        RuleFor(query => query.Parameters.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(query => query.Parameters.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.");
    }
}