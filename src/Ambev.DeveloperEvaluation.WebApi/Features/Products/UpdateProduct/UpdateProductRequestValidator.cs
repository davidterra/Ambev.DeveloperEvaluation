using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductRequest that defines validation rules for updating a product.
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, must not exceed 100 characters.
    /// - Price: Must be greater than zero.
    /// - Description: Required, must not exceed 500 characters.
    /// - Category: Required, must not exceed 50 characters.
    /// - Image: Required, must be a valid URL.
    /// - Rating: Required, must be valid according to UpdateRatingRequestValidator.
    /// </remarks>
    public UpdateProductRequestValidator()
    {
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
            .SetValidator(new UpdateRatingRequestValidator());
    }

    /// <summary>
    /// Validator for UpdateRatingRequest that defines validation rules for product ratings.
    /// </summary>
    public class UpdateRatingRequestValidator : AbstractValidator<UpdateRatingRequest>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateRatingRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Rate: Must be between 1 and 5.
        /// - Count: Must be greater than zero.
        /// </remarks>
        public UpdateRatingRequestValidator()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 5).WithMessage("Rate must be between 1 and 5.");

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than zero.");
        }
    }
}
