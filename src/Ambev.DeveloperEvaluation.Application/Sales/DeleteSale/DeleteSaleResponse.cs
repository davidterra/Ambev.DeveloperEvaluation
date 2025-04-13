namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Represents the response model for the DeleteSale operation, indicating the outcome of the sale deletion process.
/// </summary>
public class DeleteSaleResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleResponse"/> class.
    /// </summary>
    public DeleteSaleResponse()
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the sale deletion was successful.
    /// </summary>
    public bool Success { get; set; }
}
