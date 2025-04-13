using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Validator for DeleteProductRequest.
/// Ensures that the request model for deleting a product meets the required validation rules.
/// </summary>
public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteProductRequest.
    /// Validates that the product Id is greater than zero.
    /// </summary>
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");
    }
}
