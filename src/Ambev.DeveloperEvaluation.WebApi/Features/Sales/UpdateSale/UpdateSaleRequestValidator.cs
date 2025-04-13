using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for the <see cref="UpdateSaleRequest"/> class. 
/// Ensures that the request contains valid data for updating a sale.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleRequestValidator"/> class.
    /// Defines validation rules for the <see cref="UpdateSaleRequest"/> properties to ensure data integrity.
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        // Validates that the sale ID is greater than zero.
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Sale ID must be greater than zero.");

        // Validates that the branch ID is greater than zero.
        RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .WithMessage("Branch ID must be greater than zero.");
    }
}

