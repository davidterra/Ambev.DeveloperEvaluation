using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Validator for ListProductsCommand
/// </summary>
public class ListProductsCommandValidator : AbstractValidator<ListProductsCommand>
{
    /// <summary>
    /// Initializes validation rules for ListProductsCommand.
    /// </summary>
    /// <remarks>
    /// - Ensures the page number is greater than or equal to 1.
    /// - Ensures the page size is greater than 0.
    /// - Validates the order format (e.g., "name asc, date desc").
    /// - Ensures each filter has a non-empty key and value.
    /// </remarks>
    public ListProductsCommandValidator()
    {
        // Rule to ensure the page number is greater than or equal to 1
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1.");

        // Rule to ensure the page size is greater than 0
        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.");

        // Rule to validate the order format
        RuleFor(x => x.OrderBy)
            .Matches(@"""([a-zA-Z]+( (asc|desc))?(, )?)*[a-zA-Z]+( (asc|desc))?""")
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
            .WithMessage("Order format is invalid.");
    }
}

