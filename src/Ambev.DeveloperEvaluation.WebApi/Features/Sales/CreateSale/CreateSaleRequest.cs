namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system. This includes details about the cart being purchased
/// and the branch where the sale is taking place.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequest"/> class.
    /// </summary>
    public CreateSaleRequest()
    {
    }

    /// <summary>
    /// Gets or sets the identifier of the cart associated with the sale. This is required to link the sale
    /// to the specific items being purchased.
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the branch where the sale is being made. This ensures the sale is
    /// associated with the correct branch location.
    /// </summary>
    public int BranchId { get; set; }
}
