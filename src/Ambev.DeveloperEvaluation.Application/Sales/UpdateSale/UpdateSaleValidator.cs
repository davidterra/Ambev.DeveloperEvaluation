using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for updating a sale.
/// Ensures that the required fields are properly populated and meet the expected constraints.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleCommandValidator"/> class.
    /// Defines validation rules for the <see cref="UpdateSaleCommand"/> properties.
    /// </summary>
    public UpdateSaleCommandValidator()
    {
        // Validates that Id is greater than zero.
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Sale ID must be greater than zero.");

        // Validates that BranchId is greater than zero.
        RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .WithMessage("Branch ID must be greater than zero.");
    }
}
