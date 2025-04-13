using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;

/// <summary>
/// Validator for the <see cref="ListCartsRequest"/> class, ensuring that pagination, sorting, and filtering parameters are valid.
/// </summary>
public class ListCartsRequestValidator : AbstractValidator<ListCartsRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListCartsRequestValidator"/> class.
    /// Defines validation rules for the <see cref="ListCartsRequest"/> properties.
    /// </summary>
    public ListCartsRequestValidator()
    {
        // Validates that the page number is greater than or equal to 1.
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1.");

        // Validates that the page size is greater than 0.
        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.");

        // Validates the format of the OrderBy string if it is not null or empty.
        RuleFor(x => x.OrderBy)
            .Matches(@"""([a-zA-Z]+( (asc|desc))?(, )?)*[a-zA-Z]+( (asc|desc))?""")
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
            .WithMessage("Order format is invalid.");
    }
}
