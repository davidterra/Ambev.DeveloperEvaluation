using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

/// <summary>
/// Command for deleting a sale item.
/// This command encapsulates the data required to perform a sale item deletion operation.
/// </summary>
public record DeleteSaleItemCommand : IRequest<DeleteSaleItemResponse>
{
    /// <summary>
    /// Gets the unique identifier of the sale to delete an item from.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the unique identifier of the item to delete.
    /// </summary>
    public int ItemId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete an item from.</param>
    /// <param name="itemId">The unique identifier of the item to delete.</param>
    public DeleteSaleItemCommand(int id, int itemId)
    {
        Id = id;
        ItemId = itemId;
    }
}
