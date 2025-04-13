using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Validator for <see cref="GetSaleCommand"/> that defines validation rules for retrieving a sale.
/// Ensures that the required fields are properly populated and meet the expected constraints.
/// </summary>
public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleCommandValidator"/> class.
    /// Defines validation rules for the <see cref="GetSaleCommand"/> properties.
    /// </summary>
    public GetSaleCommandValidator()
    {
        // Validates that Id is greater than zero.
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Cart ID must be greater than zero.");
    }
}
