using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Validator for <see cref="GetProductRequest"/>.
/// Ensures that the request contains valid data for retrieving a product.
/// </summary>
public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductRequestValidator"/> class.
    /// Defines validation rules for the <see cref="GetProductRequest"/> model.
    /// </summary>
    public GetProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
