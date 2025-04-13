using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Command for retrieving a product by its unique identifier.
/// </summary>
public record GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// Gets the unique identifier of the product to retrieve.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    public GetProductCommand(int id)
    {
        Id = id;
    }
}
