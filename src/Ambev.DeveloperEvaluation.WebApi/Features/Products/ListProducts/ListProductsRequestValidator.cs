using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Validator for <see cref="ListProductsRequest"/>.
/// </summary>
public class ListProductsRequestValidator : AbstractValidator<ListProductsRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductsRequestValidator"/> class.
    /// Defines validation rules for <see cref="ListProductsRequest"/>.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <list type="bullet">
    /// <item><description>Page number must be greater than or equal to 1.</description></item>
    /// <item><description>Page size must be greater than 0.</description></item>
    /// <item><description>OrderBy must follow a valid format (e.g., "name asc, date desc").</description></item>
    /// </list>
    /// </remarks>
    public ListProductsRequestValidator()
    {
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
