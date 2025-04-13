using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents a command for creating a new sale in the system.
/// This command encapsulates the necessary data required to initiate the creation of a sale, 
/// including the cart and branch identifiers. It is processed by a handler to generate a 
/// <see cref="CreateSaleResult"/>.
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommand"/> class.
    /// </summary>
    /// <param name="cartId">The identifier of the cart associated with the sale.</param>
    /// <param name="branchId">The identifier of the branch where the sale is made.</param>
    public CreateSaleCommand(int cartId, int branchId)
    {
        CartId = cartId;
        BranchId = branchId;
    }

    /// <summary>
    /// Gets or sets the identifier of the cart associated with the sale.
    /// This value is required to link the sale to a specific cart in the system.
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the branch where the sale is made.
    /// This value is required to associate the sale with a specific branch.
    /// </summary>
    public int BranchId { get; set; }
}
