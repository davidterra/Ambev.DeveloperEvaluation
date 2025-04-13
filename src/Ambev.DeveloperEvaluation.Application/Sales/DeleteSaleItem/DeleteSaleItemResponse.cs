namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

/// <summary>
/// Represents the response model for the DeleteSaleItem operation, 
/// indicating the outcome of the sale item deletion process.
/// </summary>
public class DeleteSaleItemResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemResponse"/> class.
    /// </summary>
    public DeleteSaleItemResponse() { }

    /// <summary>
    /// Gets or sets a value indicating whether the deletion of the sale item was successful.
    /// </summary>
    public bool Success { get; set; }
}
