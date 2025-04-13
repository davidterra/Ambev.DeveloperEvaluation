using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;

/// <summary>
/// Command for deleting a cart item.
/// </summary>
public record DeleteCartItemCommand : IRequest<DeleteCartItemResponse>
{
    /// <summary>
    /// The unique identifier of the cart item to delete.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartItemCommand"/> class.
    /// </summary>
    /// <param name="id">The ID of the cart item to delete.</param>
    public DeleteCartItemCommand(int id)
    {
        Id = id;
    }
}
