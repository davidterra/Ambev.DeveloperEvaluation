using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for creating a sale.
/// Ensures that the required fields are properly populated and meet the expected constraints.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> class.
    /// Defines validation rules for the <see cref="CreateSaleCommand"/> properties.
    /// </summary>
    public CreateSaleCommandValidator()
    {
        // Validates that CartId is greater than zero.
        RuleFor(x => x.CartId)
            .GreaterThan(0)
            .WithMessage("Cart ID must be greater than zero.");

        // Validates that BranchId is greater than zero.
        RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .WithMessage("Branch ID must be greater than zero.");
    }
}
