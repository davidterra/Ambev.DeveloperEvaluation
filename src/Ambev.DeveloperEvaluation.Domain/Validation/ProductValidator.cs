using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

        RuleFor(product => product.Price)
            .NotNull()
            .WithMessage("Price cannot be null.")
            .SetValidator(new MonetaryValueValidator());

        RuleFor(product => product.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty.")
            .MinimumLength(5).WithMessage("Description must be at least 10 characters long.")
            .MaximumLength(1000).WithMessage("Description cannot be longer than 500 characters.");

        RuleFor(product => product.Category)
            .NotEmpty()
            .WithMessage("Category cannot be empty.")
            .MaximumLength(100).WithMessage("Category cannot be longer than 100 characters.");

        RuleFor(product => product.Image)
            .NotEmpty()
            .WithMessage("Image cannot be empty.")
            .Must(image => Uri.IsWellFormedUriString(image, UriKind.Absolute))
            .WithMessage("Image must be a valid URL.")
            .MaximumLength(500).WithMessage("Image cannot be longer than 500 characters.");

        RuleFor(product => product.Rating)
            .NotNull()
            .WithMessage("Rating cannot be null.")
            .SetValidator(new RatingValueValidator());
    }
}

public class MonetaryValueValidator : AbstractValidator<MonetaryValue>
{
    public MonetaryValueValidator()
    {
        RuleFor(mv => mv.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");
    }
}

public class RatingValueValidator : AbstractValidator<RatingValue>
{
    public RatingValueValidator()
    {
        RuleFor(rv => rv.Rate)
            .InclusiveBetween(0, 5)
            .WithMessage("Rate must be between 0 and 5.");
        RuleFor(rv => rv.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Count must be greater than or equal to 0.");
    }
}