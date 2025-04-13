namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Represents the request model for retrieving a product by its unique identifier.
/// </summary>
public class GetProductRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to retrieve.
    /// </summary>
    public int Id { get; set; }
}
