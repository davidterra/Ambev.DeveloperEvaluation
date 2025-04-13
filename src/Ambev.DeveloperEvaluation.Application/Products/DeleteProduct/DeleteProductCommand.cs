using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command for deleting a product.
/// This command encapsulates the data required to delete a product, 
/// including the unique identifier of the product to be removed.
/// </summary>
public record DeleteProductCommand : IRequest<DeleteProductResponse>
{
    /// <summary>
    /// Gets the unique identifier of the product to delete.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}
