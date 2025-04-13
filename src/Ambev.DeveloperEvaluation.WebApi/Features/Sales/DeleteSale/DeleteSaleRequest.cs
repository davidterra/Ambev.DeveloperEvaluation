namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Represents a request model for deleting a sale.
/// This class encapsulates the data required to delete a sale from the system.
/// </summary>
public class DeleteSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to delete.
    /// This ID is used to locate and remove the sale from the database.
    /// </summary>
    public int Id { get; set; }
}
