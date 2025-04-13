using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Represents a command to retrieve a cart by its unique identifier.
/// Implements the IRequest interface to return a <see cref="GetCartResult"/>.
/// </summary>
public record GetCartCommand : IRequest<GetCartResult>
{
    /// <summary>
    /// Gets the unique identifier of the cart to be retrieved.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartCommand"/> class with the specified cart ID.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    public GetCartCommand(int id)
    {
        Id = id;
    }
}
