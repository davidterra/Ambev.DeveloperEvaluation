using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for the <see cref="GetSaleRequest"/> class. 
/// Ensures that the request contains valid data for retrieving a sale.
/// </summary>
public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleRequestValidator"/> class.
    /// Defines validation rules for the <see cref="GetSaleRequest"/> properties.
    /// </summary>
    public GetSaleRequestValidator()
    {
        // Validates that the Sale ID is greater than zero.
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Sale ID must be greater than zero.");
    }
}