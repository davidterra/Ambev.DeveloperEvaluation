namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Represents a request to retrieve details of a specific sale in the system.
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleRequest"/> class.
    /// </summary>
    public GetSaleRequest()
    {
    }

    /// <summary>
    /// Gets or sets the unique identifier of the sale to be retrieved.
    /// </summary>
    public int Id { get; set; }
}
