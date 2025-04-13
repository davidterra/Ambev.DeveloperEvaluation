using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Represents a command to delete a sale.
/// This command encapsulates the data required to perform a sale deletion operation.
/// </summary>
public record DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    /// <summary>
    /// Gets the unique identifier of the sale to delete.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete.</param>
    public DeleteSaleCommand(int id)
    {
        Id = id;
    }
}
