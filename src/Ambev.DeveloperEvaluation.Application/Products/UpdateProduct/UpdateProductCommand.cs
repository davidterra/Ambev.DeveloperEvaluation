using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Command for updating an existing product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a product, 
/// including title, price, description, category, image URL, and rating. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be updated.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating details of the product.
    /// </summary>
    public UpdateRatingCommand Rating { get; set; } = new();

    /// <summary>
    /// Validates the current command instance using the <see cref="UpdateProductCommandValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing the validation results, 
    /// including whether the command is valid and any validation errors.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the rating details of a product.
/// </summary>
public record class UpdateRatingCommand
{
    /// <summary>
    /// Gets or sets the average rating value of the product.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the total number of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}
