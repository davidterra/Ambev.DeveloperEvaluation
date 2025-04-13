using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for <see cref="CreateProductRequest"/> that defines validation rules for product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductRequestValidator"/> class with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - <see cref="CreateProductRequest.Title"/>: Required, must not exceed 100 characters.
    /// - <see cref="CreateProductRequest.Price"/>: Must be greater than zero.
    /// - <see cref="CreateProductRequest.Description"/>: Required, must not exceed 500 characters.
    /// - <see cref="CreateProductRequest.Category"/>: Required, must not exceed 50 characters.
    /// - <see cref="CreateProductRequest.Image"/>: Required, must be a valid URL.
    /// - <see cref="CreateProductRequest.Rating"/>: Required, must be valid according to <see cref="CreateRatingRequestValidator"/>.
    /// </remarks>
    public CreateProductRequestValidator()
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
            .SetValidator(new CreateRatingRequestValidator());
    }

    /// <summary>
    /// Validator for <see cref="CreateRatingRequest"/> that defines validation rules for product ratings.
    /// </summary>
    public class CreateRatingRequestValidator : AbstractValidator<CreateRatingRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRatingRequestValidator"/> class with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - <see cref="CreateRatingRequest.Rate"/>: Must be between 1 and 5.
        /// - <see cref="CreateRatingRequest.Count"/>: Must be greater than zero.
        /// </remarks>
        public CreateRatingRequestValidator()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 5).WithMessage("Rate must be between 1 and 5.");

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than zero.");
        }
    }
}
