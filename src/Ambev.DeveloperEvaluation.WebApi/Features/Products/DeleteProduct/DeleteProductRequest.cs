namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Represents the request model for deleting a product in the system.
/// </summary>
public class DeleteProductRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to delete.
    /// </summary>
    public int Id { get; set; }
}
