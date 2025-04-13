using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

/// <summary>
/// Validator for the <see cref="ListCartsCommand"/> class.
/// Ensures that the pagination, sorting, and filtering parameters are valid.
/// </summary>
public class ListCartsCommandValidator : AbstractValidator<ListCartsCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListCartsCommandValidator"/> class.
    /// Defines validation rules for the <see cref="ListCartsCommand"/> properties.
    /// </summary>
    public ListCartsCommandValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(x => x.Size)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.");

        RuleFor(x => x.OrderBy)
            .Matches(@"""([a-zA-Z]+( (asc|desc))?(, )?)*[a-zA-Z]+( (asc|desc))?""")
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
            .WithMessage("Order format is invalid.");
    }
}

