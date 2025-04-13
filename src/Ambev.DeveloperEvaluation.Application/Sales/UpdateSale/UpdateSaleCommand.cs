using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating an existing sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a sale, 
/// including the sale identifier and branch identifier. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateSaleCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to be updated.
    /// </summary>
    /// <value>An integer representing the sale ID.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the branch where the sale is associated.
    /// </summary>
    /// <value>An integer representing the branch ID.</value>
    public int BranchId { get; set; }
}
