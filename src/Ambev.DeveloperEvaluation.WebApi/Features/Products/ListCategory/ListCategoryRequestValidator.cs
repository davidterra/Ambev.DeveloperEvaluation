using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategory;

/// <summary>
/// Validator for the <see cref="ListCategoryRequest"/> class.
/// This class defines validation rules for the properties of the ListCategoryRequest object:
/// - Ensures the Category is not empty and does not exceed 100 characters.
/// - Ensures the Page number is greater than or equal to 1.
/// - Ensures the Size is greater than 0.
/// - Validates the format of the OrderBy string (e.g., "name asc, date desc") if provided.
/// </summary>
public class ListCategoryRequestValidator : AbstractValidator<ListCategoryRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoryRequestValidator"/> class.
    /// </summary>
    public ListCategoryRequestValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required.")
            .MaximumLength(100)
            .WithMessage("Category must not exceed 100 characters.");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.");

        RuleFor(x => x.OrderBy)
             .Matches(@"""([a-zA-Z]+( (asc|desc))?(, )?)*[a-zA-Z]+( (asc|desc))?""")
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
            .WithMessage("Order format is invalid.");
    }
}
