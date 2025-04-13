using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for updating a product.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must be greater than zero.
    /// - Title: Required, must not exceed 100 characters.
    /// - Price: Must be greater than zero.
    /// - Description: Required, must not exceed 500 characters.
    /// - Category: Required, must not exceed 50 characters.
    /// - Image: Required, must be a valid URL.
    /// - Rating: Required, must be valid according to UpdateRatingCommandValidator.
    /// </remarks>
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Image must be a valid URL.");

        RuleFor(x => x.Rating)
            .NotNull().WithMessage("Rating is required.")
            .SetValidator(new UpdateRatingCommandValidator());
    }

    /// <summary>
    /// Validator for UpdateRatingCommand that defines validation rules for product rating.
    /// </summary>
    public class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateRatingCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Rate: Must be between 0 and 5 (inclusive).
        /// - Count: Must be greater than or equal to zero.
        /// </remarks>
        public UpdateRatingCommandValidator()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0).WithMessage("Count must be greater than or equal to zero.");
        }
    }
}
