using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for the <see cref="CreateSaleRequest"/> class. 
/// Ensures that the request contains valid data for creating a sale by applying specific validation rules.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequestValidator"/> class.
    /// Defines validation rules to ensure that the <see cref="CreateSaleRequest"/> properties meet the required criteria.
    /// </summary>
    public CreateSaleRequestValidator()
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

