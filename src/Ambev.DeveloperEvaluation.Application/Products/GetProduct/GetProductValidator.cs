using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Validator for GetProductCommand
/// </summary>
public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductCommand
    /// </summary>
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than zero");
    }
}
