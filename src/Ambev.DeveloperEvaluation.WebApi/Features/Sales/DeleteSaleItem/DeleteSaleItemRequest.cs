namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;

/// <summary>
/// Request model for deleting a sale item.
/// This class is used to encapsulate the data required to delete a specific sale item from the system.
/// </summary>
public class DeleteSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the sale to which the item belongs.
    /// This ID is used to locate the sale in the database.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The unique identifier of the item to delete.
    /// This ID is used to locate and remove the specific item from the sale.
    /// </summary>
    public int ItemId { get; set; }
}
