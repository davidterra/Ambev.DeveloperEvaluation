namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update an existing sale in the system, including the cart and branch details.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to be updated.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the branch where the sale is being updated.
    /// </summary>
    public int BranchId { get; set; }
}
